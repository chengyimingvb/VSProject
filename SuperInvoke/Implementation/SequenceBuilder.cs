

using System;
using System.Collections.Generic;

namespace Invoke
{

    internal class SequenceBuilder : ISuperInvokeSequence {

        private const int DefaultCapacity = 10;
        private List<DelayedTask> tasks = new List<DelayedTask>(DefaultCapacity); 

        private bool thereIsDelayToAdd;
        private float delayToAdd;

        private bool thereIsSequenceDelayToAdd;
        private float sequenceDelayToAdd;

        private SuperInvokeTag superInvokeTag;
        

        #region Sequence creation

        public ISuperInvokeSequence AddMethod(Action method, float delay = 0) {
            CheckMethod(method);
            CheckDelay(delay);
            DelayedTask task = new DelayedTask(method, delay);

            if (thereIsDelayToAdd) {
                task.DelayTime += delayToAdd;
                thereIsDelayToAdd = false;
            }

            tasks.Add(task);

            return this;
        }

        public ISuperInvokeSequence AddMethod(float delay, Action method) {
            CheckMethod(method);
            CheckDelay(delay);
            AddMethod(method, delay);
            return this;
        }

        public ISuperInvokeSequence AddDelay(float delay) {
            CheckDelay(delay);
            if (thereIsDelayToAdd) {
                delayToAdd += delay;
            }
            else {
                delayToAdd = delay;
                thereIsDelayToAdd = true;
            }

            return this;
        }

        #endregion

        public void Clear() {
            tasks.Clear();
            thereIsDelayToAdd = false;
            delayToAdd = 0;
            thereIsSequenceDelayToAdd = false;
            superInvokeTag = null;
        }


        #region Runs



        public IJob Run() {
            return ScheduleSequence();
        }

        public IJob Run(float delay) {
            CheckDelay(delay);
            SetSequenceDelayToAdd(delay);
            return ScheduleSequence();
        }
        
        public IJob Run(string tagName) {
            SetGroupTag(tagName);
            return ScheduleSequence();
        }
        
        public IJob Run(float delay, string tagName) {
            CheckDelay(delay);
            SetSequenceDelayToAdd(delay);
            SetGroupTag(tagName);
            return ScheduleSequence();
        }


		#endregion



		#region RunRepeat



		public IJobRepeat RunRepeat(float delay, float repeatRate, int repeats) {
		    return ActualRunRepeat(delay, repeatRate, repeats, null);

		}

		public IJobRepeat RunRepeat(float delay, float repeatRate, int repeats, string tag) {
		    return ActualRunRepeat(delay, repeatRate, repeats, tag);
		}

        private IJobRepeat ActualRunRepeat(float delay, float repeatRate, int repeats, string tag) {
            CheckDelay(delay);
            IJob job = null;

            RepeatSettings repeatSettings = new RepeatSettings(delay, repeatRate, repeats);

            if (repeats == SuperInvoke.INFINITY || repeats > 0) {
                SetGroupTag(tag);
                job = ScheduleSequence(repeatSettings);
            }

            return (IJobRepeat)job;
        }
        
        #endregion
       


        

		#region Private Stuff


        private IJob ScheduleSequence(object repeatOpt = null) {

            IJob job = null;

            Sequence sequence = Sequence.MakeInstance();
            sequence.SetGroupTag(superInvokeTag);

            float seqDelay = 0;
            if (thereIsSequenceDelayToAdd) {
                seqDelay = sequenceDelayToAdd;
                thereIsSequenceDelayToAdd = false;
            }

            for (int i = 0; i < tasks.Count; i++) {
                DelayedTask t = tasks[i];
                SingleTask singleTask = SingleTask.MakeInstance(t.Method, seqDelay + t.DelayTime, null);
                seqDelay = 0;
                sequence.AddSingleTask(singleTask);
            }
            

            if (thereIsDelayToAdd) {
                SingleTask singleTask = SingleTask.MakeInstance(delegate {}, seqDelay + delayToAdd, null);
                sequence.AddSingleTask(singleTask);
            } else if (tasks.Count == 0) {
                SingleTask singleTask = SingleTask.MakeInstance(delegate {}, seqDelay, null);
                sequence.AddSingleTask(singleTask);
            }

            if (repeatOpt != null) {
                RepeatSettings repeatSettings = (RepeatSettings)repeatOpt;
                JobRepeat jobRepeat = new JobRepeat() { TotalRepeatsNumber = repeatSettings.repeats };

                sequence.SetIterativeMode(repeatSettings);

                job = jobRepeat;
            } else {
                job = new Job();
            }

            sequence.SetJob(job);
            ScheduleBridge.Schedule(sequence);

            ResetStuff();

            return job;
        }

        private void ResetStuff() {
            superInvokeTag = null;
        }

        private void SetSequenceDelayToAdd(float delay) {
            sequenceDelayToAdd = delay;
            thereIsSequenceDelayToAdd = true;
        }

        private void SetGroupTag(string tag) {
            if (tag != null) {
                superInvokeTag = SuperInvokeTag.GetInstance(tag);
            }
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

        private struct DelayedTask {
            public Action Method;
            public float DelayTime;

            public DelayedTask(Action method, float delayTime) {
                Method = method;
                DelayTime = delayTime;
            }
        }

		#endregion
    }
}
