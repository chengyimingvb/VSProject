using System;

namespace Invoke
{

    internal struct RepeatSettings {

        public readonly float delay;
        public readonly float repeatRate;

        public readonly int repeats;
        public readonly bool thereIsIterationLimit;


        public RepeatSettings(float delay, float repeatRate, int repeats) {
            
            if (repeats < SuperInvoke.INFINITY) {
				throw new ArgumentException("Argument 'repeats' cannot be less than -1. (INFINITY is modelled with '-1'.)");
            }

            if (repeatRate < 0) {
				throw new ArgumentException("Argument 'repeatRate' cannot be less than 0.");
            }

			this.delay = delay;
			this.repeatRate = repeatRate;
			this.repeats = repeats;

            thereIsIterationLimit = repeats != SuperInvoke.INFINITY;
        }
    }
}
