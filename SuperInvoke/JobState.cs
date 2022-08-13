namespace Invoke
{

	/// <summary>
	/// Possible job state:
	/// 
	/// Scheduled = any execution of Run or RunRepeat puts the job or the jobrepeat in schedule.
	/// Paused = the delay of the job is freezed
	/// Killed = the job is killed
	/// Completed = the job has been fully completed
	/// </summary>

	public enum JobState {				        
				Scheduled,        
				Paused,        
				Killed,        
				Completed
		}
			
}
