using System;
using UnityEngine;


namespace Invoke
{


	/// <summary>
	/// A Job is any delayed action, either throught SuperInvoke.Run or sequence.Run.
	/// </summary>
	public interface IJob {
				
				/// <summary>
				/// Executes 'action' when this job has finished.
				/// </summary>
				/// <param name="action">Action to execute.</param>        
				IJob OnComplete(Action action);

        
				/// <summary>
				/// Pauses this job.
				/// </summary>
				void Pause();
        
				/// <summary>
				/// Resumes this job.
				/// </summary>
				void Resume();
        
				/// <summary>
				/// Kills this job.
				/// </summary>
				void Kill();

        
				/// <summary>
				/// Returns the current state of this job.
				/// </summary>
				/// <returns>The state of this job.</returns>
				JobState GetState();
        

				/// <summary>
				/// Determines whether this job is scheduled.
				/// A job is scheduled when its delay is still not elasped or it is currently executing.
				/// </summary>
				/// <returns><c>true</c> if this job is scheduled; otherwise, <c>false</c>.</returns>
				bool IsScheduled();
        
				/// <summary>
				/// Determines whether this job is paused.
				/// </summary>
				/// <returns><c>true</c> if this job is paused; otherwise, <c>false</c>.</returns>
				bool IsPaused();
        
				/// <summary>
				/// Determines whether this job is killed.
				/// </summary>
				/// <returns><c>true</c> if this instance is killed; otherwise, <c>false</c>.</returns>
				bool IsKilled();
        
				/// <summary>
				/// Determines whether this job is completed.
				/// </summary>
				/// <returns><c>true</c> if this job is completed; otherwise, <c>false</c>.</returns>
				bool IsCompleted();

        
				/// <summary>
				/// Automatically kills this job when 'gameobject' is destroyed.
				/// </summary>
				/// <param name="gameObject">The game object to depend upon.</param>
				IJob KillOnDetroy(GameObject gameObject);

				/// <summary>
				/// Pauses this job when 'gameobject' is disabled.
				/// By default, automatically resumes this job when 'gameobject' is enabled.
				/// </summary>
				/// <param name="gameObject">The game object to depend upon.</param>
				/// <param name="autoResumeOnEnable">If set to <c>true</c> automatically resumes this job OnEnable.</param>
				IJob PauseOnDisable(GameObject gameObject, bool autoResumeOnEnable = true);
    }
}
