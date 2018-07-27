using System.Web;
using System.Web.Optimization;

namespace ProiectPentalog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/rest")
                .Include("~/Scripts/headroom.min.js")
                .Include("~/Scripts/html5shiv.js")
                .Include("~/Scripts/jQuery.headroom.min.js")
                .Include("~/Scripts/template.js")
                .Include("~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/css/bootstrap-theme.css", "~/Content/css/bootstrap.min.css", "~/Content/css/font-awesome.min.css", "~/Content/css/main.css", "~/Content/css/ionicons.min.css", "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/css2")
               .Include("~/Content/css2/slick.css", "~/Content/css2/slick-theme.css", "~/Content/css2/animate.css", "~/Content/css2/iconfont.css", "~/Content/css2/font-awesome.min.css", "~/Content/css2/bootstrap.css", "~/Content/css2/magnific-popup.css", "~/Content/css2/bootsnav.css"));


        }
    }
}
