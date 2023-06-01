namespace NewPlatform.Flexberry.ORM.ODataService.Model
{
    using System;

    /// <summary>
    /// Designed to pass the <see cref="IServiceProvider"/> interface to the <see cref="DataObjectEdmModel"/>,
    /// which doesn't support named entities, but allows third-party DI in the future.
    /// </summary>
    public class DataObjectEdmModelDependencies
    {
        public IExportService ExportService { get; private set; }

        public IExportService ExportServiceNamed { get; private set; }

        public IExportStringedObjectViewService ExportStringedObjectViewService { get; private set; }

        public IExportStringedObjectViewService ExportStringedObjectViewServiceNamed { get; private set; }

        public IODataExportService ODataExportService { get; private set; }

        public IODataExportService ODataExportServiceNamed { get; private set; }

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
