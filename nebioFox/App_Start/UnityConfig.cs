
using Businnes.OFX;
using DataAcessMock;
using Models.Interface.Business;
using Models.Interface.Repository;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace nebioFox.App_Start
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }

        public static void RegistraComponentes()
        {
            var container = new UnityContainer();
            ConsomeBusiness(container);
            DadosProducao(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }


        private static void ConsomeBusiness(UnityContainer container) {
            container.RegisterType(typeof(IBusinessOFX), typeof(OfxBusiness));
        }
        private static void DadosProducao(UnityContainer container) {
            container.RegisterType(typeof(IRepositoryOFX), typeof(DataAcessOFX));
        }


    }
}