using System;
using System.Collections.Generic;

namespace Invoke
{


	public static class SuperInvoke {

        		/// <summary>
        		/// Constant that models the concept of infinity.
        		/// </summary>
				public const int INFINITY = -1;

                /// <summary>
                /// Optional initialization method.
                /// If never called SuperInvoke will be automatically initialized on its first usage.
                /// </summary>
                /// <param name="dontDestroyOnLoad">Sets "DontDestroyOnLoad" on the SuperInvoke manager game object.</param>
                public static void Init(bool dontDestroyOnLoad = true) {
		            ScheduleBridge.Init(dontDestroyOnLoad);
		        }

    
				#region Run

				/// <summary>
				/// Runs a method after 'delay' seconds.
				/// </summary>
				/// <param name="method">Actual code to run after the delay elapses.</param>
				/// <param name="delay">Delay after which the method will run.</param>
				public static IJob Run(Action method, float delay) { 
						return ActualRun(method, delay, null);
				}


        
        

				/// <summary>
				/// After 'delay' seconds, runs a method.
				/// </summary>
				/// <param name="delay">Delay after which the method will run.</param>
				/// <param name="method">Actual code to run after the delay elapses.</param>
				public static IJob Run(float delay, Action method) {            
						return ActualRun(method, delay, null);        
				}


  
      
				/// <summary>
				/// Runs a method after 'delay' seconds.
				/// Tags the job with 'tag'.
				/// </summary>
				/// <param name="method">Actual code to run after the delay elapses.</param>
				/// <param name="delay">Delay after which the method will run.</param>
				/// <param name="tag">The tag of the job.</param>
				public static IJob Run(Action method, float delay, string tag) {            
						return ActualRun(method, delay, tag);       
				}

        
        
				/// <summary>
				/// After 'delay' seconds, runs a method.
				/// Tags the job with 'tag'.
				/// </summary>
				/// <param name="delay">Delay after which the method will run.</param>
				/// <param name="tag">The tag of the job.</param>
				/// <param name="method">Actual code to run after the delay elapses.</param>
				public static IJob Run(float delay, string tag, Action method) {            
						return ActualRun(method, delay, tag);        
				}
						
				#endregion




        
				#region RunRepeat


        
				/// <summary>
				/// After 'delay' seconds, repeats a method 'repeats' times every 'repeatRate' seconds.
				/// Use SuperInvoke.INFINITY to repeat the job ad infinitum.
				/// </summary>
				/// <param name="delay">Initial delay after which the repeats will start.</param>
				/// <param name="repeatRate">Seconds between each repeat.</param>
				/// <param name="repeats">Total number of repeats.</param>
				/// <param name="method">Actual code to repeat.</param>
				public static IJobRepeat RunRepeat(float delay, float repeatRate, int repeats, Action method) {
						return ActualRunRepeat(method, null, new RepeatSettings(delay, repeatRate, repeats));
				}




        
				/// <summary>        
				/// After 'delay' seconds, repeats a method 'repeats' times every 'repeatRate' seconds.
				/// Use SuperInvoke.INFINITY to repeat the job ad infinitum.        
				/// Tags the job with 'tag'.        
				/// </summary>
				/// <param name="delay">Initial delay after which the repeats will start.</param>
				/// <param name="repeatRate">Seconds between each repeat.</param>
				/// <param name="repeats">Total number of repeats.</param>
				/// <param name="tag">The tag of the job.</param>
				/// <param name="method">Actual code to repeat.</param>
				public static IJobRepeat RunRepeat(float delay, float repeatRate, int repeats, string tag, Action method) {
						return ActualRunRepeat(method, tag, new RepeatSettings(delay, repeatRate, repeats));
				}
        
        
				#endregion

			

		  



				#region Kills

				/// <summary>
				/// Kills every job tagged with 'tag(s)'.
				/// </summary>
				/// <param name="tags">The tag(s) of the jobs that must be killed.</param>
				public static void Kill(params string[] tags) {						
						for(int i = 0; i < tags.Length; i++) {
								ScheduleBridge.Kill(SuperInvokeTag.GetInstance(tags[i]));		
						}
				}


				/// <summary>
				/// Kills every job currently scheduled or paused in the project.
				/// Any sequence in execution will immediately stop its execution.
				/// Any repeated job will stop its execution and will not be repeated anymore.
				/// </summary>
				public static void KillAll() {
						ScheduleBridge.KillAll();
				}


