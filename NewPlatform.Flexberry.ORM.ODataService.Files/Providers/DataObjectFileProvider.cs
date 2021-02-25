﻿namespace NewPlatform.Flexberry.ORM.ODataService.Files.Providers
{
    using System.IO;

    using File = ICSSoft.STORMNET.FileType.File;

    /// <summary>
    /// Провайдер для файловых свойств объектов данных типа <see cref="File"/>.
    /// </summary>
    public class DataObjectFileProvider : BaseDataObjectFileProvider<File>
    {
        /// <summary>
        /// Конструктор класса <see cref="DataObjectFileProvider"/>.
        /// </summary>
        public DataObjectFileProvider()
            : base()
        {
        }

        /// <summary>
        /// Осуществляет получение файлового свойства из файла, расположенного по заданному пути.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Значение файлового свойства объекта данных, соответствующее типу <see cref="File"/>.</returns>
        public override File GetFileProperty(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"File \"{filePath}\" not found.");
            }

            var fileInfo = new FileInfo(filePath);
            File fileProperty = new File { Name = fileInfo.Name, Size = fileInfo.Length };
            fileProperty.FromNormalToZip(filePath);

            return fileProperty;
        }

        /// <summary>
        /// Осуществляет получение имени файла для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">Файловому свойству объекта данных, для которого требуется получить имя файла.</param>
        /// <returns>Имя файла.</returns>
        public override string GetFileName(File fileProperty)
        {
            return fileProperty?.Name;
        }

        /// <summary>
        /// Осуществляет получение размера файла, связанного с объектом данных, в байтах.
        /// </summary>
        /// <param name="fileProperty">Файловое свойство объекта данных, для которого требуется получить размер файла.</param>
        /// <returns>Размер файла в байтах.</returns>
        public override long GetFileSize(File fileProperty)
        {
            return fileProperty?.Size ?? 0;
        }

        /// <summary>
        /// Осуществляет получение потока данных для файлового свойства объекта данных.
        /// </summary>
        /// <param name="fileProperty">Значение файлового свойства объекта данных, для которого требуется получить поток данных.</param>
        /// <returns>Поток данных.</returns>
        public override Stream GetFileStream(File fileProperty)
        {
            return fileProperty?.GetUnzippedFile();
        }
    }
}
