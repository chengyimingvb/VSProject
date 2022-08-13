using UnityEngine;

namespace CYM
{
    public enum KMGType
    {
        OneK,
        TenK,
    }
    public class BaseUIUtil
    {
        const string Yes = "Yes";
        const string No = "No";
        const string ColorFormat = "<color={1}>{0}</color>";

        #region Base
        // 根据sign是否大于0决定一些东西
        // positiveSign:是否给正数写加号
        static string WrapColorSign(string numberStr, float? sign, bool positiveSign, bool reverseColor)
        {
            if (numberStr == null) return "";
            if (sign == null) return "";
            return string.Format("<color={0}>{1}{2}</color>", reverseColor ? GetColor(-sign.Value) : GetColor(sign.Value), positiveSign && sign >= 0 ? "+" : "", numberStr);
        }

        static string GetSign(float? number)
        {
            if (number == null) return "";
            if (number >= 0) return "+";
            else if (number < 0) return "";
            else return "";
        }
        static string GetColor(float? number, bool isReverseCol = false)
        {
            if (number == null) return "";
            if (number > 0)
            {
                if (!isReverseCol) return SysConst.COL_Green;
                else return SysConst.COL_Red;
            }
            else if (number < 0)
            {
                if (!isReverseCol) return SysConst.COL_Red;
                else return SysConst.COL_Green;
            }
            else return SysConst.COL_Yellow;
        }
        #endregion

        #region Normal
        public static string CS(int? number) => WrapColorSign(number.ToString(), number, true, false);
        public static string CS(float? number, bool reverseColor = false) => WrapColorSign(number.ToString(), number, true, reverseColor);
        public static string Sign(float? val) => val >= 0 ? "+" + val.ToString() : val.ToString();
        public static string Sign(float? val, string str) => val >= 0 ? "+" + str : str;
        public static string Sign(string str) => !str.StartsWith("-") ? "+" + str : str;
        #endregion

        #region floor & ceil
        public static string Floor(float? f)
        {
            if (f == null) return "";
            return Mathf.FloorToInt(f.Value).ToString();
        }
        public static string Ceil(float? f)
        {
            if (f == null) return "";
            return Mathf.CeilToInt(f.Value).ToString();
        }
        #endregion

        #region bool
        public static string Bool(float? val)
        {
            if (val == null) return "";
            if (val <= 0.0f) return No;
            else return Yes;
        }
        public static string Bool(bool? b)
        {
            if (b == null) return "";
            if (!b.Value) return No;
            else return Yes;
        }
        #endregion

        #region Round
        public static string Round(float? f)
        {
            if (f == null) return "";
            return Mathf.RoundToInt(f.Value).ToString();
        }
        public static string RoundS(float? f)
        {
            if (f == null) return "";
            return GetSign(f) + Round(Mathf.Abs(f.Value));
        }
        public static string RoundC(float? f) => WrapColorSign(Round(f), f, false, false);
        // 小于1的时候显示1位小数,否则返回整数
        public static string RoundD(float? f)
        {
            if (f == null) return "";
            if (f == 0.0f) return Round(f);
            if (f < 1.0f && f > -1.0f) return string.Format("{0:0.0}", f);
            else return Round(f);
        }
        #endregion

