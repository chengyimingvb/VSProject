using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace Invoke
{
    internal class SuperInvokeManager : MonoBehaviour {

        private bool inExecution;

        private readonly LinkedList<ISuperInvokeRunnable> scheduleQueue = new LinkedList<ISuperInvokeRunnable>();
        private readonly LinkedList<ISuperInvokeRunnable> executionQueue = new LinkedList<ISuperInvokeRunnable>();
        private readonly LinkedList<ISuperInvokeRunnable> pauseQueue = new LinkedList<ISuperInvokeRunnable>();

        private readonly HashSet<ISuperInvokeRunnable> killAfterExecutionBag = new HashSet<ISuperInvokeRunnable>();
        private readonly HashSet<ISuperInvokeRunnable> pauseAfterExecutionBag = new HashSet<ISuperInvokeRunnable>();
        
        internal void ScheduleTask(ISuperInvokeRunnable superInvokeRunnable) {
            if (superInvokeRunnable is Sequence) {
                ((Sequence)superInvokeRunnable).AddedToScheduler();
            }

            superInvokeRunnable.SetTriggerTime(SystemTime.Current);

            scheduleQueue.AddLast(GetPoolNode(superInvokeRunnable));
        }

        internal void Kill(SuperInvokeTag superInvokeTag) {
            ActualKillTasks(runnable => runnable.HasThisTag(superInvokeTag));
        }

        internal void KillAll() {
            ActualKillTasks(runnable => true);
        }

        internal void KillAllExcept(SuperInvokeTag[] tags) {
            ActualKillTasks(runnable => !HasAnyOfTheseTags(runnable, tags));
        }

        internal void Pause(SuperInvokeTag superInvokeTag) {
           //pause scheduled node
            LinkedListNode<ISuperInvokeRunnable> scheduleNode = scheduleQueue.First;

            while (scheduleNode != null) {
                
                if (scheduleNode.Value.HasThisTag(superInvokeTag)) {
                    LinkedListNode<ISuperInvokeRunnable> nodeToPause = scheduleNode;
                    scheduleNode = scheduleNode.Next;

                    scheduleQueue.Remove(nodeToPause);
                    pauseQueue.AddLast(nodeToPause);

                    nodeToPause.Value.Pause(SystemTime.Current);
                } else {
                    scheduleNode = scheduleNode.Next;
                }

            }

            //pause node in execution
            if (inExecution) {
                for (LinkedListNode<ISuperInvokeRunnable> executionNode = executionQueue.First;
                    executionNode != null;
                    executionNode = executionNode.Next) {
                    
                    if (executionNode.Value.HasThisTag(superInvokeTag) && executionNode.Value.GetJobState() == JobState.Scheduled) {
                        pauseAfterExecutionBag.Add(executionNode.Value);
                        executionNode.Value.Pause(SystemTime.Current);
                    }
                }
            }
        }

        internal void Resume(SuperInvokeTag superInvokeTag) {
            //resume paused nodes
            LinkedListNode<ISuperInvokeRunnable> pausedNode = pauseQueue.First;

            while (pausedNode != null) {
                
                if (pausedNode.Value.HasThisTag(superInvokeTag)) {
                    LinkedListNode<ISuperInvokeRunnable> nodeToResume = pausedNode;
                    pausedNode = pausedNode.Next;

                    pauseQueue.Remove(nodeToResume);
                    scheduleQueue.AddLast(nodeToResume);

                    nodeToResume.Value.Resume(SystemTime.Current);
                } else {
                    pausedNode = pausedNode.Next;
                }

            }

            //resume node in execution
            if (inExecution) {
                for (LinkedListNode<ISuperInvokeRunnable> executionNode = executionQueue.First;
                    executionNode != null;
                    executionNode = executionNode.Next) {
                    
                    if (executionNode.Value.HasThisTag(superInvokeTag) && executionNode.Value.GetJobState() == JobState.Paused) {
                        pauseAfterExecutionBag.Remove(executionNode.Value);
                        executionNode.Value.Resume(SystemTime.Current);
                    }
                }
            }
        }
        

        private void Update() {
            if (scheduleQueue.Count > 0) {

                SelectExecutionQueue();

                if (executionQueue.Count > 0) {
                    inExecution = true;

                    Execute();
                    ReschedulingAfterExecution();

                    inExecution = false;

                    executionQueue.Clear();
                }
            }
        }

        private void SelectExecutionQueue() {
            LinkedListNode<ISuperInvokeRunnable> node = scheduleQueue.First;

            float currentTime = SystemTime.Current;
            while (node != null) {
                if (node.Value.IsReadyToRun(currentTime)) {
                    LinkedListNode<ISuperInvokeRunnable> nodeToRemove = node;
                    node = node.Next;

                    scheduleQueue.Remove(nodeToRemove);
                    executionQueue.AddLast(nodeToRemove);
                } else {
                    node = node.Next;
                }
            }
        }

        private void Execute() {
            for (LinkedListNode<ISuperInvokeRunnable> node = executionQueue.First;
                node != null;
                node = node.Next) {

                node.Value.InvokeTask();
            }
        }

        private void ReschedulingAfterExecution() {
            LinkedListNode<ISuperInvokeRunnable> node = executionQueue.First;

            while (node != null) {

                LinkedListNode<ISuperInvokeRunnable> nextNode = node.Next;
                executionQueue.Remove(node);

                ISuperInvokeRunnable runnable = node.Value;

                if (runnable.NoMoreTasks()) {
                    if (runnable.GetJobState() != JobState.Killed) {
                        runnable.Complete();
                    }
                    runnable.RemovedFromScheduler(); //ended
                } else if (killAfterExecutionBag.Contains(runnable)) {
                    runnable.RemovedFromScheduler(); //killed
                } else if (pauseAfterExecutionBag.Contains(runnable)) {
                    pauseQueue.AddLast(GetPoolNode(runnable)); //paused
                } else {
                    scheduleQueue.AddLast(GetPoolNode(runnable));
                }

                ReturnNode(node);
                node = nextNode;
            }

            killAfterExecutionBag.Clear();
            pauseAfterExecutionBag.Clear();
        }

        private void ActualKillTasks(Func<ISuperInvokeRunnable, bool> killCondition) {

            //kill scheduled node
            LinkedListNode<ISuperInvokeRunnable> scheduleNode = scheduleQueue.First;

            while (scheduleNode != null) {

                if (killCondition(scheduleNode.Value)) {
                    LinkedListNode<ISuperInvokeRunnable> nodeToRemove = scheduleNode;
                    scheduleNode = nodeToRemove.Next;
                    scheduleQueue.Remove(nodeToRemove);

                    nodeToRemove.Value.Kill();
                    nodeToRemove.Value.RemovedFromScheduler();
                } else {
                    scheduleNode = scheduleNode.Next;
                }

            }

            //kill node in execution
            if (inExecution) {
                for (LinkedListNode<ISuperInvokeRunnable> executionNode = executionQueue.First;
                    executionNode != null;
                    executionNode = executionNode.Next) {

                    if (killCondition(executionNode.Value)) {
                        executionNode.Value.Kill();
                        killAfterExecutionBag.Add(executionNode.Value);
                    }
                }
            }

            //kill paused nodes
            LinkedListNode<ISuperInvokeRunnable> pausedNode = pauseQueue.First;

            while (pausedNode != null) {

                if (killCondition(pausedNode.Value)) {
                    LinkedListNode<ISuperInvokeRunnable> nodeToRemove = pausedNode;
                    pausedNode = pausedNode.Next;
                    pauseQueue.Remove(nodeToRemove);

                    nodeToRemove.Value.Kill();
                    nodeToRemove.Value.RemovedFromScheduler();
                } else {
                    pausedNode = pausedNode.Next;
                }

            }
        }

        private bool HasAnyOfTheseTags(ISuperInvokeRunnable runnable, SuperInvokeTag[] tags) {
            bool hasAnyOfTheseTags = false;

            int i = 0;
            while (i < tags.Length && !hasAnyOfTheseTags) {
                hasAnyOfTheseTags = runnable.HasThisTag(tags[i]);
                i++;
            }

            return hasAnyOfTheseTags;
        }

        private LinkedListNode<ISuperInvokeRunnable> GetPoolNode(ISuperInvokeRunnable value) {
            var node = SuperInvokePoolHolder.PoolManager.GetInstance<LinkedListNode<ISuperInvokeRunnable>>();
            node.Value = value;
            return node;
        }

        private void ReturnNode(LinkedListNode<ISuperInvokeRunnable> node) {
            SuperInvokePoolHolder.PoolManager.ReturnInstance(node);
        }

        internal void SkipFrames(int frames, Action method) {
            StartCoroutine(SkipFramesCoroutine(frames, method));
        }

        private IEnumerator SkipFramesCoroutine(int frames, Action method) {

            for (int i = 0; i < frames; i++) {
                yield return null;
            }

            method.Invoke();
        }

        private void OnDestroy() {
            ScheduleBridge.ManagerWasDestroyed();
        }
    }
}
