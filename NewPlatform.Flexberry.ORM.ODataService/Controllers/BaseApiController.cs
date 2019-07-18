namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// Базовый класс для WebApi-контроллеров.
    /// </summary>
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
            if (string.IsNullOrEmpty(FileController.BaseUrl) && !string.IsNullOrEmpty(FileController.RouteName))
            {
                FileController.BaseUrl = Url.Link(FileController.RouteName, new { controller = "File" });
            }
        }
    }
}
