using System;

namespace Yourslides.Utils.DateTimeUtils {
    public class UnixTime {
        public static int Now {
            get {
                return ToUnix(System.DateTime.UtcNow);
            }
        }

        public static int ToUnix(System.DateTime dateTime) {
            return (int)dateTime.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime ToDateTime(int unixTimestamp) {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddSeconds(unixTimestamp).ToUniversalTime();
        }
    }
}