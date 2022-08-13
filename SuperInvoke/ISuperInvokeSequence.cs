using System;

namespace Invoke
{

	/// <summary>
	/// A sequence of an arbitrary number of methods and delays.
	/// </summary>    
	public interface ISuperInvokeSequence {


        
				#region Sequence composers
        		/// <summary>
        		/// Adds a method to the sequence. 
				/// It will be executed after "delay" seconds from the previous method.
        		/// </summary>
        		ISuperInvokeSequence AddMethod(Action method, float delay = 0);

        		/// <summary>
        		/// Adds a method to the sequence. 
				/// It will be executed after "delay" seconds from the previous method.
        		/// </summary>
        		ISuperInvokeSequence AddMethod(float delay, Action method);

        		/// <summary>
        		/// Adds a delay to the sequence.
        		/// </summary>
        		ISuperInvokeSequence AddDelay(float delay);

				#endregion


		        void Clear();


				#region Run


        		/// <summary>
        		/// Runs the sequence.
        		/// </summary>
        		IJob Run();

        		/// <summary>
        		/// Runs the sequence.
		        /// Tags the job with 'tag'.
		        /// </summary>
				/// <param name="tage">The tag of the job.</param>
		        IJob Run(string tag);

		        /// <summary>
		        /// Runs the entire sequence after "delay" seconds.
		        /// </summary>
		        IJob Run(float delay);

		        /// <summary>
		        /// Runs the entire sequence after "delay" seconds.
		        /// Tags the job with 'tag'.
		        /// </summary>
				/// <param name="tagName">The tag of the job.</param>
		        IJob Run(float delay, string tagName);

		        #endregion



        		#region RunRepeat


		        /// <summary>
		        /// After 'delay' seconds, repeats the sequence 'repeats' times every 'repeatRate' seconds.
				/// Use SuperInvoke.INFINITY to repeat the job ad infinitum.
		        /// </summary>
		        /// <param name="delay">Initial delay after which the repeats will start.</param>
		        /// <param name="repeatRate">Seconds between each repeat.</param>
		        /// <param name="repeats">Total number of repeats.</param>
		        IJobRepeat RunRepeat(float delay, float repeatRate, int repeats);

		        /// <summary>
		        /// After 'delay' seconds, repeats the sequence 'repeats' times every 'repeatRate' seconds.
				/// Use SuperInvoke.INFINITY to repeat the job ad infinitum.
		        /// Tags the task with 'tag'.
		        /// </summary>
		        /// <param name="delay">Initial delay after which the repeats will start.</param>
		        /// <param name="repeatRate">Seconds between each repeat.</param>
		        /// <param name="repeats">Total number of repeats.</param>
		        /// <param name="tag">The tag of the job.</param>
		        IJobRepeat RunRepeat(float delay, float repeatRate, int repeats, string tag);
		        
				#endregion
	
    }
}
