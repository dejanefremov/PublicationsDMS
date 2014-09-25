using System.Web;
using System.Web.Optimization;

namespace PublicationsDMS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-local-storage.min.js",
                        "~/Scripts/angular-loading-bar.js"));

            bundles.Add(new ScriptBundle("~/bundles/publication").Include(
                "~/AngularJS/publication.main.js", // must be first

                // Helpers
                "~/AngularJS/Helpers/authorizationInterceptor.js",
                "~/AngularJS/Helpers/config.js",
                
                // Services
                "~/AngularJS/Services/Authentication.service.js",
                "~/AngularJS/Services/Document.service.js",
                "~/AngularJS/Services/Folder.service.js",
                "~/AngularJS/Services/Node.service.js",
                "~/AngularJS/Services/ShareNode.service.js",
                "~/AngularJS/Services/User.service.js",

                // Controllers
                "~/AngularJS/App/Node/List/NodeList.controller.js",
                "~/AngularJS/App/Node/PublicList/PublicNodeList.controller.js",
                "~/AngularJS/App/Node/Share/NodeShare.controller.js",

                "~/AngularJS/App/Folder/Edit/FolderEdit.controller.js",
                "~/AngularJS/App/Document/Edit/DocumentEdit.controller.js",
                "~/AngularJS/App/Document/Details/DocumentDetails.controller.js",
                "~/AngularJS/App/Document/Search/DocumentSearch.controller.js",
                "~/AngularJS/App/Document/Upload/DocumentUpload.controller.js",

                "~/AngularJS/App/User/List/UserList.controller.js",
                "~/AngularJS/App/User/Add/UserAdd.controller.js",
                "~/AngularJS/App/User/Edit/UserEdit.controller.js",
                "~/AngularJS/App/User/ChangePassword/ChangePassword.controller.js",

                "~/AngularJS/App/User/Login/Login.controller.js"
                ));

            //jQuery required for bootstrap
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.9.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/BootstrapScript").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/app.css",
                "~/Content/angular-loading-bar.css"));
        }
    }
}