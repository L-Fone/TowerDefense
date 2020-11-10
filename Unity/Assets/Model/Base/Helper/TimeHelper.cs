﻿using System;

namespace ET
{
    public static class TimeHelper
    {
        private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        /// <summary>
        /// 客户端时间
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - epoch) / 10000;
        }

        public static long ClientNowSeconds()
        {
            return (DateTime.UtcNow.Ticks - epoch) / 10000000;
        }

        public static long Now()
        {
            return ClientNow();
        }
        public static DateTime GetUTCTime(long time)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(time).UtcDateTime;
        }
        public static DateTime GetTime(long time)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(time).LocalDateTime;
        }
    }
}