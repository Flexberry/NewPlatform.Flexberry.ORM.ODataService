namespace NewPlatform.Flexberry.ORM.ODataService.Model
{
    public class DataObjectEdmModelDependencies
    {
        public IExportService ExportService;
        public IExportService ExportServiceNamed;
        public IExportStringedObjectViewService ExportStringedObjectViewService;
        public IExportStringedObjectViewService ExportStringedObjectViewServiceNamed;
        public IODataExportService ODataExportService;
        public IODataExportService ODataExportServiceNamed;

        public DataObjectEdmModelDependencies(
            IExportService exportService,
            IExportService exportServiceNamed,
            IExportStringedObjectViewService exportStringedObjectViewService,
            IExportStringedObjectViewService exportStringedObjectViewServiceNamed,
            IODataExportService oDataExportService,
            IODataExportService oDataExportServiceNamed)
        {
            ExportService = exportService;
            ExportServiceNamed = exportServiceNamed;
            ExportStringedObjectViewService = exportStringedObjectViewService;
            ExportStringedObjectViewServiceNamed = exportStringedObjectViewServiceNamed;
            ODataExportService = oDataExportService;
            ODataExportServiceNamed = oDataExportServiceNamed;
        }
    }
}
