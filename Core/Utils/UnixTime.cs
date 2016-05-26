using System;

namespace Core.Utils {
    public class UnixTime {
        public static int Now {
            get {
                return ToUnix(DateTime.Now);
            }
        }
        public static int UtcNow {
            get {
                return ToUnix(DateTime.UtcNow);
            }
        }

        public static int ToUnix(DateTime dateTime) {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}