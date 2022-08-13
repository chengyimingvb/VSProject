using System;
using UnityEngine;

namespace Invoke
{
    internal class Job : IJob {

        public SuperInvokeTag Tag { get; private set; } 


        public JobState State { get; set; }

        public Action OnCompleteAction = delegate { };
        
        public Job() {
            Tag = SuperInvokeTag.CreateInstance();
        }

        public IJob OnComplete(Action action) {
            if (action != null) {
                OnCompleteAction = action;
            }
            
            return this;
        }

        public void Pause() {
            ScheduleBridge.Pause(Tag);
        }

        public void Resume() {
            ScheduleBridge.Resume(Tag);
        }

       
        public void Kill() {
            ScheduleBridge.Kill(Tag);
        }

        public JobState GetState() {
            return State;
        }

        public bool IsScheduled() {
            return State == JobState.Scheduled;
        }

        public bool IsPaused() {
            return State == JobState.Paused;
        }

        public bool IsKilled() {
            return State == JobState.Killed;
        }

        public bool IsCompleted() {
            return State == JobState.Completed;
        }

        public IJob KillOnDetroy(GameObject gameObject) {
            GrabSuperInvokeHandler(gameObject)
                .KillOnDestroy(this);

            return this;
        }

        public IJob PauseOnDisable(GameObject gameObject, bool autoResumeOnEnable = true) {
            GrabSuperInvokeHandler(gameObject)
                .PauseOnDisable(autoResumeOnEnable, this);

            return this;
        }
        
        private SuperInvokeHandler GrabSuperInvokeHandler(GameObject gameObject) {
            SuperInvokeHandler superInvokeHandler = gameObject.GetComponent<SuperInvokeHandler>();

            if (superInvokeHandler == null) {
                superInvokeHandler = gameObject.AddComponent<SuperInvokeHandler>();
            }

            return superInvokeHandler;
        }
    }
}
