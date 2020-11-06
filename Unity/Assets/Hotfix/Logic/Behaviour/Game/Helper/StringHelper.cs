using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class StringUtil
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()) || string.IsNullOrWhiteSpace(obj.ToString()))
                return true;
            return false;
        }
    }
}
