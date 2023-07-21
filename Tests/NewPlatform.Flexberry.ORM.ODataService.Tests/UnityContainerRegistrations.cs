namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Interfaces;
    using ICSSoft.STORMNET.Security;
    using Microsoft.AspNetCore.Http;
#if NETCOREAPP
    using Microsoft.AspNetCore.Mvc.Infrastructure;
#endif
    using NewPlatform.Flexberry.Caching;
    using NewPlatform.Flexberry.ORM.CurrentUserService;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read.Excel;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Http;
    using NewPlatform.Flexberry.Reports.ExportToExcel;
    using NewPlatform.Flexberry.Security;
    using Unity;
    using Unity.Injection;

    /// <summary>
    /// This is just a helper class for tests which is designed to register dependencies in the Unity container.
    /// Created to eliminate code duplication for different dotnet versions.
    /// </summary>
    /// <remarks>
    /// When removing the UnityConfigResolver for dotnet 4.5 version, you need to determine if this class is needed.
    /// (Now it's necessary to re-register because of this class).
    /// Also, the mention "UnityConfigResolver" is neccessary so that when deleting this class, the search will stumble upon this note.
    /// </remarks>
    public static class UnityContainerRegistrations
    {
        /// <summary>
        /// Method for base dependencies registration into Unity container.
        /// </summary>
        /// <param name="unityContainer">Unity container.</param>
        public static void Registration(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IHttpContextAccessor, HttpContextAccessor>();
#if NETFRAMEWORK
            unityContainer.RegisterType<ICurrentUser, WebHttpUser>(
                new InjectionConstructor(unityContainer.Resolve<IHttpContextAccessor>()));
#endif
#if NETCOREAPP
            unityContainer.RegisterType<ICurrentUser, WebHttpUser>();
            unityContainer.RegisterType<IActionContextAccessor, ActionContextAccessor>();
#endif
            unityContainer.RegisterType<IAuditService, AuditService>(
                new InjectionConstructor(unityContainer.Resolve<ICurrentUser>()));

            unityContainer.RegisterSingleton<IODataExportService, ExportExcel>();
            unityContainer.RegisterSingleton<IConfigResolver, ConfigResolver>();

            unityContainer.RegisterSingleton<ICacheService, MemoryCacheService>(
                new InjectionConstructor("defaultCacheForApplication", 3600));

            unityContainer.RegisterSingleton<ISecurityManager, EmptySecurityManager>("securityManagerWithoutRightsCheck");

            unityContainer.RegisterSingleton<IDataService, MSSQLDataService>(
                "dataServiceForAuditAgentManagerAdapter",
                new InjectionConstructor(
                    unityContainer.Resolve<ISecurityManager>("securityManagerWithoutRightsCheck"),
                    unityContainer.Resolve<IAuditService>(),
                    unityContainer.Resolve<IBusinessServerProvider>()),
                new InjectionProperty(nameof(MSSQLDataService.CustomizationStringName), "DefConnStr"));

            unityContainer.RegisterType<IDataService, MSSQLDataService>(
               "dataServiceForSecurityManager",
               new InjectionConstructor(
                   unityContainer.Resolve<ISecurityManager>("securityManagerWithoutRightsCheck"),
                   unityContainer.Resolve<IAuditService>(),
                   unityContainer.Resolve<IBusinessServerProvider>()),
               Inject.Property(nameof(MSSQLDataService.CustomizationStringName), "DefConnStr"));

            unityContainer.RegisterSingleton<ICacheService, MemoryCacheService>(
                "cacheServiceForSecurityManager",
                new InjectionConstructor("cacheForSecurityManager"));

            unityContainer.RegisterSingleton<ICacheService, MemoryCacheService>(
                "cacheServiceForAgentManager", new InjectionConstructor("cacheForAgentManager"));

            unityContainer.RegisterSingleton<ISecurityManager, SecurityManager>(
                new InjectionConstructor(
                    unityContainer.Resolve<IDataService>("dataServiceForSecurityManager"),
                    unityContainer.Resolve<ICacheService>("cacheServiceForSecurityManager")));

            unityContainer.RegisterSingleton<IAgentManager, AgentManager>(
                new InjectionConstructor(
                    unityContainer.Resolve<IDataService>("dataServiceForSecurityManager"),
                    unityContainer.Resolve<ICacheService>("cacheServiceForSecurityManager")));

            unityContainer.RegisterSingleton<IPasswordHasher, Sha1PasswordHasher>();
        }

        /// <summary>
        /// Method for <see cref="IBusinessServerProvider"/> registration into Unity container.
        /// </summary>
        /// <param name="unityContainer">Unity container.</param>
        public static void BSProviderRegistration(IUnityContainer unityContainer)
        {
            unityContainer.RegisterFactory<IBusinessServerProvider>(new Func<IUnityContainer, object>(o => new BusinessServerProvider(new UnityServiceProvider(o))), FactoryLifetime.Singleton);
        }
    }
}
