namespace NewPlatform.Flexberry.ORM.ODataService.Tests.DependencyInjection
{
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Security;

    using Microsoft.Extensions.DependencyInjection;

    using Unity;

    using Xunit;

    /// <summary>
    /// Unity dependecy injection tests.
    /// </summary>
    public class UnityTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="factory">Custom web application factory for tests.</param>
        /// <param name="output">Debug output.</param>
        public UnityTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif
#if NETCOREAPP
        /// <summary>
        /// Resolving a named dependency through extension method of IServiceProvider from ICSSoft.Services.
        /// </summary>
        [Fact]
        public void NamedDependencyResolveTest()
        {
            // Arrange
            var serviceProvider = _factory.Services;
            var unityContainer = serviceProvider.GetService<IUnityContainer>();
            var securityManager = new EmptySecurityManager();

            // Act
            unityContainer.RegisterInstance<ISecurityManager>("named dependency", securityManager);

            // Assert
            var service = serviceProvider.GetService<ISecurityManager>("named dependency");
            Assert.Equal(securityManager, service);
        }

        /// <summary>
        /// Resolving non registered named dependency through extension method of IServiceProvider from ICSSoft.Services.
        /// </summary>
        [Fact]
        public void NamedDependencyNotRegisteredResolveTest()
        {
            // Arrange
            var serviceProvider = _factory.Services;
            var unityContainer = serviceProvider.GetService<IUnityContainer>();

            // Act
            // - none -

            // Assert
            Assert.Throws<ResolutionFailedException>(() => serviceProvider.GetService<ISecurityManager>("non-existing"));
        }

        /// <summary>
        /// Resolving regular (typed) dependency.
        /// We check that our extension method does not break regular resolving.
        /// </summary>
        [Fact]
        public void TypeDependencyResolveTest()
        {
            // Arrange
            var serviceProvider = _factory.Services;
            var unityContainer = serviceProvider.GetService<IUnityContainer>();
            var securityManager = new EmptySecurityManager();

            // Act
            unityContainer.RegisterInstance<ISecurityManager>(securityManager);

            // Assert
            var service = serviceProvider.GetService<ISecurityManager>();
            Assert.Equal(securityManager, service);
        }

        /// <summary>
        /// Registered dependencies are successfully injected into the constructor of a resolved class.
        /// We check that our extension method does not break regular resolving.
        /// </summary>
        [Fact]
        public void TypeDependencyConstructorInjectionTest()
        {
            // Arrange
            var serviceProvider = _factory.Services;
            var unityContainer = serviceProvider.GetService<IUnityContainer>();
            var securityManager = new EmptySecurityManager();

            // Act
            unityContainer.RegisterInstance<ISecurityManager>(securityManager);
            unityContainer.RegisterSingleton<ConstructorInjectionTestObject>();

            // Assert
            var service = serviceProvider.GetService<ConstructorInjectionTestObject>();
            Assert.Equal(securityManager, service.SecurityManager);
        }
#endif

        /// <summary>
        /// For testing that registered dependencies are succesfully injected into a constructor of a resolved class.
        /// </summary>
        internal class ConstructorInjectionTestObject
        {
            /// <summary>
            /// Injected dependency.
            /// </summary>
            internal ISecurityManager SecurityManager { get; set; }

            /// <summary>
            /// A constructor with injected dependency.
            /// </summary>
            /// <param name="securityManager">Injected dependency.</param>
            internal ConstructorInjectionTestObject(ISecurityManager securityManager)
            {
                this.SecurityManager = securityManager;
            }
        }
    }
}
