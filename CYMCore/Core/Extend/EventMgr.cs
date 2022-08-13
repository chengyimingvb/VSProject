//------------------------------------------------------------------------------
// TriggerMgr.cs
// Copyright 2020 2020/6/17 
// Created by CYM on 2020/6/17
// Owner: CYM
// 挂载到指定的单位下面,触发管理器,注册一系列的触发器,然后触发,可以代替一些冷门的Callback
//------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace CYM
{
    [Unobfus]
    public class EventMgr<TKey>
    {
        public Dictionary<TKey, Callback<object>> Data { get; private set; } = new Dictionary<TKey, Callback<object>>();

        #region set
        public void Trigger(TKey key, object obj)
        {
            if (!Data.ContainsKey(key))
            {
                Debug.LogError("MsgDispatcher,不包含的Key:" + key);
                return;
            }
            Data[key].Invoke(obj);
        }
        public void Add(TKey key, Callback<object> callback)
        {
            if (Data.ContainsKey(key))
            {
                Debug.LogError("MsgDispatcher,重复的Key:" + key);
                return;
            }
            Data.Add(key, callback);
        }
        public void Remove(TKey key)
        {
            Data.Remove(key);
        }
        public void Clear()
        {
            Data.Clear();
        }
        #endregion
    }
}