namespace NewPlatform.Flexberry.ORM.ODataService.Files.Providers
{
    using System.IO;

    using WebFile = ICSSoft.STORMNET.UserDataTypes.WebFile;

    /// <summary>
    /// Провайдер для свойства объектов данных типа <see cref="WebFile"/>.
    /// </summary>
    public class DataObjectWebFileProvider : BaseDataObjectFileProvider<WebFile>
    {
        /// <summary>
        /// Конструктор класса <see cref="DataObjectWebFileProvider"/>.
        /// </summary>
        public DataObjectWebFileProvider()
            : base()
        {
        }

        /// <summary>
        /// Осуществляет получение метаданных с описанием файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить метаданные файла.
        /// </param>
        /// <returns>
        /// Метаданные с описанием файлового свойства объекта данных.
        /// </returns>
        public override FileDescription GetFileDescription(WebFile fileProperty)
        {
            FileDescription fileDescription = base.GetFileDescription(fileProperty);
            if (fileDescription != null)
            {
                fileDescription.FileUrl = fileProperty?.Url;
            }

            return fileDescription;
        }

        /// <summary>
        /// Осуществляет получение файлового свойства из файла, расположенного по заданному пути.
        /// </summary>
        /// <param name="filePath">
        /// Путь к файлу.
        /// </param>
        /// <returns>
        /// Значение файлового свойства объекта данных, соответствующее типу <see cref="WebFile"/>.
        /// </returns>
        public override WebFile GetFileProperty(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("File \"{0}\" not found.", filePath));
            }

            FileDescription fileDescription = new FileDescription(FileBaseUrl, filePath);
            WebFile fileProperty = new WebFile
            {
                Name = fileDescription.FileName,
                Size = (int)fileDescription.FileSize,
                Url = fileDescription.FileUrl,
            };

            return fileProperty;
        }

        /// <summary>
        /// Осуществляет получение имени файла для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловому свойству объекта данных, для которого требуется получить имя файла.
        /// </param>
        /// <returns>
        /// Имя файла.
        /// </returns>
        public override string GetFileName(WebFile fileProperty)
        {
            return fileProperty?.Name;
        }

        /// <summary>
        /// Осуществляет получение размера файла, связанного с объектом данных, в байтах.
        /// </summary>
        /// <param name="fileProperty">
        /// Файловое свойство объекта данных, для которого требуется получить размер файла.
        /// </param>
        /// <returns>
        /// Размер файла в байтах.
        /// </returns>
        public override long GetFileSize(WebFile fileProperty)
        {
            return fileProperty?.Size ?? 0;
        }

        /// <summary>
        /// Осуществляет получение потока данных для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">
        /// Значение файлового свойства объекта данных, для которого требуется получить поток данных.
        /// </param>
        /// <returns>
        /// Поток данных.
        /// </returns>
        public override Stream GetFileStream(WebFile fileProperty)
        {
            FileDescription fileDescription = new FileDescription(FileBaseUrl)
            {
                FileUrl = fileProperty?.Url,
            };

            string filePath = string.Concat(UploadsDirectoryPath, Path.DirectorySeparatorChar, fileDescription.FileUploadKey, Path.DirectorySeparatorChar, fileDescription.FileName);
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("File \"{0}\" not found.", filePath));
            }

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
    }
}