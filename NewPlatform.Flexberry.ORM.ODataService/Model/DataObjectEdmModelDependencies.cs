namespace NewPlatform.Flexberry.ORM.ODataService.Model
{
    using System;
    using ICSSoft.STORMNET.Business;

    /// <summary>
    /// Designed to pass the <see cref="IServiceProvider"/> interface to the <see cref="DataObjectEdmModel"/>,
    /// which doesn't support named entities, but allows third-party DI in the future.
    /// </summary>
    public class DataObjectEdmModelDependencies
    {
        /// <summary>
        /// An unnamed entity of data export service from ORM.
        /// </summary>
        public IExportService ExportService { get; private set; }

        /// <summary>
        /// Named entity of data export service from ORM.
        /// </summary>
        public IExportService ExportServiceNamed { get; private set; }

        /// <summary>
        /// An unnamed entity of data export service with <see cref="ObjectStringDataView"/>.
        /// </summary>
        public IExportStringedObjectViewService ExportStringedObjectViewService { get; private set; }

        /// <summary>
        /// Named entity of data export service with <see cref="ObjectStringDataView"/>.
        /// </summary>
        public IExportStringedObjectViewService ExportStringedObjectViewServiceNamed { get; private set; }

        /// <summary>
        /// An unnamed entity of data export service from ODataService.
        /// </summary>
        public IODataExportService ODataExportService { get; private set; }

        /// <summary>
        /// Named entity of data export service from ODataService.
        /// </summary>
        public IODataExportService ODataExportServiceNamed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectEdmModelDependencies"/> class.
        /// </summary>
        /// <param name="exportService">An unnamed entity of data export service from ORM.</param>
        /// <param name="exportServiceNamed">Named entity of data export service from ORM.</param>
        /// <param name="exportStringedObjectViewService">An unnamed entity of data export service with <see cref="ObjectStringDataView"/>.</param>
        /// <param name="exportStringedObjectViewServiceNamed">Named entity of data export service with <see cref="ObjectStringDataView"/>.</param>
        /// <param name="oDataExportService">An unnamed entity of data export service from ODataService.</param>
        /// <param name="oDataExportServiceNamed">Named entity of data export service from ODataService.</param>
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
