using System.Web.Optimization;

namespace FalMedia
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //"~/Scripts/jquery-{version}.js"
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/admin/admin.css"));
            bundles.Add(new StyleBundle("~/Content/admin-rtl").Include(
                "~/Content/admin/admin-rtl.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/main.min.css"));
            bundles.Add(new StyleBundle("~/Content/css-rtl").Include(
                "~/Content/main.min-rtl.css"));
            bundles.Add(new StyleBundle("~/Content/page").Include(
                "~/Content/page.min.css"));
            bundles.Add(new StyleBundle("~/Content/page-rtl").Include(
                "~/Content/page.min-rtl.css"));
        }
    }
}