using System;
using UnityEngine;

namespace Invoke
{


	public interface IJobRepeat : IJob {
        
				/// <summary>
				/// Executes 'action' when this job has finished.
				/// </summary>
				/// <param name="action">The action to execute.</param>
				new IJobRepeat OnComplete(Action action);
        
				/// <summary>
				/// Executes 'action' after each repeat is completed.
				/// </summary>
				/// <param name="action">The action to execute.</param>
				IJobRepeat OnRepeatComplete(Action action);

        
				/// <summary>
				/// Returns the number of completed repeats of this job.
				/// </summary>
				/// <returns>The repeats executed.</returns>
				int GetCompletedRepeats();
        

				/// <summary>
				/// Returns the number of repeats this job must yet execute.
				/// </summary>
				/// <returns>The remaining repeats.</returns>
				int GetRemainingRepeats();


				/// <summary>
				/// Returns the total number of repeats of this job.
				/// </summary>
				/// <returns>The total repeats.</returns>
				int GetTotalRepeats();
        


				/// <summary>
				/// Automatically kills this job when 'gameobject' is destroyed.
				/// </summary>
				/// <param name="gameObject">The game object to depend upon.</param>
				new IJobRepeat KillOnDetroy(GameObject gameObject);
        
				/// <summary>
				/// Pauses this job when 'gameobject' is disabled.
				/// By default, automatically resumes this job when 'gameobject' is enabled.
				/// </summary>
				/// <param name="gameObject">The game object to depend upon.</param>
				/// <param name="autoResumeOnEnable">If set to <c>true</c> automatically resumes this job OnEnable.</param>
				new IJobRepeat PauseOnDisable(GameObject gameObject, bool autoResumeOnEnable = true);
    }
}