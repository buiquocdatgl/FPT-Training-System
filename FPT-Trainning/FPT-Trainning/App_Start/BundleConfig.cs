using System.Web;
using System.Web.Optimization;

namespace FPT_Trainning
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/assets/css").Include(
                      "~/assets/bootstrap/css/bootstrap.min.css",
                      "~/assets/css/alert.css",
                      "~/assets/css/dh-navbar-centered-brand.css",
                      "~/assets/css/dh-row.titile-text-image-right-1.css",
                      "~/assets/css/Footer-Dark.css",
                      "~/assets/css/Form.css",
                      "~/assets/css/Pretty-Registration-Form.css",
                      "~/assets/css/Projects-Clean.css",
                      "~/assets/css/Table-with-search--sort-filters.css"
                      )
                );
            bundles.Add(new StyleBundle("~/assets/login").Include(
                "~/assets/bootstrap/css/bootstrap.min.css",
                "~/assets/css/Pretty-Registration-Form.css"));

            bundles.Add(new StyleBundle("~/assets/js").Include(
                 "~/assets/bs-init.js",
                 "~/assets/chart.min.js",
                 "~/assets/jquery.min.js",
                 "~/Table-with-search--sort-filters.js",
                 "~/assets/theme.js"
                ));
        }
    }
}
