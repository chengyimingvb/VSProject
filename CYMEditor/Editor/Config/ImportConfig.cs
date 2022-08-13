using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace CYM
{
    [System.Serializable]
    public class TextureImportSettings
    {
        public TextureImportSettings(string direction, SpriteDirRoot rootType)
        {
            PackingTag = dir = direction;
            dirRoot = rootType;
        }
        [SerializeField,HideInInspector]
        private string dir = "";
        [SerializeField, HideInInspector]
        public string PackingTag = "";
        [SerializeField, HideInInspector]
        private SpriteDirRoot dirRoot = SpriteDirRoot.Bundle;
        [SerializeField]
        public bool CrunchedCompression = true;
        [Range(1, 100)][SerializeField]
        public int CompressionQuality = 100;
        [SerializeField]
        public FilterMode FilterMode = FilterMode.Bilinear;
#if UNITY_EDITOR
        [SerializeField]
        public TextureImporterType TextureImporterType = TextureImporterType.Sprite;
        [SerializeField]
        public TextureImporterCompression TextureCompression = TextureImporterCompression.Compressed;
#endif
        public bool IsContainInDirectoryTag(string path)
        {
            HashSet<string> split = new HashSet<string>( path.Split('/'));
            if (dirRoot == SpriteDirRoot.Bundle && !split.Contains(SysConst.Dir_Bundles))
            {
                return false;
            }
            if (dirRoot == SpriteDirRoot.Art && !split.Contains(SysConst.Dir_Art))
            {
                return false;
            }

            bool ret = true;
            if (!split.Contains(dir))
            {
                ret = false;
            }
            return ret;
        }
    }
    [System.Serializable]
    public class AudioImportSettings
    {
        public AudioImportSettings(string direction, AudioClipLoadType loadType)
        {
            dir = direction;
            LoadType = loadType;
        }
        [SerializeField, HideInInspector]
        private string dir = "";
        [SerializeField]
        public AudioClipLoadType LoadType = AudioClipLoadType.DecompressOnLoad;
        [SerializeField]
        public AudioCompressionFormat CompressionFormat = AudioCompressionFormat.Vorbis;
        [Range(0.01f,1)][SerializeField]
        public float Quality = 1f;

        public bool IsContainInDirectoryTag(string path)
        {
            HashSet<string> split = new HashSet<string>(path.Split('/'));
            if (!split.Contains(SysConst.Dir_Bundles))
            {
                return false;
            }
            if (!split.Contains(SysConst.Dir_Art))
            {
                return false;
            }

            bool ret = true;
            if (!split.Contains(dir))
            {
                ret = false;
            }
            return ret;
        }
    }
    [CreateAssetMenu(fileName = "ImportConfig", menuName = "Config/Import", order = 5)]
    public sealed class ImportConfig : ScriptableObjectConfig<ImportConfig>
    {
        [SerializeField] public TextureImportSettings UI = new TextureImportSettings("UI", SpriteDirRoot.Art);
        [SerializeField] public TextureImportSettings Sprite = new TextureImportSettings("Sprite", SpriteDirRoot.Bundle);
        [SerializeField] public TextureImportSettings Icon = new TextureImportSettings("Icon", SpriteDirRoot.Bundle);
        [SerializeField] public TextureImportSettings Head = new TextureImportSettings("Head", SpriteDirRoot.Bundle);
        [SerializeField] public TextureImportSettings Flag = new TextureImportSettings("Flag", SpriteDirRoot.Bundle);
        [SerializeField] public TextureImportSettings Illustration = new TextureImportSettings("Illustration", SpriteDirRoot.Bundle);
        [SerializeField] public TextureImportSettings BG = new TextureImportSettings("BG", SpriteDirRoot.Bundle);
        [SerializeField] public AudioImportSettings Audio = new AudioImportSettings("Audio", AudioClipLoadType.DecompressOnLoad);
        [SerializeField] public AudioImportSettings Music = new AudioImportSettings("Music", AudioClipLoadType.Streaming);
    }
}
