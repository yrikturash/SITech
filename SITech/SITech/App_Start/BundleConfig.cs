using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web;
using System.Web.Optimization;

namespace SITech
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.UseCdn = true;
            var cssTransformer = new CssTransformer();
            var jsTransformer = new JsTransformer();
            var nullOrderer = new NullOrderer();

            var cssBundle = new CustomStyleBundle("~/bundles/css");
            cssBundle.Include(
                "~/Content/Site.less", 
                "~/Content/bootstrap.css");
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var jqueryBundle = new CustomScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/jquery-{version}.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            var bootbox = new CustomScriptBundle("~/bundles/bootbox");
            bootbox.Include("~/Scripts/bootbox.min.js");
            bootbox.Transforms.Add(jsTransformer);
            bootbox.Orderer = nullOrderer;
            bundles.Add(bootbox);

            var bootboxPushups = new CustomScriptBundle("~/bundles/bootbox_pushups");
            bootboxPushups.Include("~/Scripts/pushups.js");
            bootboxPushups.Transforms.Add(jsTransformer);
            bootboxPushups.Orderer = nullOrderer;
            bundles.Add(bootboxPushups);

            var jqueryvalBundle = new CustomScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/Scripts/jquery.validate*");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new CustomScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/Scripts/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);


            var bootstrapBundle = new CustomScriptBundle("~/bundles/bootstrap");
            bootstrapBundle.Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js");
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);


        }
    }
}
