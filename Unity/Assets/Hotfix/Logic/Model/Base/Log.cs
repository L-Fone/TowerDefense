//using ET;
//using System;

//namespace ET
//{
//	public static class Log
//	{
//		public static void Trace(string msg)
//		{
//#if UNITY_EDITOR
//            ET.Log.Trace(msg);
//#endif
//        }

//        public static void Warning(string msg)
//        {
//#if UNITY_EDITOR
//            ET.Log.Warning(msg);
//#endif
//        }

//        public static void Info(string msg)
//        {
//#if UNITY_EDITOR
//            ET.Log.Info(msg);
//#endif
//        }

//        public static void Error(Exception e)
//        {
//#if UNITY_EDITOR
//            ET.Log.Error(e);
//#endif
//        }

//        public static void Error(string msg)
//        {
//#if UNITY_EDITOR
//            ET.Log.Error(msg);
//#endif
//		}

//		public static void Debug(string msg)
//        {
//#if UNITY_EDITOR
//            ET.Log.Debug(msg);
//#endif
//        }

//        public static void Trace(string message, params object[] args)
//        {
//#if UNITY_EDITOR
//            ET.Log.Trace(message, args);
//#endif
//        }

//        public static void Warning(string message, params object[] args)
//        {
//#if UNITY_EDITOR
//            ET.Log.Warning(message, args);
//#endif
//        }

//        public static void Info(string message, params object[] args)
//        {
//#if UNITY_EDITOR
//            ET.Log.Info(message, args);
//#endif
//        }

//        public static void Debug(string message, params object[] args)
//        {
//#if UNITY_EDITOR
//            ET.Log.Debug(message, args);
//#endif
//        }

//        public static void Error(string message, params object[] args)
//        {
//#if UNITY_EDITOR
//            ET.Log.Error(message, args);
//#endif
//        }

//        public static void Msg(ushort opcode, object msg)
//        {
//#if UNITY_EDITOR
//            if (opcode != HotfixOpcode1.Frame_ClickMap
//                   && opcode != HotfixOpcode1.M2C_PathfindingResult
//                 && opcode != HotfixOpcode1.C2G_HeartBeat
//                  && opcode != HotfixOpcode1.G2C_HeartBeat
//                   )
//                Debug(MongoHelper.ToJson(msg));
//#endif
//        }
//    }
//}