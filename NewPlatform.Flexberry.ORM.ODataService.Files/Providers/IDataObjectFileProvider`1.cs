namespace NewPlatform.Flexberry.ORM.ODataService.Files.Providers
{
    using System.Collections.Generic;
    using System.IO;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    /// <summary>
    /// Интерфейс для провайдеров файловых свойств объектов данных.
    /// </summary>
    /// <typeparam name="T">Тип файлового свойства.</typeparam>
    public interface IDataObjectFileProvider<T>
        where T : class
    {
        /// <summary>
        /// Осуществляет получение метаданных с описанием файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить метаданные файла.
        /// </param>
        /// <returns>
        /// Метаданные с описанием файлового свойства объекта данных.
        /// </returns>
        FileDescription GetFileDescription(T fileProperty);

        /// <summary>
        /// Осуществляет получение файлового свойства объекта данных.
        /// </summary>
        /// <remarks>
        /// При необходимости будет произведена дочитка объекта данных.
        /// </remarks>
        /// <param name="dataService">
        /// Сервис данных для операций с БД.
        /// </param>
        /// <param name="dataObject">
        /// Объект данных, содержащий файловое свойство.
        /// </param>
        /// <param name="dataObjectFilePropertyName">
        /// Имя файлового свойства в объекте данных.
        /// </param>
        /// <returns>
        /// Значение файлового свойства объекта данных.
        /// </returns>
        T GetFileProperty(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName);

        /// <summary>
        /// Осуществляет получение файлового свойства из файла, расположенного по заданному пути.
        /// </summary>
        /// <param name="filePath">
        /// Путь к файлу.
        /// </param>
        /// <returns>
        /// Значение файлового свойства объекта данных.
        /// </returns>
        T GetFileProperty(string filePath);

        /// <summary>
        /// Осуществляет получение файлового свойства объекта данных, по его метаданным.
        /// </summary>
        /// <remarks>
        /// При необходимости будет  вычитан объект данных.
        /// </remarks>
        /// <param name="dataService">
        /// Сервис данных для операций с БД.
        /// </param>
        /// <param name="fileDescription">
        /// Метаданные с описанием файлового свойства объекта данных.
        /// </param>
        /// <returns>
        /// Значение файлового свойства объекта данных.
        /// </returns>
        T GetFileProperty(IDataService dataService, FileDescription fileDescription);

        /// <summary>
        /// Осуществляет получение списка файловых свойств объекта данных.
        /// </summary>
        /// <remarks>
        /// При необходимости будет произведена дочитка объекта данных.
        /// </remarks>
        /// <param name="dataService">
        /// Сервис данных для операций с БД.
        /// </param>
        /// <param name="dataObject">
        /// Объект данных, содержащий файловые свойства.
        /// </param>
        /// <returns>
        /// Список файловых свойств объекта данных.
        /// </returns>
        IEnumerable<T> GetFileProperties(IDataService dataService, DataObject dataObject);

        /// <summary>
        /// Осуществляет получение имени файла для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить имя файла.
        /// </param>
        /// <returns>
        /// Имя файла.
        /// </returns>
        string GetFileName(T fileProperty);

        /// <summary>
        /// Осуществляет получение MIME-типа для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить MIME-тип.
        /// </param>
        /// <returns>
        /// MIME-тип файла, соответствующего заданному файловому свойству.
        /// </returns>
        string GetFileMimeType(T fileProperty);

        /// <summary>
        /// Осуществляет получение размера файла, связанного с объектом данных, в байтах.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить размер файла.
        /// </param>
        /// <returns>
        /// Размер файла в байтах.
        /// </returns>
        long GetFileSize(T fileProperty);

        /// <summary>
        /// Осуществляет получение потока данных для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Значение файлового свойства объекта данных, для которого требуется получить поток данных.
        /// </param>
        /// <returns>
        /// Поток данных.
        /// </returns>
        Stream GetFileStream(T fileProperty);

        /// <summary>
        /// Осуществляет удаление из файловой системы файла, соответствующего файловому свойству объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Значение файлового свойства объекта данных, для которого требуется выполнить удаление.
        /// </param>
        void RemoveFile(T fileProperty);
    }
}
