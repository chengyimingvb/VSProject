//------------------------------------------------------------------------------
// AssetBundleConfig.cs
// Copyright 2018 2018/5/18 
// Created by CYM on 2018/5/18
// Owner: CYM
// 填写类的描述...
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;
namespace CYM
{
    public class DLCItemConfig
    {
        public string Name;
        public bool IsActive = true;
        public int Version = 0;
        public DLCItemConfig(string name)
        {
            Name = name;
        }
    }
    [CreateAssetMenu(fileName = "DLCConfig", menuName = "Config/DLC", order = 3)]
    public sealed class DLCConfig : ScriptableObjectConfig<DLCConfig>
    {
        #region private inspector
        [SerializeField,ReadOnly,HideInInspector] 
        List<BuildRuleConfig> BuildRule = new List<BuildRuleConfig>();
        [SerializeField, ReadOnly, HideInInspector]
        List<string> IgnoreConst = new List<string>();
        [SerializeField]
        List<DLCItemConfig> DLC = new List<DLCItemConfig>();
        #endregion

        #region dlc config
        //内部DLC
        public DLCItemConfig ConfigInternal { get; private set; }
        //默认DLC
        public DLCItemConfig ConfigNative { get; private set; }
        //扩展DLC 不包含 Native
        public List<DLCItemConfig> ConfigExtend { get; private set; } = new List<DLCItemConfig>();
        public List<DLCItemConfig> ConfigAll { get; private set; } = new List<DLCItemConfig>();
        #endregion

        #region editor dlc item
        public DLCItem EditorInternal { get; private set; }
        public DLCItem EditorNative { get; private set; }
        public List<DLCItem> EditorExtend { get; private set; } = new List<DLCItem>();
        public List<DLCItem> EditorAll { get; private set; } = new List<DLCItem>();
        public List<DLCItem> EditorInner { get; private set; } = new List<DLCItem>();
        #endregion

        #region runtime
        public HashSet<string> AllDirectory { get; private set; } = new HashSet<string>();
        public List<BuildRuleConfig> Config { get; private set; } = new List<BuildRuleConfig>();
        public List<string> CopyDirectory { get; private set; } = new List<string>();
        public HashSet<string> IgnoreConstSet { get; private set; } = new HashSet<string>();
        #endregion

        #region life
        public override void OnCreate()
        {
            base.OnCreate();
            RecreateDLC();
        }
        public override void OnInited()
        {
            base.OnInited();
            RefreshDLC();
        }
        public void RecreateDLC()
        {
            BuildRule.Clear();
            CopyDirectory.Clear();
            IgnoreConst.Clear();
            //配置资源
            BuildRule.Add(new BuildRuleConfig("Config", BuildRuleType.Directroy, true));
            BuildRule.Add(new BuildRuleConfig("Lua", BuildRuleType.Directroy, true));
            BuildRule.Add(new BuildRuleConfig("Language", BuildRuleType.Directroy, true));
            BuildRule.Add(new BuildRuleConfig("Excel", BuildRuleType.Directroy, true));
            BuildRule.Add(new BuildRuleConfig("Text", BuildRuleType.Directroy, true));
            BuildRule.Add(new BuildRuleConfig("CSharp", BuildRuleType.Directroy, true));
            //图片资源
            BuildRule.Add(new BuildRuleConfig("Sprite", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("BG", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Icon", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Head", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Flag", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Texture", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Illustration", BuildRuleType.Directroy));
            //其他资源
            BuildRule.Add(new BuildRuleConfig("Audio", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("AudioMixer", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Material", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Music", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("PhysicsMaterial", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Video", BuildRuleType.Directroy));
            //Prefab资源
            BuildRule.Add(new BuildRuleConfig("Animator", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Prefab", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("Perform", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("System", BuildRuleType.Directroy));
            BuildRule.Add(new BuildRuleConfig("UI", BuildRuleType.Directroy));
            //场景资源
            BuildRule.Add(new BuildRuleConfig("Scene", BuildRuleType.File));
            //忽略的Const
            IgnoreConst.Add("CONFIG_DLCItem");
            IgnoreConst.Add("CONFIG_DLCManifest");
        }
        //刷新DLC
        public void RefreshDLC()
        {
            Config.Clear();
            AllDirectory.Clear();
            CopyDirectory.Clear();
            IgnoreConstSet.Clear();

            ConfigExtend.Clear();
            ConfigAll.Clear();

            EditorExtend.Clear();
            EditorAll.Clear();
            EditorInner.Clear();

            foreach (var item in BuildRule)
                AddBuildConfig(item);
            foreach (var item in IgnoreConst)
                AddIgnoreConst(item);

            //加载内部dlc
            ConfigInternal = new DLCItemConfig(SysConst.STR_InternalDLC);
            EditorInternal = new DLCItem(ConfigInternal);

            //加载原生dlc
            ConfigNative = new DLCItemConfig(SysConst.STR_NativeDLC);
            EditorNative = new DLCItem(ConfigNative);

            //加载其他额外DLC
            foreach (var item in DLC)
            {
                var dlcItem = new DLCItemConfig(item.Name);
                ConfigExtend.Add(dlcItem);
                EditorExtend.Add(new DLCItem(dlcItem));
            }

            ConfigAll = new List<DLCItemConfig>(ConfigExtend);
            ConfigAll.Add(ConfigInternal);
            ConfigAll.Add(ConfigNative);

            EditorAll = new List<DLCItem>(EditorExtend);
            EditorAll.Add(EditorInternal);
            EditorAll.Add(EditorNative);

            EditorInner.Add(EditorInternal);
            EditorInner.Add(EditorNative);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                foreach (var item in AllDirectory)
                {
                    BaseFileUtil.EnsureDirectory(Path.Combine(SysConst.Path_Bundles, item));
                }

                foreach (var item in DLC)
                {
                    foreach (var dic in AllDirectory)
                    {
                        BaseFileUtil.EnsureDirectory(Path.Combine(SysConst.Path_Dlc, item.Name, dic));
                    }
                }
            }
#endif
        }

        void AddBuildConfig(BuildRuleConfig config)
        {
            BuildRuleConfig data = config.Clone() as BuildRuleConfig;
            Config.Add(data);
            if (data.IsCopyDirectory)
            {
                CopyDirectory.Add(data.Name);
            }
            if (!AllDirectory.Contains(data.Name))
                AllDirectory.Add(data.Name);
        }
        void AddIgnoreConst(string name)
        {
            if (!IgnoreConstSet.Contains(name))
                IgnoreConstSet.Add(name);
        }
        public bool IsInIgnoreConst(string name)
        {
            return IgnoreConstSet.Contains(name);
        }
        #endregion


        public void RemoveDLC(string name)
        {
            DLC.RemoveAll(x=>x.Name == name);
            RefreshDLC();
        }
        public void AddDLC(string name)
        {
            DLC.Add(new DLCItemConfig(name));
            RefreshDLC();
        }
    }
}