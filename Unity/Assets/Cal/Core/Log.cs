using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ET
{
    public static class Log
    {
        [Conditional("LOG_ENABLE")]
        public static void Trace(string msg)
        {
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
#if UNITY
            UnityEngine.Debug.Log(msg);
#else
            Console.WriteLine($"[Trace]:{msg}");
#endif
        }
        [Conditional("LOG_ENABLE")]
        public static void Trace(string msg, params object[] args)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogFormat(msg, args);
#else
            Console.WriteLine($"[Trace]:{string.Format(msg, args)}");
#endif
        }

        [Conditional("LOG_ENABLE")]
        public static void Debug(string msg)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.Log(msg);
#else
            Console.WriteLine($"[Debug]:{msg}");
#endif
        }

        [Conditional("LOG_ENABLE")]
        public static void Debug(string msg, params object[] args)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogFormat(msg, args);
#else
            Console.WriteLine($"[Debug]:{string.Format(msg, args)}");
#endif
        }
        [Conditional("LOG_ENABLE")]
        public static void Info(string msg)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.Log(msg);
#else
            Console.WriteLine($"[Info]:{msg}");
#endif
        }
        [Conditional("LOG_ENABLE")]
        public static void Info(string msg, params object[] args)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogFormat(msg, args);
#else
            Console.WriteLine($"[Info]:{string.Format(msg, args)}");
#endif
        }


        [Conditional("LOG_ENABLE")]
        public static void Warning(string msg)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogWarning(msg);
#else
            Console.WriteLine($"[Warning]:{msg}");
#endif
        }

        [Conditional("LOG_ENABLE")]
        public static void Warning(string msg, params object[] args)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogWarningFormat(msg, args);
#else
            Console.WriteLine($"[Warning]:{string.Format(msg, args)}");
#endif
        }
        [Conditional("LOG_ENABLE")]
        public static void Error(string msg)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogError(msg);
#else
            Console.WriteLine($"[Error]:{msg}");
#endif
        }

        [Conditional("LOG_ENABLE")]
        public static void Error(Exception e)
        {
#if UNITY
            UnityEngine.Debug.LogException(e);
#else
            Console.WriteLine($"[Error]:{e}");
#endif
        }


        [Conditional("LOG_ENABLE")]
        public static void Error(string msg, params object[] args)
        {
#if UNITY
            //msg = $"{DateTime.Now:mm:ss:FFF} {msg}";
            UnityEngine.Debug.LogErrorFormat(msg, args);
#else
            Console.WriteLine($"[Error]:{string.Format(msg, args)}");
#endif
        }

        [Conditional("LOG_ENABLE")]
        public static void Msg(object msg)
        {
#if UNITY
            Debug(Dumper.DumpAsString(msg));
#else
            Console.WriteLine($"[Msg]:{msg}");
#endif
        }
    }
}
