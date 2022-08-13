

namespace Invoke
{
    internal interface ISuperInvokeRunnable {
        void SetGroupTag(SuperInvokeTag tag);
        void SetJob(IJob job);
        JobState GetJobState();

        bool HasThisTag(SuperInvokeTag tag);

        void SetTriggerTime(float currentTime);
        float GetTriggerTime();
        bool IsReadyToRun(float currentTime);
        void InvokeTask();
        bool NoMoreTasks();
        void Pause(float currentTime);
        void Resume(float currentTime);
        void Kill();
        void Complete();
        void RemovedFromScheduler();
    }
}
