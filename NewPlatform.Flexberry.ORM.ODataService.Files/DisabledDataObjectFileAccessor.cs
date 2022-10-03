namespace NewPlatform.Flexberry.ORM.ODataService.Files
{
    using System;
    using System.Collections.Generic;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using NewPlatform.Flexberry.ORM.ODataService.Files.Providers;

    /// <summary>
    /// Класс File Accessor с деактивированными функциями. Для ситуаций, когда работа с файлами в OdataService не нужна.
    /// </summary>
    public class DisabledDataObjectFileAccessor : IDataObjectFileAccessor
    {
        /// <summary>
        /// Список зарегистрированных провайдеров файловых свойств объектов данных.
        /// </summary>
        public List<IDataObjectFileProvider> DataObjectFileProviders => throw new NotImplementedException();

        /// <inheritdoc/>
        public Uri BaseUri => throw new NotImplementedException();

        /// <inheritdoc/>
        public string RouteUrl => throw new NotImplementedException();

        /// <summary>
        /// Осуществляет создание подкаталога с заданным именем в каталоге файлового хранилища.
        /// </summary>
        /// <param name="fileUploadKey">Ключ загрузки файла (используется как имя создаваемого подкаталога).</param>
        /// <returns>Путь к созданному подкаталогу. В  данной реализации File Accessor всегда возвращает null.</returns>
        public string CreateFileUploadDirectory(string fileUploadKey)
        {
            return null;
        }

        /// <summary>
        /// Осуществляет получение списка метаданных с описанием файловых свойств объекта данных,
        /// соответствующих всем типам файловых свойств, для которых есть зарегистрированные провайдеры.
        /// </summary>
        /// <param name="dataService">Сервис данных для операций с БД.</param>
        /// <param name="dataObject">Объект данных, содержащий файловые свойства.</param>
        /// <param name="excludedFilePropertiesTypes">Список типов файловых свойств объекта данных, для которых не требуется получение метаданных.</param>
        /// <returns>
        /// Список метаданных с описанием файловых свойств объекта данных,
        /// соответствующих всем типам файловых свойств, для которых есть зарегистрированные провайдеры.
        /// В данной реализации всегда возвращает пустой список.
        /// </returns>
        public List<FileDescription> GetDataObjectFileDescriptions(IDataService dataService, DataObject dataObject, List<Type> excludedFilePropertiesTypes = null)
        {
            return new List<FileDescription>();
        }

        /// <inheritdoc/>
        public IDataObjectFileProvider GetDataObjectFileProvider(Type dataObjectFilePropertyType)
        {
            return null;
        }

        /// <summary>
        /// Проверяет, имеется ли зарегистрированный провайдер для заданного типа файловых свойств объектов данных.
        /// </summary>
        /// <param name="dataObjectFilePropertyType">Тип файловыйх свойств объектов данных.</param>
        /// <returns>В данной реализации всегда возвращает false. Это значит, что провайдер не зарегистрирован.</returns>
        public bool HasDataObjectFileProvider(Type dataObjectFilePropertyType)
        {
            return false;
        }

        /// <inheritdoc/>
        public void RemoveFileUploadDirectories(List<FileDescription> removingFileDescriptions)
        {
        }

        /// <inheritdoc/>
        public void RemoveFileUploadDirectory(string fileUploadKey)
        {
        }
    }
}
