using System;
using System.Collections.Generic;
using UnityEngine;
namespace CYM
{
    public enum ScreenEdgeType
    { 
        None,
        Left,
        Right,
        Top,
        Bot,
    }
    public class BaseUtil
    {
        #region static
        static DateTime DateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        static TimeSpan TimeSpan = new TimeSpan();
        #endregion

        #region info
        public static string SimpleSystemInfo
        {
            get
            {
                string info =
                       "OS:" + SystemInfo.operatingSystem +
                       "\nProcessor:" + SystemInfo.processorType +
                       "\nMemory:" + SystemInfo.systemMemorySize +
                       "\nGraphics API:" + SystemInfo.graphicsDeviceType +
                       "\nGraphics Processor:" + SystemInfo.graphicsDeviceName +
                       "\nGraphics Memory:" + SystemInfo.graphicsMemorySize +
                       "\nGraphics Vendor:" + SystemInfo.graphicsDeviceVendor;
                return info;
            }
        }
        // 基本系统信息
        public static string BaseSystemInfo
        {
            get
            {
                string systemInfo =
                "DeviceModel：" + SystemInfo.deviceModel +
                "\nDeviceName：" + SystemInfo.deviceName +
                "\nDeviceType：" + SystemInfo.deviceType +
                "\nGraphicsDeviceName：" + SystemInfo.graphicsDeviceName +
                "\nGraphicsDeviceVersion:" + SystemInfo.graphicsDeviceVersion +
                "\nGraphicsMemorySize（M）：" + SystemInfo.graphicsMemorySize +
                "\nGraphicsShaderLevel：" + SystemInfo.graphicsShaderLevel +
                "\nMaxTextureSize：" + SystemInfo.maxTextureSize +
                "\nOperatingSystem：" + SystemInfo.operatingSystem +
                "\nProcessorCount：" + SystemInfo.processorCount +
                "\nProcessorType：" + SystemInfo.processorType +
                "\nSystemMemorySize：" + SystemInfo.systemMemorySize;

                return systemInfo;
            }
        }
        // 高级系统信息
        public static string AdvSystemInfo
        {
            get
            {
                string systemInfo =
                "DeviceModel：" + SystemInfo.deviceModel +
                "\nDeviceName：" + SystemInfo.deviceName +
                "\nDeviceType：" + SystemInfo.deviceType +
                "\nDeviceUniqueIdentifier：" + SystemInfo.deviceUniqueIdentifier +
                "\nGraphicsDeviceID：" + SystemInfo.graphicsDeviceID +
                "\nGraphicsDeviceName：" + SystemInfo.graphicsDeviceName +
                "\nGraphicsDeviceVendor：" + SystemInfo.graphicsDeviceVendor +
                "\nGraphicsDeviceVendorID:" + SystemInfo.graphicsDeviceVendorID +
                "\nGraphicsDeviceVersion:" + SystemInfo.graphicsDeviceVersion +
                "\nGraphicsMemorySize（M）：" + SystemInfo.graphicsMemorySize +
                "\nGraphicsShaderLevel：" + SystemInfo.graphicsShaderLevel +
                "\nMaxTextureSize：" + SystemInfo.maxTextureSize +
                "\nNpotSupport：" + SystemInfo.npotSupport +
                "\nOperatingSystem：" + SystemInfo.operatingSystem +
                "\nProcessorCount：" + SystemInfo.processorCount +
                "\nProcessorType：" + SystemInfo.processorType +
                "\nSupportedRenderTargetCount：" + SystemInfo.supportedRenderTargetCount +
                "\nSupports3DTextures：" + SystemInfo.supports3DTextures +
                "\nSupportsAccelerometer：" + SystemInfo.supportsAccelerometer +
                "\nSupportsComputeShaders：" + SystemInfo.supportsComputeShaders +
                "\nSupportsGyroscope：" + SystemInfo.supportsGyroscope +
                "\nSupportsInstancing：" + SystemInfo.supportsInstancing +
                "\nSupportsLocationService：" + SystemInfo.supportsLocationService +
                "\nSupportsShadows：" + SystemInfo.supportsShadows +
                "\nSupportsSparseTextures：" + SystemInfo.supportsSparseTextures +
                "\nSupportsVibration：" + SystemInfo.supportsVibration +
                "\nSystemMemorySize：" + SystemInfo.systemMemorySize;

                return systemInfo;
            }
        }
        #endregion

        #region get ray cast pos
        /// <summary>
        /// 获得Y轴坐标
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="yoffset"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static Vector3 GetRaycastY(Transform trans, float yoffset, LayerData layer)
        {
            return GetRaycastY(trans, yoffset, (LayerMask)layer);
        }
        /// <summary>
        /// 获得Y轴坐标
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="yoffset"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static Vector3 GetRaycastY(Transform trans, float yoffset, LayerMask mask)
        {
            RaycastHit hitInfo;
            Vector3 opos = trans.position + Vector3.up * 99999.0f;
            Physics.Raycast(new Ray(opos, trans.position - opos), out hitInfo, float.MaxValue, mask);
            return new Vector3(hitInfo.point.x, yoffset + hitInfo.point.y, hitInfo.point.z);
        }
        public static Vector3 GetRaycastY(Vector3 pos, float yoffset, LayerMask mask)
        {
            RaycastHit hitInfo;
            Vector3 opos = pos + Vector3.up * 99999.0f;
            Physics.Raycast(new Ray(opos, pos - opos), out hitInfo, float.MaxValue, mask);
            return new Vector3(hitInfo.point.x, yoffset + hitInfo.point.y, hitInfo.point.z);
        }
        #endregion

