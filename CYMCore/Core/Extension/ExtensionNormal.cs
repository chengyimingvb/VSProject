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
    public static class ExtensionNormal
    {
        public static Vector3 SetX(this Vector3 pos, float x)
        {
            pos = new Vector3(x, pos.y, pos.z);
            return pos;
        }
        public static Vector3 SetY(this Vector3 pos, float y)
        {
            pos = new Vector3(pos.x, y, pos.z);
            return pos;
        }
        public static Vector3 SetZ(this Vector3 pos, float z)
        {
            pos = new Vector3(pos.x, pos.y, z);
            return pos;
        }

        public static Vector3 SetXZ(this Vector3 pos, float x, float z)
        {
            pos = new Vector3(x, pos.y, z);
            return pos;
        }

        public static Vector3 SetXY(this Vector3 pos, float x, float y)
        {
            pos = new Vector3(x, y, pos.z);
            return pos;
        }

        public static Vector3 SetYZ(this Vector3 pos, float y, float z)
        {
            pos = new Vector3(pos.x, y, z);
            return pos;
        }
    }
}