
using System.Collections.Generic;

namespace Invoke
{

    internal class Sequence : ISuperInvokeRunnable {
        private const int FIRST_CYCLE = 1;

        private readonly LinkedList<SingleTask> tasks = new LinkedList<SingleTask>();
        private LinkedListNode<SingleTask> node;

        private bool iterativeMode;
        private int cycleCount;
        private RepeatSettings repeatSettings;
        
        private bool noMoreTasks;

        private SuperInvokeTag groupTag;
        private Job job;
        private JobRepeat jobRepeat;

        public static Sequence MakeInstance() {
            return SuperInvokePoolHolder.PoolManager.GetInstance<Sequence>();
        }

        public void AddedToScheduler() {
            node = tasks.First;
            cycleCount = FIRST_CYCLE;
        }

        public void SetIterativeMode(RepeatSettings repeatSettings) {
            this.repeatSettings = repeatSettings;
            iterativeMode = true;
        }

        public void AddSingleTask(SingleTask task) {
            tasks.AddLast(GetPoolNode(task));
        }

        public void SetGroupTag(SuperInvokeTag tag) {
            groupTag = tag;
        }

        public void SetJob(IJob job) {
            this.job = (Job) job;

            if (job is JobRepeat) {
                jobRepeat = (JobRepeat) job;
            }
        }

        public JobState GetJobState() {
            return job.GetState();
        }

        public bool HasThisTag(SuperInvokeTag tag) {
            return tag.Equals(groupTag) || tag.Equals(job.Tag);
        }

        public void SetTriggerTime(float currentTime) {
            float triggerTime = currentTime;

            if (iterativeMode && node == tasks.First) {
                triggerTime += cycleCount == FIRST_CYCLE
                    ? repeatSettings.delay
                    : repeatSettings.repeatRate;
            }

            node.Value.SetTriggerTime(triggerTime);
        }

        public float GetTriggerTime() {
            return node.Value.GetTriggerTime();
        }

        public bool IsReadyToRun(float currentTime) {
            return currentTime >= GetTriggerTime();
        }

        public void InvokeTask() {
            float lastTriggerTime = GetTriggerTime();
            SingleTask task = node.Value;

            bool invokeRepeatCompleteAction = false;

            if (node == tasks.Last) {//last node

                //Continue after last node in these cases:
                // - infite loops
                // - limited loops but with avaible cycle yet
                //Else
                // - execution ended
                if (iterativeMode && (!repeatSettings.thereIsIterationLimit || cycleCount < repeatSettings.repeats)) {
                    cycleCount++;
                    node = tasks.First;
                    SetTriggerTime(lastTriggerTime);
                } else {
                    noMoreTasks = true;
                }

                invokeRepeatCompleteAction = iterativeMode;
            } else {
                node = node.Next;
                SetTriggerTime(lastTriggerTime);
            }

            task.InvokeTask();
            
            if (invokeRepeatCompleteAction) {
                jobRepeat.CompletedRepeatsNumber++;
                
                if (!jobRepeat.IsKilled()) {
                    jobRepeat.OnRepeatCompleteAction.Invoke();
                }
            }
        }

        public bool NoMoreTasks() {
            return noMoreTasks;
        }

        public void Pause(float currentTime) {
            for (LinkedListNode<SingleTask> task = tasks.First;
                    task != null;
                    task = task.Next) {

                task.Value.Pause(currentTime);
            }

            job.State = JobState.Paused;
        }

        public void Resume(float currentTime) {
            for (LinkedListNode<SingleTask> task = tasks.First;
                    task != null;
                    task = task.Next) {

                task.Value.Resume(currentTime);
            }

            job.State = JobState.Scheduled;
        }

        public void Kill() {
            job.State = JobState.Killed;
        }

        public void Complete() {
            job.State = JobState.Completed;
            job.OnCompleteAction.Invoke();
        }


        public void RemovedFromScheduler() {
            groupTag = null;
            job = null;
            jobRepeat = null;
            noMoreTasks = false;

            iterativeMode = false;
            node = null;

            for (LinkedListNode<SingleTask> task = tasks.First;
                task != null;
                task = task.Next) {

                task.Value.RemovedFromScheduler();
            }

            for (LinkedListNode<SingleTask> taskNode = tasks.First; taskNode != null; taskNode = taskNode.Next) {
                ReturnNode(taskNode);
            }
            tasks.Clear();

            SuperInvokePoolHolder.PoolManager.ReturnInstance<Sequence>(this);
        }

        private LinkedListNode<SingleTask> GetPoolNode(SingleTask value) {
            LinkedListNode<SingleTask> node = SuperInvokePoolHolder.PoolManager.GetInstance<LinkedListNode<SingleTask>>();
            node.Value = value;
            return node;
        }

        private void ReturnNode(LinkedListNode<SingleTask> node) {
            SuperInvokePoolHolder.PoolManager.ReturnInstance(node);
        }
    }
}