        #region Ray cast
        public static GameObject MousePick(int mask)
        {
            if (Camera.main == null)
                return null;
            GameObject ret = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
            {
                ret = hitInfo.collider.gameObject;
            }
            return ret;
        }
        public static GameObject ScreenPick(Vector2 screenPos,int mask)
        {
            if (Camera.main == null)
                return null;
            GameObject ret = null;
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
            {
                ret = hitInfo.collider.gameObject;
            }
            return ret;
        }
        public static void FollowToMousePos(GameObject go, float fixedHeight = 0.5f)
        {
            if (Camera.main == null) 
                return;
            if (go == null)
                return;
            Vector3 dragItemScreenSpace = Camera.main.WorldToScreenPoint(go.transform.position);
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragItemScreenSpace.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);
            currentPosition = currentPosition.SetY(fixedHeight);
            go.transform.position = currentPosition;
        }
        public static void FollowToScreenPos(GameObject go,Vector2 screenPos, float fixedHeight = 0.5f)
        {
            if (Camera.main == null)
                return;
            if (go == null)
                return;
            Vector3 dragItemScreenSpace = Camera.main.WorldToScreenPoint(go.transform.position);
            Vector3 currentScreenSpace = new Vector3(screenPos.x, screenPos.y, dragItemScreenSpace.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);
            currentPosition = currentPosition.SetY(fixedHeight);
            go.transform.position = currentPosition;
        }
        /// <summary>
        /// 检测Y轴碰撞体,防止下沉
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="yoffset"></param>
        /// <param name="mask"></param>
        public static void RaycastY(Transform trans, float yoffset, LayerData layer)
        {
            trans.position = GetRaycastY(trans, yoffset, layer);
        }
        /// <summary>
        /// 检测Y轴碰撞体,防止下沉
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="yoffset"></param>
        /// <param name="layer"></param>
        public static void RaycastY(Transform trans, float yoffset, LayerMask mask)
        {
            trans.position = GetRaycastY(trans, yoffset, mask);
        }
        public static bool RayCast(out RaycastHit hit, Vector2 pos, LayerMask layer)
        {
            hit = new RaycastHit();
            if (Camera.main == null) 
                return false;
            return Physics.Raycast(new Ray(new Vector3(pos.x, int.MaxValue * 0.5f, pos.y), -Vector3.up), out hit, int.MaxValue, layer, QueryTriggerInteraction.Collide);
        }
        public static bool ScreenRayCast(out RaycastHit hit, Vector2 screenPos, LayerMask layer)
        {
            hit = new RaycastHit();
            if (Camera.main == null) 
                return false;
            return Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out hit, 9999, layer, QueryTriggerInteraction.Collide);
        }
        public static bool ScreenCenterRayCast(out RaycastHit hit, LayerMask layer)
        {
            return ScreenRayCast(out hit, new Vector2(Screen.width / 2, Screen.height / 2),layer);
        }
        public static bool OverlapPoint2D(out Collider2D collider, Vector2 screenPos, LayerMask layer)
        {
            collider = null;
            if (Camera.main == null) 
                return false;
            collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(screenPos), layer, -9999, 9999);
            return collider != null;
        }
        public static bool OverlapPointAll2D(out Collider2D[] collider, Vector2 screenPos, LayerMask layer)
        {
            collider = null;
            if (Camera.main == null) 
                return false;
            collider = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(screenPos), layer, -9999, 9999);
            return collider != null;
        }
        public static (Collider col , Collider2D col2D) PickCollider(Vector2 screenPos, LayerMask layer)
        {
            if (ScreenRayCast(out RaycastHit hitInfo, screenPos, layer))
            {
                return (hitInfo.collider,null);
            }
            else if(OverlapPoint2D(out Collider2D collider2D, screenPos, layer))
            {
                return (null, collider2D);
            }
            return (null,null);
        }
        public static Component PickColliderCom(Vector2 screenPos, LayerMask layer)
        {
            var picked = PickCollider(screenPos, layer);
            if (picked.col != null)
                return picked.col;
            else if (picked.col2D != null)
                return picked.col2D;
            return null;
        }
        public static (Collider[] cols, Collider2D[] cols2D) OverlapSphere(Vector3 pos,float radius, LayerMask layer)
        {
            var cols = Physics.OverlapSphere(pos, radius, layer);
            if (cols != null && cols.Length > 0)
            {
                return (cols, null);
            }
            else
            {
                var cols2D = Physics2D.OverlapCircleAll(pos, radius, layer, -9999, 9999);
                if (cols2D != null && cols2D.Length > 0)
                {
                    return (null, cols2D);
                }
            }
            return (null, null);
        }
        public static Component[] OverlapSphereCom(Vector3 pos, float radius, LayerMask layer)
        {
            var picked = OverlapSphere(pos, radius, layer);
            if (picked.cols != null)
                return picked.cols;
            else if (picked.cols2D != null)
                return picked.cols2D;
            return null;
        }
        public static ScreenEdgeType ScreenEdge(GameObject go,float width)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            if (screenPos.y >= Screen.height - width)
            {
                return ScreenEdgeType.Top;
            }
            if (screenPos.y <= width)
            {
                return ScreenEdgeType.Bot;
            }
            if (screenPos.x <= width)
            {
                return ScreenEdgeType.Left;
            }
            if (screenPos.x >= Screen.width - width)
            {
                return ScreenEdgeType.Right;
            }
            return ScreenEdgeType.None;
        }
        public static bool KeepInScreenEdge(GameObject go, float screenEdgeWidth,float moveStep)
        {
            bool ret = false;
            var curTrans = go.transform;
            var screenEdge = ScreenEdge(go, screenEdgeWidth);
            var sourcePos = curTrans.position;
            if (screenEdge == ScreenEdgeType.Top)
            {
                curTrans.position = curTrans.position.SetXZ(sourcePos.x + moveStep, sourcePos.z - moveStep);
                ret = true;
            }
            else if (screenEdge == ScreenEdgeType.Bot)
            {
                curTrans.position = curTrans.position.SetXZ(sourcePos.x - moveStep, sourcePos.z + moveStep);
                ret = true;
            }
            else if (screenEdge == ScreenEdgeType.Left)
            {
                curTrans.position = curTrans.position.SetXZ(sourcePos.x + moveStep, sourcePos.z + moveStep);
                ret = true;
            }
            else if (screenEdge == ScreenEdgeType.Right)
            {
                curTrans.position = curTrans.position.SetXZ(sourcePos.x - moveStep, sourcePos.z - moveStep);
                ret = true;
            }
            return ret;
        }
        #endregion

        #region pos
        public static Vector3 MousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        }
        #endregion

        #region mail
        public static void Send(string mailTo, string subject, string body)
        {
            Application.OpenURL(string.Format("mailto:{0}?subject={1}&body={2}", mailTo, Uri.EscapeDataString(subject), Uri.EscapeDataString(body)));
        }
        #endregion

        #region other
        public static bool IsArrayValid<T>(IList<T> data)
        {
            if (data == null) return false;
            if (data.Count <= 0)
                return false;
            return true;
        }
        public static void CopyTextToClipboard(string str)
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = str;
            textEditor.OnFocus();
            textEditor.Copy();
        }
        public static T Cast<T>(object obj) where T : class
        {
            return obj as T;
        }

        public static List<Type> ReflectionSubClass(Type type)
        {
            List<Type> types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var item in assembly.GetTypes())
                {
                    if (item.IsDerivedFromOpenGenericType(type))
                    {
                        if (item.IsClass && !item.IsAbstract)
                        {
                            types.Add(item);
                        }
                    }
                }
            }
            return types;
        }
        #endregion

        #region screen pos
        public static float PosScreenX(Vector3 WorldPos)
        {
            if (Camera.main == null)
                return 0;
            var Pos = Camera.main.WorldToScreenPoint(WorldPos);
            float x = Pos.x / Screen.width;
            return x;
        }
        // 转换屏幕坐标Y
        public static float PosScreenY(Vector3 WorldPos)
        {
            if (Camera.main == null)
                return 0;
            var Pos = Camera.main.WorldToScreenPoint(WorldPos);
            float y = Pos.y / Screen.height;
            return y;
        }
        // 世界坐标转换到屏幕坐标
        public static Vector2 PosScreen(Vector3 WorldPos)
        {
            if (Camera.main == null)
                return Vector2.zero;
            var Pos = Camera.main.WorldToScreenPoint(WorldPos);
            Vector2 retPos = new Vector2(Pos.x, Pos.y);
            return retPos;
        }
        public static Vector2 ScreenToWorld(Vector2 screenPos)
        {
            if (Camera.main == null)
                return Vector2.zero;
            var Pos = Camera.main.ScreenToWorldPoint(screenPos, Camera.MonoOrStereoscopicEye.Mono);
            return Pos;
        }
        #endregion

        #region for
        public static void For(int count,Callback<int> action)
        {
            for (int i = 0; i < count; ++i)
            {
                action?.Invoke(i);
            }
        }
        public static void Foreach<T>(IEnumerable<T> data, Callback<int,T> action)
        {
            int index = 0;
            foreach (var item in data)
            {
                action?.Invoke(index,item);
                index++;
            }
        }
        #endregion

        #region time
        public static long GetTimestamp()
        {
            return DateTime.Now.Ticks;
        }
        public static TimeSpan GetTimespan(long startTicks)
        {
            var ret = DateTime.Now.Ticks - startTicks;
            if (ret < 0)
                ret = 0;
            return TimeSpan.FromTicks(ret);
        }
        #endregion

        #region other
        public static byte[] IntToBytes(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        #endregion
    }
}
