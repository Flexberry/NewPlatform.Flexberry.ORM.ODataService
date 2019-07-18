namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using Microsoft.AspNet.OData;

    /// <summary>
    /// Базовый класс для OData-контроллеров.
    /// </summary>
    public class BaseODataController : ODataController
    {
        public BaseODataController()
        {
            if (string.IsNullOrEmpty(FileController.BaseUrl) && !string.IsNullOrEmpty(FileController.RouteName))
            {
                FileController.BaseUrl = Url.Link(FileController.RouteName, new { controller = "File" });
            }
        }
    }
}
