using System;
using UnityEngine;

namespace Invoke
{
    internal class JobRepeat : Job, IJobRepeat {

        public int TotalRepeatsNumber { get; set; }
        public int CompletedRepeatsNumber { get; set; }

        public Action OnRepeatCompleteAction = delegate { };

        public new IJobRepeat OnComplete(Action action) {
            return (IJobRepeat)base.OnComplete(action);
        }

        public IJobRepeat OnRepeatComplete(Action action) {
            if (action != null) {
                OnRepeatCompleteAction = action;
            }

            return this;
        }

        public int GetCompletedRepeats() {
            return CompletedRepeatsNumber;
        }

        public int GetTotalRepeats() {
            return TotalRepeatsNumber;
        }

        public int GetRemainingRepeats() {
            return TotalRepeatsNumber == SuperInvoke.INFINITY
                ? SuperInvoke.INFINITY
                : TotalRepeatsNumber - CompletedRepeatsNumber;
        }

        public new IJobRepeat KillOnDetroy(GameObject gameObject) {
            return (IJobRepeat) base.KillOnDetroy(gameObject);
        }

        public new IJobRepeat PauseOnDisable(GameObject gameObject, bool autoResumeOnEnable = true) {
            return (IJobRepeat) base.PauseOnDisable(gameObject, autoResumeOnEnable);
        }
    }
}