        #region Digital
        public static string OneD(float? f, bool isOption = false)
        {
            return OneDFormat();
            string OneDFormat() => isOption ? string.Format("{0:0.#}", f.HasValue ? f : 0) : string.Format("{0:0.0}", f);
        }
        public static string TwoD(float? f, bool isOption = false)
        {
            return TwoDFormat();
            string TwoDFormat() => isOption ? string.Format("{0:0.##}", f) : string.Format("{0:0.00}", f);
        }
        public static string TwoDS(float? f)
        {
            string sign = GetSign(f);
            return string.Format("{1}{0:0.00}", f, sign);
        }
        public static string TwoDC(float? f, bool isReverseCol = false)
        {
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1:0.00}</color>", color, f);
        }
        public static string TwoDCS(float? f, bool isReverseCol = false)
        {
            string sign = GetSign(f);
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1}{2:0.00}</color>", color, sign, f);
        }
        public static string OneDS(float? f)
        {
            string sign = GetSign(f);
            return string.Format("{1}{0:0.0}", f, sign);
        }
        public static string OneDC(float? f, bool isReverseCol = false)
        {
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1:0.0}</color>", color, f);
        }
        public static string OneDCS(float? f, bool isReverseCol = false)
        {
            string sign = GetSign(f);
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1}{2:0.0}</color>", color, sign, f);
        }
        #endregion

        #region KMG
        public static string KMG(float? number, KMGType type = KMGType.TenK)
        {
            if (number == null) return "";
            float f = GetNumber(number.Value);
            return ((int)f) +  GetSuffix(number.Value);//ValidDigit(f, 2) + GetSuffix((int)number);

            float GetNumber(float num)
            {
                float abs = Mathf.Abs(num);

                if (type == KMGType.TenK)
                {
                    if (abs >= 10000000)
                    {
                        return (num / 1000000);
                    }
                    else if (abs >= 10000)
                    {
                        return (num / 1000.0f);
                    }
                }
                else if (type == KMGType.OneK)
                {
                    if (abs >= 1000000)
                    {
                        return (num / 1000000);
                    }
                    else if (abs >= 1000)
                    {
                        return (num / 1000.0f);
                    }
                }
                return num;
            }
            string GetSuffix(float num)
            {
                float abs = Mathf.Abs(num);
                if (type == KMGType.TenK)
                {
                    if (abs >= 10000000)
                    {
                        return "M";
                    }
                    else if (abs >= 10000)
                    {
                        return "K";
                    }
                }
                else if (type == KMGType.OneK)
                {
                    if (abs >= 1000000)
                    {
                        return "M";
                    }
                    else if (abs >= 1000)
                    {
                        return "K";
                    }
                }
                return "";
            }
            //string ValidDigit(float val, int digit)
            //{
            //    if (digit <= 0) throw new ArgumentOutOfRangeException();
            //    string e = string.Format("{0:e" + (digit - 1) + "}", val);
            //    float fd = float.Parse(e);
            //    return fd.ToString();
            //}
        }
        public static string KMGC(float? number, bool reverseColor = false, KMGType type = KMGType.TenK)
        {
            return WrapColorSign(KMG((int)number, type), number, false, reverseColor);
        }
        public static string KMGCS(float? number, bool reverseColor = false, KMGType type = KMGType.TenK)
        {
            return WrapColorSign(KMG((int)number, type), number, true, reverseColor);
        }
        #endregion

        #region 百分比
        // 裁剪小数部分
        public static string PerS(int? percent)
        {
            if (percent == null) return "";
            return string.Format("{0}%", percent.Value);
        }
        //百分号，无小数，I=Integer
        public static string PerI(float? percent, bool isHaveSignal = true)
        {
            if (percent == null) return "";
            return string.Format("{0}", (int)(percent.Value * 100)) + (isHaveSignal ? "%" : "");
        }
        //百分号，无小数，C=Color，I=Integer
        public static string PerCI(float? percent, bool isHaveSignal = true)
        {
            if (percent == null) return "";
            return WrapColorSign(string.Format("{0}%", (int)(percent.Value * 100)), percent.Value, isHaveSignal, false);
        }
        // 保留小数部分
        public static string Per(float? percent)
        {
            if (percent == null) return "";
            return string.Format("{0}%", OneD(percent.Value * 100));
        }
        public static string PerS(float? percent)
        {
            if (percent == null) return "";
            return percent >= 0 ? "+" + Per(percent) : Per(percent);
        }
        public static string PerC(float? percent, bool reverseColor = false)
        {
            if (percent == null) return "";
            return WrapColorSign(Per(percent), percent, false, reverseColor);
        }
        public static string PerCS(float? percent, bool reverseColor = false)
        {
            if (percent == null) return "";
            return WrapColorSign(Per(percent), percent, true, reverseColor);
        }
        #endregion

        #region UI Special
        // 1-100数值越大,颜色越红，C=Color，I=Integer
        public static string RiseCI(float? num, bool isReverseColor = false, int min = 0, int max = 100)
        {
            if (num == null) return "";
            int round = Mathf.RoundToInt(num.Value);
            int small = Mathf.RoundToInt((max - min) * 0.3f + min);
            int big = Mathf.RoundToInt((max - min) * 0.7f + min);
            float colorSign = GetRiseColSign(round, small, big);
            if (isReverseColor)
            {
                colorSign = -colorSign;
            }

            return Decorate(round.ToString(), GetColor(colorSign));

            float GetRiseColSign(int num1, int small1, int big1)=> num1 < small1 ? -1 : (big1 < 70 ? 0 : 1);

            string Decorate(string numberStr, string color) => string.Format("<color={0}>{1}</color>", color, numberStr);
        }
        // 1-100数值越小,颜色越红，C=Color，I=Integer
        public static string DeriseCI(float? num, int min = 0, int max = 100)=> RiseCI(num, true, min, max);
        // 0-1%百分数值越小,颜色越红，C=Color，P=Percent
        public static string RiseCP(float? num, bool isReverseColor = false, bool isHaveSignal = true)
        {
            string percentStr = PerI(num, isHaveSignal);
            if (!isReverseColor)
            {
                if (num > 0.7) return Green(percentStr);
                if (num <= 0.7f && num > 0.3f) return Yellow(percentStr);
                if (num <= 0.3f) return Red(percentStr);
            }
            else
            {
                if (num > 0.7) return Red(percentStr);
                if (num <= 0.7f && num > 0.3f) return Yellow(percentStr);
                if (num <= 0.3f) return Green(percentStr);
            }
            return percentStr;
        }
        public static string DeriseCP(float? num, bool isHaveSignal = true)=> RiseCP(num, true, isHaveSignal);
        // 天枰颜色，P=Percent
        public static string ApparP(float? percent, string colorLeft = SysConst.COL_Yellow, string colorRight = SysConst.COL_Green)
        {
            if (percent == null) return "";
            return string.Format("<color={0}>{1}</color>", percent.Value <= 0.0f ? colorLeft : colorRight, Per(Mathf.Abs(percent.Value)));
        }
        //CD时间
        public static string CD(float? f)
        {
            if (f == null) return null;
            if (f > 1.0f) return Round(f);
            return string.Format("{0:0.0}", f);
        }
        #endregion

        #region UIColor
        //国家颜色
        public static string Nation(string name) => string.Format(ColorFormat, name, SysConst.COL_Yellow);
        //城市颜色
        public static string Castle(string name) => string.Format(ColorFormat, name, SysConst.COL_Green);
        //宗教
        public static string Religion(string name) => string.Format(ColorFormat, name, SysConst.COL_Yellow);
        //贸易
        public static string TradeRes(string name) => string.Format(ColorFormat, name, SysConst.COL_Grey);
        //黄色
        public static string Yellow(string name) => string.Format(ColorFormat, name, SysConst.COL_Yellow);
        public static string Yellow(float number) => Yellow(number.ToString());
        //红色
        public static string Red(string name) => string.Format(ColorFormat, name, SysConst.COL_Red);
        public static string Red(float number) => Red(number.ToString());
        //绿色
        public static string Green(string name) => string.Format(ColorFormat, name, SysConst.COL_Green);
        public static string Green(float number) => Green(number);
        //灰色
        public static string Grey(string name) => string.Format(ColorFormat, name, SysConst.COL_Grey);
        public static string Grey(float number) => Grey(number);
        #endregion

        #region Misc
        public static Color FromHex(string str)
        {
            Color color = new Color();
            ColorUtility.TryParseHtmlString(str, out color);
            return color;
        }
        public static string ToHex(Color color)=>ColorUtility.ToHtmlStringRGBA(color);
        public static string Indent(string str)=> SysConst.STR_Indent + str;
        #endregion

        #region presenter
        public static void ResetRectTransform(RectTransform rectTransform)
        {
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        #endregion   
    }
}
