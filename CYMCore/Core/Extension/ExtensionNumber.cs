using UnityEngine;
//**********************************************
// Discription	：Base Core Calss .All the Mono will inherit this class
// Author	：CYM
// Team		：MoBaGame
// Date		：2020-7-16
// Copyright ©1995 [CYMCmmon] Powered By [CYM] Version 1.0.0 
// Desc     ：此代码由陈宜明于2020年编写,版权归陈宜明所有
// Copyright (c) 2020 陈宜明 All rights reserved.
namespace CYM
{
    public static class ExtensionNumber
    {
        #region color
        public static string Nation(this string name) => BaseUIUtil.Nation(name);
        public static string Castle(this string name) => BaseUIUtil.Castle(name);
        public static string Religion(this string name) => BaseUIUtil.Religion(name);
        public static string TradeRes(this string name) => BaseUIUtil.TradeRes(name);
        public static string Red(this float number) => BaseUIUtil.Red(number);
        public static string Red(this string name) => BaseUIUtil.Red(name);
        public static string Yellow(this float number) => BaseUIUtil.Yellow(number);
        public static string Yellow(this string name) => BaseUIUtil.Yellow(name);
        public static string Green(this float number) => BaseUIUtil.Green(number);
        public static string Green(this string name) => BaseUIUtil.Green(name);
        public static string Grey(this float number) => BaseUIUtil.Grey(number);
        public static string Grey(this string name) => BaseUIUtil.Grey(name);
        #endregion

        #region str col
        public static Color ToColor(this string str) => BaseUIUtil.FromHex(str);
        public static string ToHex(this Color color) => BaseUIUtil.ToHex(color);
        public static string Indent(this string str) => BaseUIUtil.Indent(str);
        #endregion

        #region ceil floor
        public static string Floor(this float? f) => BaseUIUtil.Floor(f);
        public static string Ceil(this float? f) => BaseUIUtil.Ceil(f);
        #endregion

        #region per
        public static string PerS(this int? percent) => BaseUIUtil.PerS(percent);
        public static string Per(this float? percent) => BaseUIUtil.Per(percent);
        public static string PerC(this float? percent, bool reverseColor = false) => BaseUIUtil.PerC(percent, reverseColor);
        public static string PerCS(this float? percent, bool reverseColor = false) => BaseUIUtil.PerCS(percent, reverseColor);
        public static string PerS(this float? percent) => BaseUIUtil.PerS(percent);
        public static string PerI(this float? percent, bool isHaveSignal = true) => BaseUIUtil.PerI(percent, isHaveSignal);
        public static string PerCI(this float? percent, bool isHaveSignal = true) => BaseUIUtil.PerCI(percent, isHaveSignal);
        #endregion

        #region kmg
        public static string KMG(this int? number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number, type);
        public static string KMGC(this int? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number, reverseColor, type);
        public static string KMGCS(this int? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number, reverseColor, type);

        public static string KMG(this float? number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number, type);
        public static string KMGC(this float? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number, reverseColor, type);
        public static string KMGCS(this float? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number, reverseColor, type);
        #endregion

        #region col
        public static string CS(this float? number, bool reverseColor = false) => BaseUIUtil.CS(number, reverseColor);
        public static string CS(this int? number) => BaseUIUtil.CS(number);
        #endregion

        #region D
        public static string OneD(this float? f, bool isOption = false) => BaseUIUtil.OneD(f, isOption);
        public static string OneDC(this float? f, bool isReverseCol = false) => BaseUIUtil.OneDC(f, isReverseCol);
        public static string OneDCS(this float? f, bool isReverseCol = false) => BaseUIUtil.OneDCS(f, isReverseCol);
        public static string OneDS(this float? f) => BaseUIUtil.OneDS(f);
        public static string TwoD(this float? f, bool isOption = false) => BaseUIUtil.TwoD(f, isOption);
        public static string TwoDC(this float? f, bool isReverseCol = false) => BaseUIUtil.TwoDC(f, isReverseCol);
        public static string TwoDCS(this float? f, bool isReverseCol = false) => BaseUIUtil.TwoDCS(f, isReverseCol);
        public static string TwoDS(this float? f) => BaseUIUtil.TwoDS(f);
        #endregion

        #region sign
        public static string Sign(this string str) => BaseUIUtil.Sign(str);
        public static string Sign(this float? val, string str) => BaseUIUtil.Sign(val, str);
        public static string Sign(this float? val) => BaseUIUtil.Sign(val);
        #endregion

        #region round
        public static string Round(this float? f) => BaseUIUtil.Round(f);
        public static string RoundC(this float? f) => BaseUIUtil.RoundC(f);
        public static string RoundD(this float? f) => BaseUIUtil.RoundD(f);
        public static string RoundS(this float? f) => BaseUIUtil.RoundS(f);
        #endregion

        #region bool
        public static string Bool(this float? val) => BaseUIUtil.Bool(val);
        public static string Bool(this bool? val) => BaseUIUtil.Bool(val);
        #endregion

        #region special
        public static string RiseCP(this float? num, bool isReverseColor = false, bool isHaveSignal = true) => BaseUIUtil.RiseCP(num, isReverseColor, isHaveSignal);
        public static string DeriseCP(this float? num, bool isHaveSignal = true) => BaseUIUtil.DeriseCP(num, isHaveSignal);
        public static string RiseCI(this float? num, bool isReverseColor = false, int min = 0, int max = 100) => BaseUIUtil.RiseCI(num,isReverseColor,min,max);
        public static string DeriseCI(this float? num, int min = 0, int max = 100) => BaseUIUtil.DeriseCI(num, min, max);
        public static string ApparP(this float? percent, string colorLeft = SysConst.COL_Yellow, string colorRight = SysConst.COL_Green) => BaseUIUtil.ApparP(percent, colorLeft, colorRight);
        public static string CD(this float? f) => BaseUIUtil.CD(f);
        #endregion
    }
}
