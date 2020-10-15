namespace NewPlatform.Flexberry.ORM.ODataService.WebApi.Controllers
{
    using System;
    using System.Web.Http;

    using NewPlatform.Flexberry.Services;
    using Unity;

    /// <summary>
    /// WebAPI controller for Flexberry Lock Service (<see cref="ILockService"/>).
    /// </summary>
    /// <seealso cref="ApiController" />
    //[Authorize]
    public class LockController : BaseApiController
    {
        private readonly ILockService _lockService;

        public static ICSSoft.STORMNET.Business.IDataService DataService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LockController"/> class.
        /// </summary>
        /// <param name="lockService">The lock service.</param>
        //public LockController(ILockService lockService)
        public LockController()
        {
            //_lockService = lockService ?? throw new ArgumentNullException(nameof(lockService), "Contract assertion not met: lockService != null");
            //_lockService = new LockService(ICSSoft.Services.UnityFactory.GetContainer().Resolve<ICSSoft.STORMNET.Business.IDataService>());
            _lockService = new LockService(DataService);
        }

        /// <summary>
        /// Locks the specified data object by identifier.
        /// </summary>
        /// <param name="dataObjectId">The data object identifier.</param>
        /// <returns>Information about lock.</returns>
        [HttpGet]
        [ActionName("Lock")]
        public LockData Lock(string dataObjectId)
        //public LockData Lock()
        {
            return _lockService.LockObject(dataObjectId, User.Identity.Name);
            //return _lockService.LockObject(dataObjectId, "Mishanya");
        }

        /// <summary>
        /// Unlocks the specified data object by identifier.
        /// </summary>
        /// <param name="dataObjectId">The data object identifier.</param>
        /// <returns>Returns <c>true</c> if object is successfully unlocked or <c>false</c> if it's not exist.</returns>
        [HttpGet]
        [ActionName("Unlock")]
        public IHttpActionResult Unlock(string dataObjectId)
        {
            return _lockService.UnlockObject(dataObjectId)
                ? (IHttpActionResult)Ok()
                : NotFound();
        }
    }
}
