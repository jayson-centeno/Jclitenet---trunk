using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace CoreFramework4.Utility
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/bundles/jquery");
            scriptBundle.Include("~/Scripts/jquery-2.1.3.min.js");
            scriptBundle.Include("~/Scripts/jquery-ui-1.8.11.min.js");
            scriptBundle.Include("~/Scripts/jquery.validate.min.js");
            scriptBundle.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            scriptBundle.Include("~/Scripts/jquery.unobtrusive-ajax.min.js");
            scriptBundle.Include("~/Scripts/application.js");
            bundles.Add(scriptBundle);

            var cssBundles = new StyleBundle("~/content/css");
            cssBundles.Include("~/Content/Reset.css");
            cssBundles.Include("~/Content/Site.css");
            bundles.Add(cssBundles);

            var jqueryUiCssBundles = new StyleBundle("~/content/themes/base/css");
            jqueryUiCssBundles.Include("~/Content/themes/base/css")
                              .Include("~/Content/themes/base/jquery.ui.core.css",
                               "~/Content/themes/base/jquery.ui.resizable.css",
                               "~/Content/themes/base/jquery.ui.selectable.css",
                               "~/Content/themes/base/jquery.ui.accordion.css",
                               "~/Content/themes/base/jquery.ui.autocomplete.css",
                               "~/Content/themes/base/jquery.ui.button.css",
                               "~/Content/themes/base/jquery.ui.dialog.css",
                               "~/Content/themes/base/jquery.ui.slider.css",
                               "~/Content/themes/base/jquery.ui.tabs.css",
                               "~/Content/themes/base/jquery.ui.datepicker.css",
                               "~/Content/themes/base/jquery.ui.progressbar.css",
                               "~/Content/themes/base/jquery.ui.theme.css");
            bundles.Add(jqueryUiCssBundles);

            //var fontAwesome = new StyleBundle("~/content/font-awesome/css");
            //fontAwesome.Include("~/Content/font-awesome/css/font-awesome.min.css");
            //bundles.Add(fontAwesome);

        }
    }
}
