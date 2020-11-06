using FairyGUI;
using System.Collections.Generic;

namespace ET
{
    public static class GObjectHelper
    {
        private static Dictionary<GObject, FUI> _keyValuePairs = new Dictionary<GObject, FUI>();

        public static T Get<T>(this GObject self) where T : FUI
        {
            if (self != null && _keyValuePairs.TryGetValue(self,out var ui))
            {
                return ui as T;
            }

            return default;
        }

        public static void Add(this GObject self, FUI fui)
        {
            if (self != null && fui != null)
            {
                _keyValuePairs[self] = fui;
            }
        }

        public static FUI Remove(this GObject self)
        {
            if (self != null && _keyValuePairs.TryGetValue(self, out var ui))
            {
                _keyValuePairs.Remove(self);
                return ui;
            }

            return null;
        }
    }
}