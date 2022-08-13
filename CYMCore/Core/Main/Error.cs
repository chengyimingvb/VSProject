using UnityEngine;

namespace CYM
{
    public class Error : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();
            GUILayout.Space(15);
            GUILayout.Label("Powered by CYM");
            GUILayout.Label("未知错误，缺少重要文件！！");
            GUILayout.Label("QQ群:460957512");
            GUILayout.Label("QQ:974057288");
            GUILayout.Label("邮箱:as8506@qq.com");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
