using System.Web.Optimization;

namespace YourSlides.Web {
    public class BundleConfig {
        public static void RegisterBundle(BundleCollection bundles) {
            bundles.Add(new Bundle("~/bundles/common_libraries").Include(
                "~/scripts/lib/jquery-{version}.js",
                "~/scripts/lib/moment-with-locale.{version}.js",
                "~/scripts/lib/handlebars.runtime-latest.js"));
        }
    }
}