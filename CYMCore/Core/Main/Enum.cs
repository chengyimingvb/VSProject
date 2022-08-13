using System;

namespace CYM
{
    #region enum
    public enum LevelType
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
    }
    public enum GameDiffType
    {
        Simple,//简单
        Ordinary,//普通
        Difficulty,//困难
        Extremely,//变态
    }
    public enum GamePropType
    {
        Low,//简单
        Middle,//普通
        Hight,//困难
    }
    #endregion

    #region Builder
    public enum VersionTag
    {
        Preview,//预览版本
        Beta,//贝塔版本
        Release,//发行版本
    }

    public enum Platform
    {
        Windows64,
        Android,
        IOS,
    }

    public enum Distribution
    {
        Steam,//Steam平台
        Rail,//腾讯WeGame平台
        Turbo,//多宝平台
        Trial,//试玩平台
        Gaopp,//版署版本
        Usual,//通用版本
    }

    /// <summary>
    /// 发布类型
    /// </summary>
    public enum BuildType
    {
        Develop,//内部开发版本
        Public,//上传发行版本
    }
    #endregion

    #region TD Config
    public enum CloneType
    {
        Memberwise, //浅层拷贝,拷贝所有值字段
        Deep,       //拷贝所有值字段,包括用户自定义的深层拷贝
    }
    #endregion

    #region Mgr
    public enum MgrType
    {
        Global,
        Unit,
        All,
    }
    #endregion

    #region Mono
    // mono 的类型
    public enum MonoType
    {
        None = 0,
        Unit = 1,
        Global = 2,
        View = 4,
        Normal = 8,
    }
    #endregion

    #region Person
    //Person head icon part enum
    public enum PHIPart
    {
        PBare,
        PEye,
        PNose,
        PHair,
        PMouse,
        PBrow,

        PHelmet,
        PBody,
        PBeard,
        PDecorate,
        PBG,
        PFrame,

        PFull,
    }
    //性别
    public enum Gender : int
    {
        Male = 0,//男
        Female,//女
    }
    //年龄阶段
    public enum AgeRange : int
    {
        Child = 0,//1-18
        Adult = 1,//18-30
        Middle = 2,//30-50
        Old = 3,//50-60
    }
    public enum PState
    {
        None, //正常
        Prisoner,//囚犯
        Captive,//俘虏
    }
    #endregion

    #region Season
    public enum SeasonType
    {
        Spring,
        Summer,
        Fall,
        Winter,
    }
    public enum SubSeasonType
    {
        Normal,//正常
        Deep, //嚴冬.盛夏
    }
    #endregion

    #region misc

    public enum SpriteDirRoot
    {
        Bundle,
        Art,
    }

    public enum LogoType
    {
        Image,
        Video,
    }
    public enum FontType
    {
        None = -1,
        Normal = 0,
        Title = 10,
        Dynamic = 100,
    }
    #endregion

    #region Lang
    [Serializable]
    public enum LanguageType : int
    {
        Chinese = 0,
        Traditional = 1,
        English = 2,
        Japanese = 3,
        Spanish = 4,
        Classical = 5,
    }
    #endregion
}
