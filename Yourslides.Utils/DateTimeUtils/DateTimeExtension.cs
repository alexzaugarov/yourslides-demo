using System;

namespace Yourslides.Utils.DateTimeUtils {
    public static class DateTimeExtension {
        public static System.DateTime RoundUp(this System.DateTime dt, TimeSpan d) {
            return new System.DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        } 
    }
}