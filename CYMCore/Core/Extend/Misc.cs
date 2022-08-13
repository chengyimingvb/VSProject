using System;
using UnityEngine;

namespace CYM
{
    [Serializable]
    public class SLGWeatherConfigData
    {
        public float Prob = 1.0f;
        public GameObject Prefab;
    }

    [Serializable]
    public class SeasonData
    {
        /// <summary>
        /// 太阳关照强度
        /// </summary>
        public float SunIntensity;
        /// <summary>
        /// 积雪百分比
        /// </summary>
        public float AccumulatedSnow;
        /// <summary>
        /// 风力
        /// </summary>
        public float WindzonePower;

    }
}
