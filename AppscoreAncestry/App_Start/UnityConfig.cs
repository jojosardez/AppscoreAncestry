using AppscoreAncestry.Entities;
using AppscoreAncestry.Infrastructure;
using AppscoreAncestry.Services;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AppscoreAncestry
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterInstance(typeof(IDataStore<Data>),
                new FileDataStore<Data>(GetAbsolutePath(ConfigurationManager.AppSettings["DataFileName"])));
            container.RegisterType<IPersonSearchService, PersonSearchService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static string GetAbsolutePath(string fileName)
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/" + fileName);
        }
    }
}