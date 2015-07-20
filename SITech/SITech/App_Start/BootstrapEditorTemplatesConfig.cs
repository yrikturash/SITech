using System.Globalization;
using System.Web.Optimization;

namespace SITech
{
    public class BootstrapEditorTemplatesConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles()
        {
            BundleCollection bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/Scripts/val").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/validation.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Scripts/globalize/globalize.js",
                "~/Scripts/globalize/cultures/globalize.culture." + CultureInfo.CurrentCulture + ".js",
                "~/Scripts/bootstrap*",
                "~/Scripts/filebutton.js",
                "~/Scripts/globalize-datepicker.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/md").Include(
                "~/Scripts/MarkdownDeepLib.min.js",
                "~/Scripts/markdown.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Activator").Include(
                "~/Scripts/Activator.js"
                ));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                "~/Content/themes/bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/GridCss").Include(
                "~/Content/WebGrid.css")
                );

            bundles.Add(new ScriptBundle("~/assets/form_elements").Include(
                "~/Content/assets/js/form_elements.js",
                "~/Content/assets/plugins/bootstrap-select2/select2.min.js"));

            bundles.Add(new ScriptBundle("~/assets/js").Include(
                "~/Content/assets/plugins/pace/pace.min.js",
                "~/Content/assets/plugins/jquery/jquery-2.1.0.min.js",
                "~/Content/assets/plugins/modernizr.custom.js",
                "~/Content/assets/plugins/jquery-ui/jquery-ui.min.js",
                "~/Content/assets/plugins/boostrapv3/js/bootstrap.min.js",
                "~/Content/assets/plugins/jquery/jquery-easy.js",
                "~/Content/assets/plugins/jquery-unveil/jquery.unveil.min.js",
                "~/Content/assets/plugins/jquery-bez/jquery.bez.min.js",
                "~/Content/assets/plugins/jquery-ios-list/jquery.ioslist.min.js",
                "~/Content/assets/plugins/jquery-actual/jquery.actual.min.js",
                "~/Content/assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js",
                "~/Content/assets/plugins/bootstrap-select2/select2.min.js",
                "~/Content/assets/plugins/classie/classie.js",
                "~/Content/assets/plugins/switchery/js/switchery.min.js",
                "~/Content/assets/plugins/bootstrap3-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                "~/Content/assets/plugins/jquery-autonumeric/autoNumeric.js",
                "~/Content/assets/plugins/dropzone/dropzone.min.js",
                "~/Content/assets/plugins/bootstrap-tag/bootstrap-tagsinput.min.js",
                "~/Content/assets/plugins/jquery-inputmask/jquery.inputmask.min.js",
                "~/Content/assets/plugins/boostrap-form-wizard/js/jquery.bootstrap.wizard.min.js",
                "~/Content/assets/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/assets/js/scripts.js",
                "~/Content/assets/js/king-common.js",
                "~/Content/assets/js/deliswitch.js",
                "~/Content/assets/js/plugins/datatable/jquery.dataTables.min.js",
                "~/Content/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js",
                "~/Content/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js",
                "~/Content/assets/js/plugins/datatable/dataTables.tableTools.min.js",
                "~/Content/assets/js/plugins/datatable/dataTables.bootstrap.js",
                "~/Content/assets/js/plugins/jqgrid/jquery.jqGrid.min.js",
                "~/Content/assets/js/plugins/jqgrid/i18n/grid.locale-en.js",
                "~/Content/assets/js/plugins/jqgrid/jquery.jqGrid.fluid.js",
                "~/Content/assets/js/plugins/bootstrap-datepicker/bootstrap-datepicker.js",
                "~/Content/assets/js/king-table.js",
                "~/Content/assets/js/king-table.min.js",
                "~/Content/assets/plugins/summernote/js/summernote.min.js",
                "~/Content/assets/plugins/moment/moment.min.js",
                "~/Content/assets/js/form_elements.js",
                "~/Content/pages/js/pages.min.js"
                ));

            bundles.Add(new ScriptBundle("~/login/js").Include(
                "~/Content/assets/js/jquery/jquery-2.1.0.min.js",
                "~/Content/assets/js/bootstrap/bootstrap.js",
                "~/Content/assets/js/plugins/modernizr/modernizr.js"
                ));

            bundles.Add(new StyleBundle("~/assets/css").Include(
                "~/Content/assets/plugins/pace/pace-theme-flash.css",
                "~/Content/assets/plugins/boostrapv3/css/bootstrap.min.css",
                "~/Content/assets/plugins/css/main.css",
                "~/Content/assets/plugins/css/my-custom-styles.css",
                "~/Content/assets/plugins/font-awesome/css/font-awesome.css",
                "~/Content/assets/plugins/jquery-scrollbar/jquery.scrollbar.css",
                "~/Content/assets/plugins/bootstrap-select2/select2.css",
                "~/Content/assets/plugins/switchery/css/switchery.min.css",
                "~/Content/assets/plugins/bootstrap3-wysihtml5/bootstrap3-wysihtml5.min.css",
                "~/Content/assets/plugins/bootstrap-tag/bootstrap-tagsinput.css",
                "~/Content/assets/plugins/dropzone/css/dropzone.css",
                "~/Content/assets/plugins/bootstrap-datepicker/css/datepicker3.css",
                "~/Content/assets/plugins/summernote/css/summernote.css",
                "~/Content/assets/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Content/assets/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css",
                "~/Content/pages/css/pages-icons.css",
                "~/Content/pages/css/pages.css"
                ));

            bundles.Add(new StyleBundle("~/default/css").Include(
                "~/Content/defaultlayout/css/bootstrap.css",
                "~/Content/defaultlayout/style.css",
                "~/Content/defaultlayout/css/dark.css",
                "~/Content/defaultlayout/css/font-icons.css",
                "~/Content/defaultlayout/css/animate.css",
                "~/Content/defaultlayout/css/magnific-popup.css",
                "~/Content/defaultlayout/css/responsive.css"
                ));

            bundles.Add(new StyleBundle("~/login/css").Include(
                "~/Content/assets/css/bootstrap.min.css",
                "~/Content/assets/css/font-awesome.min.css",
                "~/Content/assets/css/main.css"
                ));


            // By default it will exclude the MarkdownDeepLib.min.js file because of its .min.js extension.
            bundles.IgnoreList.Clear();
            // BundleTable.EnableOptimizations = true;
        }
    }
}