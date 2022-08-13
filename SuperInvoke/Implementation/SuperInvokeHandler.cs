using System.Collections.Generic;
using UnityEngine;

namespace Invoke
{
    internal class SuperInvokeHandler : MonoBehaviour {
        
        private readonly HashSet<Job> pauseOnDisableSet = new HashSet<Job>(); 
        private readonly HashSet<Job> resumeOnEnableSet = new HashSet<Job>();
        private readonly HashSet<Job> killOnDestroySet = new HashSet<Job>();
         
        public void PauseOnDisable(bool autoResumeOnEnable, Job job) {
            
            pauseOnDisableSet.Add(job);

            if (autoResumeOnEnable) {
                resumeOnEnableSet.Add(job);
            }
            else {
                resumeOnEnableSet.Remove(job);
            }
        }

        public void KillOnDestroy(Job job) {
            killOnDestroySet.Add(job);
        }
        
        private void OnEnable() {
            if (ScheduleBridge.IsManagerAlive()) {
                foreach (Job superInvokeTask in resumeOnEnableSet) {
                    superInvokeTask.Resume();
                }
            }
        }

        private void OnDisable() {
            if (ScheduleBridge.IsManagerAlive()) {
                foreach (Job superInvokeTask in pauseOnDisableSet) {
                    superInvokeTask.Pause();
                }
            }
        }

        private void OnDestroy() {
            if (ScheduleBridge.IsManagerAlive()) {
                foreach (Job superInvokeTask in killOnDestroySet) {
                    superInvokeTask.Kill();
                }
            }
        }
    }
}