				/// <summary>
				/// Kills every job except those tagged with 'tag(s)'.
				/// </summary>
				/// <param name="tags">The tag(s) of the jobs that must remain alive.</param>
				public static void KillAllExcept(params string[] tags) {
						List<SuperInvokeTag> tagList = new List<SuperInvokeTag>();
						foreach (var tag in tags) {
								tagList.Add(SuperInvokeTag.GetInstance(tag));
						}
						ScheduleBridge.KillAllExcept(tagList.ToArray());
				}



				#endregion

																						


				       		
				#region Other Jobs features
        

				/// <summary>
				/// Pauses every job tagged with 'tag(s)'.
				/// </summary>
				/// <param name="tags">The tag(s) of the jobs to be paused.</param>
				public static void Pause(params string[] tags) {
						for(int i = 0; i < tags.Length; i++) {
								ScheduleBridge.Pause(SuperInvokeTag.GetInstance(tags[i]));
						}		           
		        }
		        
				/// <summary>
				/// Resumes every job tagged with 'tag(s)'.
				/// </summary>
				/// <param name="tag">The tag(s) of the jobs to be resumed.</param>
				public static void Resume(params string[] tags) {
						for(int i = 0; i < tags.Length; i++) {
								ScheduleBridge.Resume(SuperInvokeTag.GetInstance(tags[i]));
						}
		        }

						


		        #endregion







		        #region Skip Frames

		        /// <summary>
		        /// Executes a method after one frame.
		        /// </summary>
		        /// <param name="method">Actual code to execute.</param>
		        public static void SkipFrame(Action method) {
		            ScheduleBridge.SkipFrames(1, method);
		        }

		        /// <summary>
				/// Executes a method after 'frames' frame.
		        /// </summary>
		        /// <param name="frames">The number of frames to skip.</param>
		        /// <param name="method">Actual code to execute.</param>
		        public static void SkipFrames(int frames, Action method) {
		            CheckMethod(method);

		            if (frames < 0) {
							throw new ArgumentException("Argument 'frames' cannot be less than 0.");
		            }

		            if (frames == 0) {
		                method.Invoke();
		            } else {
		                ScheduleBridge.SkipFrames(frames, method);
		            }
		        }

		        #endregion






				#region Creators


				/// <summary> 
				/// Creates an new sequence.
				/// </summary>
				public static ISuperInvokeSequence CreateSequence() {
						return new SequenceBuilder();
				}




				/// <summary>
				/// Returns a unique string tag any time it is called.
				/// Every string tag returned starts with the upper-case sigma symbol 'Σ'.
				/// </summary>		    
				public static string CreateTag() {
						return SuperInvokeTag.CreateStringTag();
				}


				#endregion






		        #region Private section


				private static IJob ActualRun(Action method, float delay, string tag) {
						CheckMethod(method);
						CheckDelay(delay);

						Job job = new Job();
						ISuperInvokeRunnable runnable = SingleTask.MakeInstance(method, delay, tag);
						runnable.SetJob(job);

						ScheduleBridge.Schedule(runnable);
						return job;
				}




			


		        private static IJobRepeat ActualRunRepeat(Action method, string tag, RepeatSettings repeatSettings) {
		            CheckMethod(method);
		            CheckDelay(repeatSettings.delay);

		            JobRepeat jobRepeat = null;
		            if (repeatSettings.repeats == INFINITY || repeatSettings.repeats > 0) { //if repeats == 0 it does nothing

		                jobRepeat = new JobRepeat() { TotalRepeatsNumber = repeatSettings.repeats };

		                Sequence sequence = Sequence.MakeInstance();
		                sequence.SetGroupTag(SuperInvokeTag.GetInstance(tag));
		                sequence.AddSingleTask(SingleTask.MakeInstance(method, 0, tag));
		                sequence.SetIterativeMode(repeatSettings);
		                sequence.SetJob(jobRepeat);

		                ScheduleBridge.Schedule(sequence);
		            }

		            return jobRepeat;
		        }



		        
				private static void CheckDelay(float delay) {		        
						if (delay < 0) {
								throw new ArgumentException("Argument 'delay' cannot be less than 0.");		            
						}
		        }


		        private static void CheckMethod(Action method) {		            
						if (method == null) {		                
								throw new ArgumentException("Argument 'method' cannot be null.");		            
						}
		        }

		        #endregion

    }
}
