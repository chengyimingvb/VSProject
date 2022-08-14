using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CYM
{
    public static class ExtensionUI
    {
        #region Digital
        public static string D1(this float? f, bool isOption = false) => BaseUIUtil.D1(f,isOption);
        public static string D2(this float? f, bool isOption = false) => BaseUIUtil.D2(f,isOption);
        public static string DS2(this float? f) => BaseUIUtil.DS2(f);
        public static string DC2(this float? f, bool isReverseCol = false) => BaseUIUtil.DC2(f,isReverseCol);
        public static string DCS2(this float? f, bool isReverseCol = false) => BaseUIUtil.DCS2(f,isReverseCol);
        public static string DS1(this float? f) => BaseUIUtil.DS1(f);
        public static string DC1(this float? f, bool isReverseCol = false) => BaseUIUtil.DC1(f,isReverseCol);
        public static string DCS1(this float? f, bool isReverseCol = false) => BaseUIUtil.DCS1(f,isReverseCol);

        public static string D1(this float f, bool isOption = false) => BaseUIUtil.D1(f, isOption);
        public static string D2(this float f, bool isOption = false) => BaseUIUtil.D2(f, isOption);
        public static string DS2(this float f) => BaseUIUtil.DS2(f);
        public static string DC2(this float f, bool isReverseCol = false) => BaseUIUtil.DC2(f, isReverseCol);
        public static string DCS2(this float f, bool isReverseCol = false) => BaseUIUtil.DCS2(f, isReverseCol);
        public static string DS1(this float f) => BaseUIUtil.DS1(f);
        public static string DC1(this float f, bool isReverseCol = false) => BaseUIUtil.DC1(f, isReverseCol);
        public static string DCS1(this float f, bool isReverseCol = false) => BaseUIUtil.DCS1(f, isReverseCol);
        #endregion

        #region KMG
        public static string KMG(this float? number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number,type);
        public static string KMGC(this float? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number,reverseColor,type);
        public static string KMGCS(this float? number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number,reverseColor,type);

        public static string KMG(this float number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number, type);
        public static string KMGC(this float number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number, reverseColor, type);
        public static string KMGCS(this float number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number, reverseColor, type);
        #endregion

        #region 百分比
        public static string PerS(this int? percent) => BaseUIUtil.PerS(percent);
        public static string PerI(this float? percent, bool isHaveSignal = true) => BaseUIUtil.PerI(percent,isHaveSignal);
        public static string PerCI(this float? percent, bool isHaveSignal = true) => BaseUIUtil.PerCI(percent,isHaveSignal);
        public static string Per(this float? percent) => BaseUIUtil.Per(percent);
        public static string PerS(this float? percent) => BaseUIUtil.PerS(percent);
        public static string PerC(this float? percent, bool reverseColor = false) => BaseUIUtil.PerC(percent,reverseColor);
        public static string PerCS(this float? percent, bool reverseColor = false) => BaseUIUtil.PerCS(percent,reverseColor);

        public static string PerS(this int percent) => BaseUIUtil.PerS(percent);
        public static string PerI(this float percent, bool isHaveSignal = true) => BaseUIUtil.PerI(percent, isHaveSignal);
        public static string PerCI(this float percent, bool isHaveSignal = true) => BaseUIUtil.PerCI(percent, isHaveSignal);
        public static string Per(this float percent) => BaseUIUtil.Per(percent);
        public static string PerS(this float percent) => BaseUIUtil.PerS(percent);
        public static string PerC(this float percent, bool reverseColor = false) => BaseUIUtil.PerC(percent, reverseColor);
        public static string PerCS(this float percent, bool reverseColor = false) => BaseUIUtil.PerCS(percent, reverseColor);
        #endregion

        #region UIColor
        //国家颜色
        public static string Nation(this string name) => BaseUIUtil.Nation(name);
        //城市颜色
        public static string Castle(this string name) => BaseUIUtil.Castle(name);
        //宗教
        public static string Religion(this string name) => BaseUIUtil.Religion(name);
        //贸易
        public static string TradeRes(this string name) => BaseUIUtil.TradeRes(name);
        //黄色
        public static string Yellow(this string name) => BaseUIUtil.Yellow(name);
        public static string Yellow(this float number) => BaseUIUtil.Yellow(number);
        //红色
        public static string Red(this string name) => BaseUIUtil.Red(name);
        public static string Red(this float number) => BaseUIUtil.Red(number);
        //绿色
        public static string Green(this string name) => BaseUIUtil.Green(name);
        public static string Green(this float number) => BaseUIUtil.Green(number);
        //灰色
        public static string Grey(this string name) => BaseUIUtil.Grey(name);
        public static string Grey(this float number) => BaseUIUtil.Grey(number);
        #endregion
    }
}
