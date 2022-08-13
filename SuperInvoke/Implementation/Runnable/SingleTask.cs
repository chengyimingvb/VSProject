using System;

namespace Invoke
{
    internal class SingleTask : ISuperInvokeRunnable {

        private Action method;
        private float delayTime;
        private float triggerTime;

        private float pausedTime;

        private bool noMoreTasks;

        private SuperInvokeTag groupTag;
        private Job job;

        public static SingleTask MakeInstance(Action method, float delayTime, string groupTag) {
            SingleTask singleTask = SuperInvokePoolHolder.PoolManager.GetInstance<SingleTask>();
            singleTask.SetTask(method, delayTime);

            if (groupTag != null) {
                singleTask.groupTag = SuperInvokeTag.GetInstance(groupTag);
            }
            
            return singleTask;
        }

        public void SetTask(Action method, float delayTime) {
            this.method = method;
            this.delayTime = delayTime;
        }

        public void SetGroupTag(SuperInvokeTag tag) {
            groupTag = tag;
        }

        public void SetJob(IJob job) {
            this.job = (Job)job;
        }

        public JobState GetJobState() {
            return job.GetState();
        }

        public bool HasThisTag(SuperInvokeTag tag) {
            return tag.Equals(groupTag) || tag.Equals(job.Tag);
        }

        public void SetTriggerTime(float currentTime) {
            triggerTime = currentTime + delayTime;
        }

        public float GetTriggerTime() {
            return triggerTime;
        }

        public bool IsReadyToRun(float currentTime) {
            return currentTime >= GetTriggerTime();
        }

        public void InvokeTask() {
            method.Invoke();
            noMoreTasks = true;
        }

        public bool NoMoreTasks() {
            return noMoreTasks;
        }

        public void Pause(float currentTime) {
            pausedTime = currentTime;

            if (job != null) {
                job.State = JobState.Paused;
            }
        }

        public void Resume(float currentTime) {
            triggerTime += currentTime - pausedTime;

            if (job != null) {
                job.State = JobState.Scheduled;
            }
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

            noMoreTasks = false;

            method = null;
            SuperInvokePoolHolder.PoolManager.ReturnInstance<SingleTask>(this);
        }
    }
}
