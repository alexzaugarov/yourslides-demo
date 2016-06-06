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
    }
}