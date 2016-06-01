using System;

namespace YourSlides.Tools {
    public class Obfuscator {
        public static string Obfuscate(long id) {
            return id.ToString();
        }
        public static long Deobfucate(string str) {
            return Int64.Parse(str);
        }
    }
}