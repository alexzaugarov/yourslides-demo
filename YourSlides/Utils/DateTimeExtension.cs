using System;

namespace CollectStatus.Utils {
    public static class DateTimeExtension {
        public static DateTime RoundUp(this DateTime dt, TimeSpan d) {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        } 
    }
}