namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Interfaces;
    using ICSSoft.STORMNET.Security;
    using Microsoft.AspNetCore.Http;
    using NewPlatform.Flexberry.Caching;
    using NewPlatform.Flexberry.ORM.CurrentUserService;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read.Excel;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Http;
    using NewPlatform.Flexberry.Reports.ExportToExcel;
    using NewPlatform.Flexberry.Security;
    using Unity;
    using Unity.Injection;

    /// <summary>
    /// This static class is designed to register dependencies in the Unity container.
    /// Created to eliminate code duplication for different dotnet versions.
    /// </summary>
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
#endif
            unityContainer.RegisterType<IAuditService, AuditService>(
                new InjectionConstructor(unityContainer.Resolve<ICurrentUser>()));

            unityContainer.RegisterSingleton<IExportService, ExportExcelODataService>("Export");
            unityContainer.RegisterSingleton<IODataExportService, ExportExcel>();
            unityContainer.RegisterSingleton<ISpreadsheetCustomizer, SpreadsheetCustomizer>();
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
