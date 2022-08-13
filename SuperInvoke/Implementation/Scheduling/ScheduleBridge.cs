

using System;
using UnityEngine;

namespace Invoke
{

    internal static class ScheduleBridge {

        private const string SUPER_INVOKE_MANAGER_GO_NAME = "[SuperInvoke]";
        private const string MANAGER_WAS_DESTROYED_ERR_MSG = "SuperInvoke: '" + SUPER_INVOKE_MANAGER_GO_NAME
            + "' gameobject could not be found in scene. It could have been accidentally destroyed.";

        private static bool isManagerInScene;
        private static bool managerWasDestroyed;

        private static SuperInvokeManager superInvokeManager;

        internal static void Init(bool dontDestroyOnLoad) {
            InstantiateManager(dontDestroyOnLoad);
        }

        internal static void Schedule(ISuperInvokeRunnable runnable) {
            GrabManager();
            superInvokeManager.ScheduleTask(runnable);
        }

        internal static void Kill(SuperInvokeTag tag) {
            if (!managerWasDestroyed && tag != null) {
                GrabManager();
                superInvokeManager.Kill(tag);
            }
        }

        internal static void KillAll() {
            if (!managerWasDestroyed) {
                GrabManager();
                superInvokeManager.KillAll();
            }
        }

        internal static void KillAllExcept(SuperInvokeTag[] tags) {
            if (!managerWasDestroyed && tags != null) {
                GrabManager();
                superInvokeManager.KillAllExcept(tags);
            }
        }

        internal static void Pause(SuperInvokeTag tag) {
            GrabManager();
            superInvokeManager.Pause(tag);
        }

        internal static void Resume(SuperInvokeTag tag) {
            GrabManager();
            superInvokeManager.Resume(tag);
        }
        

        internal static void SkipFrames(int frames, Action method) {
            GrabManager();
            superInvokeManager.SkipFrames(frames, method);
        }


        internal static void ManagerWasDestroyed() {
            managerWasDestroyed = true;
        }

        internal static bool IsManagerAlive() {
            return superInvokeManager != null;
        }

        private static void GrabManager() {

            if (managerWasDestroyed) {
                Debug.LogError(MANAGER_WAS_DESTROYED_ERR_MSG);
            }

            InstantiateManager(true);
        }

        private static void InstantiateManager(bool dontDestroyOnLoad) {
            if (!isManagerInScene) {
                superInvokeManager = new GameObject(SUPER_INVOKE_MANAGER_GO_NAME).AddComponent<SuperInvokeManager>();
                superInvokeManager.transform.hideFlags = HideFlags.HideInHierarchy;
                if (dontDestroyOnLoad) {
                    GameObject.DontDestroyOnLoad(superInvokeManager);
                }

                isManagerInScene = true;
            }
        }
    }
}
