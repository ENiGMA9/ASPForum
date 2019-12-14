using ASPForum.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ASPForum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.SetInitializer<CategoryDBContext>(new DropCreateDatabaseIfModelChanges<CategoryDBContext>());
            Database.SetInitializer<SubjectDBContext>(new DropCreateDatabaseIfModelChanges<SubjectDBContext>());
            Database.SetInitializer<ThreadDBContext>(new DropCreateDatabaseIfModelChanges<ThreadDBContext>());
        }
    }
}
