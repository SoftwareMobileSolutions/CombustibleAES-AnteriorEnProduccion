using System.Web;
using System.Web.Optimization;

namespace CombustibleVales
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Login
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery-{version}.min.js"));
            bundles.Add(new StyleBundle("~/Content/cssLogin").Include(
                     "~/Content/LoginCss/bootstrap.min.css",
                     "~/Content/LoginCss/site.css", "~/Content/LoginCss/materialadmin.css"));
            #endregion

            #region Moment
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"
            ));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            #region SweetAlert2
            bundles.Add(new StyleBundle("~/bundles/sweetAlert2").Include(
                "~/Content/LoginCss/sweetalert.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/sweetAlert2Script").Include(
                "~/Scripts/sweetalert.js", "~/Scripts/sweetalert.min.js"
                ));
            #endregion

            #region "KENDO"
            //JS

            bundles.Add(new ScriptBundle("~/bundles/kendojs").Include(
                       "~/Scripts/kendo/2018.1.221/kendo.all.min.js",
                       "~/Scripts/kendo/2018.1.221/kendo.aspnetmvc.min.js",
                       "~/Scripts/kendo.modernizr.custom.js",
                       "~/Scripts/lenguajeKendoUI/kendo.es-MX.js"
                       ));

            //CSS 
            bundles.Add(new StyleBundle("~/Content/kendoui")
                .Include("~/Content/kendo/2018.1.221/kendo.mobile.all.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2018.1.221/kendo.dataviz.min.css", new CssRewriteUrlTransform())
            );

            ////Estilos
            ////Black
            //bundles.Add(new StyleBundle("~/Content/kendouiblack")
            //   .Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.black.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.dataviz.black.min.css", new CssRewriteUrlTransform())
            //);

            ////BlueOpal
            //bundles.Add(new StyleBundle("~/Content/kendouiblueopal")
            //   .Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.blueopal.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.dataviz.blueopal.min.css", new CssRewriteUrlTransform())
            //);

            ////Bootstrap
            //bundles.Add(new StyleBundle("~/Content/kendouibootstrap")
            //   .Include("~/Content/kendo/2015.2.624/kendo.common-bootstrap.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.bootstrap.min.css", new CssRewriteUrlTransform())
            //   .Include("~/Content/kendo/2015.2.624/kendo.dataviz.bootstrap.min.css", new CssRewriteUrlTransform())
            //);

            ////Default
            //bundles.Add(new StyleBundle("~/Content/kendouidefault")
            // );
            ////Fiori
            //bundles.Add(new StyleBundle("~/Content/kendouifiori")
            // .Include("~/Content/kendo/2015.2.624/kendo.common-fiori.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.fiori.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.dataviz.fiori.min.css", new CssRewriteUrlTransform())
            //);

            ////Flat
            //bundles.Add(new StyleBundle("~/Content/kendouiflat")
            // .Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.flat.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.dataviz.flat.min.css", new CssRewriteUrlTransform())
            //);

            ////HighContrast
            //bundles.Add(new StyleBundle("~/Content/kendouihighcontrast")
            // .Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.highcontrast.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.dataviz.highcontrast.min.css", new CssRewriteUrlTransform())
            //);

            ////Material
            //bundles.Add(new StyleBundle("~/Content/kendouimaterial")
            // .Include("~/Content/kendo/2015.2.624/kendo.common-material.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.material.min.css", new CssRewriteUrlTransform())
            // .Include("~/Content/kendo/2015.2.624/kendo.dataviz.material.min.css", new CssRewriteUrlTransform())
            //);

            ////MaterialBlack
            //bundles.Add(new StyleBundle("~/Content/kendouimaterialblack")
            //.Include("~/Content/kendo/2015.2.624/kendo.common-material.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.materialblack.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.materialblack.min.css", new CssRewriteUrlTransform())
            //);

            ////Metro
            //bundles.Add(new StyleBundle("~/Content/kendouimetro")
            //.Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.metro.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.metro.min.css", new CssRewriteUrlTransform())
            //);

            ////Moonligth
            //bundles.Add(new StyleBundle("~/Content/kendouimoonligth")
            //.Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.moonlight.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.moonlight.min.css", new CssRewriteUrlTransform())
            //);

            ////Office365
            //bundles.Add(new StyleBundle("~/Content/kendouioffice365")
            //.Include("~/Content/kendo/2015.2.624/kendo.common-office365.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.office365.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.office365.min.css", new CssRewriteUrlTransform())
            //);

            ////Silver
            //bundles.Add(new StyleBundle("~/Content/kendouisilver")
            //.Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.silver.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.silver.min.css", new CssRewriteUrlTransform())
            //);

            ////Uniform
            //bundles.Add(new StyleBundle("~/Content/kendouiuniform")
            //.Include("~/Content/kendo/2015.2.624/kendo.common.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.uniform.min.css", new CssRewriteUrlTransform())
            //.Include("~/Content/kendo/2015.2.624/kendo.dataviz.uniform.min.css", new CssRewriteUrlTransform())
            //);

            #endregion
        }
    }
}
