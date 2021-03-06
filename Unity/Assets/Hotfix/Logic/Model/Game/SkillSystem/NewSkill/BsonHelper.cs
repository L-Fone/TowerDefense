﻿//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年12月10日 12:53:38
//------------------------------------------------------------

using System;
using System.Numerics;
using Cal;
using MongoDB.Bson.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ET
{
    /// <summary>
    /// Bson序列化反序列化辅助类
    /// </summary>
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class BsonHelper
    {
        static BsonHelper()
        {
            Log.Info("执行了BsonHelper初始化");
            RegisterStructSerializer();
        }

        /// <summary>
        /// 注册所有需要使用Bson序列化反序列化的结构体
        /// </summary>
        public static void RegisterStructSerializer()
        {
            BsonSerializer.RegisterSerializer(typeof(FP), new StructBsonSerialize<FP>());
            BsonSerializer.RegisterSerializer(typeof(Vector2), new StructBsonSerialize<Vector2>());
            BsonSerializer.RegisterSerializer(typeof(Vector3), new StructBsonSerialize<Vector3>());
#if UNITY
            BsonSerializer.RegisterSerializer(typeof(UnityEngine.Rect), new StructBsonSerialize<UnityEngine.Rect>());
            //BsonSerializer.RegisterSerializer(typeof(UnityEngine.Vector2), new StructBsonSerialize<UnityEngine.Vector2>());
            BsonSerializer.RegisterSerializer(typeof(UnityEngine.Vector3), new StructBsonSerialize<UnityEngine.Vector3>());
#endif
            BsonSerializer.RegisterSerializer(typeof(ModifierId), new StructBsonSerialize<ModifierId>());
            BsonSerializer.RegisterSerializer(typeof(SkillParam), new StructBsonSerialize<SkillParam>());
        }

        /// <summary>
        /// 初始化BsonHelper
        /// </summary>
        public static void Init()
        {
            //调用这个是为了执行静态构造方法
        }
    }
}