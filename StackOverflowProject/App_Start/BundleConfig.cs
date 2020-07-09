using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StackOverflowProject
{
    public class BundleConfig
    {
        public static void RegiterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.5.1.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/Styles.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}