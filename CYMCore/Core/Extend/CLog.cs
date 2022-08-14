using System.Collections.Generic;
using UnityEngine;

namespace CYM
{
    public class CLog
    {
        #region member variable
        const string TagNormal = "Normal";
        public static bool Enable { get; private set; } = true;
        public static LogLevel Level { get; private set; } = LogLevel.Warn;
        public static Dictionary<string, TagInfo> Tags { get; private set; } = new Dictionary<string, TagInfo>();
        public static bool IsLogTime { get; set; } = false;
        #endregion

        #region data class
        public enum LogLevel
        {
            None = -1,
            Debug,
            Info,
            Warn,
            Error
        }

        [System.Serializable]
        public class TagInfo
        {
            public bool Enable;
            public Color Color = Color.black;
            public string Hex { get; private set; } = null;

            public void Init()
            {
                Hex = ColorUtility.ToHtmlStringRGBA(Color);
            }
        }
        #endregion

        #region log methon
        public static void Init(bool isEnable, LogLevel level, Dictionary<string, TagInfo> tags)
        {
            Enable = isEnable;
            Level = level;
            Tags = tags;
        }
        public static void GUILog(Vector3 pos, string format, params object[] objs)
        {
            if (!Enable) return;
            if (Level > LogLevel.Debug) return;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
            if (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
            {
                string str = string.Format(format, objs);
                Vector2 size = GUI.skin.textArea.CalcSize(new GUIContent(str));
                float width = size.x;
                float height = size.y;
                GUI.TextArea(new Rect(screenPos.x - (float)(width / 2), Screen.height - screenPos.y - (float)(height / 2), (float)width, (float)height), str);
            }
        }
        public static void Error(string format, params object[] ps)
        {
            if (!Enable) return;
            if (Level <= LogLevel.Error)
            {
                UnityEngine.Debug.LogError(GetTime() + string.Format(format, ps));
            }
        }
        public static void Info(string format, params object[] ps)
        {
            if (!Enable) return;
            if (Level <= LogLevel.Info)
            {
                UnityEngine.Debug.LogFormat(GetTime() + string.Format("<b>Info:</b>{0}",format), ps);
            }
        }
        public static void Warn(string format, params object[] ps)
        {
            if (!Enable) return;
            if (Level <= LogLevel.Warn)
            {
                UnityEngine.Debug.LogWarningFormat(GetTime() + format, ps);
            }
        }
        public static void Log(string message, params object[] ps)
        {
            if (!Enable) return;
            if (Level <= LogLevel.Debug)
            {
                UnityEngine.Debug.LogFormat(GetTime() + message, ps);
            }
        }
        public static void Debug(string tag, string format, params object[] ps)
        {
            if (!Enable) return;
            if (Level > LogLevel.Debug) return;
            if (tag == TagNormal)
            {
                UnityEngine.Debug.LogFormat(GetTime() + string.Format("<b>Debug:</b>{0}",format), ps);
            }
            else
            {
                if (!IsTagExist(tag)) return;
                if (!Tags[tag].Enable) return;
                if (string.IsNullOrEmpty(Tags[tag].Hex))
                {
                    UnityEngine.Debug.LogFormat(GetTime() + format, ps);
                    return;
                }
                UnityEngine.Debug.LogFormat(GetTime() + string.Format("<b>Debug:</b><color={0}>{1}</color>", Tags[tag].Hex, format), ps);
            }
        }
        public static void Debug(string format, params object[] ps) => Debug(TagNormal, format, ps);
        public static void Green(string format, params object[] ps) => Color("green", format, ps);
        public static void Red(string format, params object[] ps) => Color("red", format, ps);
        public static void Yellow(string format, params object[] ps) => Color("yellow", format, ps);
        public static void Cyan(string format, params object[] ps) => Color("cyan", format, ps);
        public static void Color(string col, string format, params object[] ps)
        {
            if (!Enable) return;
            if (Level > LogLevel.Debug) return;
            UnityEngine.Debug.LogFormat(GetTime() + string.Format("<color={0}>{1}</color>", col, format), ps);
        }
        public static void Editor(string format, params object[] ps)
        {
            if (!Application.isEditor)
                return;
            UnityEngine.Debug.Log(string.Format(format,ps));
        }
        #endregion

        #region is
        static bool IsTagExist(string tag) => Tags.ContainsKey(tag);
        #endregion

        static string GetTime()
        {
            if(IsLogTime)
                return $"[{Time.realtimeSinceStartup.ToString("0.00")}]";
            return "";
        }
    }
}
