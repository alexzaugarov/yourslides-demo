using System;

namespace YourSlides.Web.Tools {
    public class Obfuscator {
        public static string Obfuscate(long id) {
            return id.ToString();
        }
        public static long Deobfuscate(string str) {
            try {
                return Int64.Parse(str);
            } catch (Exception) {
                return -1;
            }

        }
    }
}