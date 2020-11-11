using Cal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public static class MathHelper
    {
        public static int RoundToInt(FP d)
        {
            return (int)Math.Round((double)d, MidpointRounding.AwayFromZero);
        }
        public static long RoundToLong(FP d)
        {
            return (long)Math.Round((double)d, MidpointRounding.AwayFromZero);
        }
        public static bool IsHit(FP p)
        {
            return p > RandomHelper.RandomFloat();
        }
        public static int GetProbabilityIndex(float[] probabilities)
        {
            int count = probabilities.Length;
            FP a = RandomHelper.RandomFloat();
            int index = -1;
            FP add = 0;
            foreach (var item in probabilities)
            {
                ++index;
                if(index+1 == count)
                {
                    return index;
                }
                if (add < a &&
                    a <= add+probabilities[index + 1])
                {
                    return index;
                }
                add += item;
            }
            return 0;
        }
    }
}
