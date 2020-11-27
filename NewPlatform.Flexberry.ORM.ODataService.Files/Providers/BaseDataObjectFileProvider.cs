namespace NewPlatform.Flexberry.ORM.ODataService.Files.Providers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    /// <summary>
    /// Базовый провайдер для файловых свойств объектов данных.
    /// </summary>
    public abstract class BaseDataObjectFileProvider<T> : IDataObjectFileProvider, IDataObjectFileProvider<T>
        where T : class
    {
        /// <inheritdoc />
        public Type FilePropertyType { get; } = typeof(T);

        /// <inheritdoc />
        public string UploadsDirectoryPath { get; set; }

        /// <inheritdoc />
        public string FileBaseUrl { get; set; }

        /// <summary>
        /// Конструктор классa <see cref="BaseDataObjectFileProvider{T}" />.
        /// </summary>
        protected BaseDataObjectFileProvider()
        {
        }

        #region IDataObjectFileProvider

        /// <inheritdoc />
        FileDescription IDataObjectFileProvider.GetFileDescription(object fileProperty)
        {
            return GetFileDescription(fileProperty as T);
        }

        /// <inheritdoc />
        object IDataObjectFileProvider.GetFileProperty(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            return GetFileProperty(dataService, dataObject, dataObjectFilePropertyName);
        }

        /// <inheritdoc />
        object IDataObjectFileProvider.GetFileProperty(string filePath)
        {
            return GetFileProperty(filePath);
        }

        /// <inheritdoc />
        object IDataObjectFileProvider.GetFileProperty(IDataService dataService, FileDescription fileDescription)
        {
            return GetFileProperty(dataService, fileDescription);
        }

        /// <inheritdoc />
        IEnumerable<object> IDataObjectFileProvider.GetFileProperties(IDataService dataService, DataObject dataObject)
        {
            return GetFileProperties(dataService, dataObject);
        }

        /// <inheritdoc />
        string IDataObjectFileProvider.GetFileName(object fileProperty)
        {
            return GetFileName(fileProperty as T);
        }

        /// <inheritdoc />
        string IDataObjectFileProvider.GetFileMimeType(object fileProperty)
        {
            return GetFileMimeType(fileProperty as T);
        }

        /// <inheritdoc />
        long IDataObjectFileProvider.GetFileSize(object fileProperty)
        {
            return GetFileSize(fileProperty as T);
        }

        /// <inheritdoc />
        Stream IDataObjectFileProvider.GetFileStream(object fileProperty)
        {
            return GetFileStream(fileProperty as T);
        }

        /// <inheritdoc />
        void IDataObjectFileProvider.RemoveFile(object fileProperty)
        {
            RemoveFile(fileProperty as T);
        }

        #endregion

        /// <inheritdoc />
        public virtual FileDescription GetFileDescription(T fileProperty)
        {
            if (fileProperty == null)
            {
                return null;
            }

            return new FileDescription
            {
                FileBaseUrl = FileBaseUrl,
                FileName = GetFileName(fileProperty),
                FileSize = GetFileSize(fileProperty),
                FileMimeType = GetFileMimeType(fileProperty),
            };
        }

        /// <inheritdoc />
        public virtual FileDescription GetFileDescription(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            FileDescription fileDescription = GetFileDescription(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));

            if (fileDescription != null)
            {
                fileDescription.EntityTypeName = dataObject.GetType().AssemblyQualifiedName;
                fileDescription.EntityPropertyName = dataObjectFilePropertyName;
                fileDescription.EntityPrimaryKey = dataObject.__PrimaryKey.ToString();
                fileDescription.FilePropertyType = FilePropertyType;
            }

            return fileDescription;
        }

        /// <inheritdoc />
        public virtual IEnumerable<FileDescription> GetFileDescriptions(IDataService dataService, DataObject dataObject)
        {
            return dataObject
                ?.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == FilePropertyType)
                .Select(x => x.Name)
                .Select(x => GetFileDescription(dataService, dataObject, x))
                .Where(x => x != null);
        }

        /// <inheritdoc />
        public virtual T GetFileProperty(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            if (dataObject == null)
            {
                throw new ArgumentNullException(nameof(dataObject));
            }

            Type dataObjectType = dataObject.GetType();
            Type dataObjectFilePropertyType = Information.GetPropertyType(dataObjectType, dataObjectFilePropertyName);
            if (dataObjectFilePropertyType != FilePropertyType)
            {
                throw new Exception(
                    string.Format(
                        "Wrong type of {0}.{1} property. Actual is {2}, but {3} is expected.",
                        nameof(dataObject),
                        dataObjectFilePropertyName,
                        dataObjectFilePropertyType.FullName,
                        FilePropertyType.FullName));
            }

            // Выполняем дочитку объекта данных, если в переданном объекте на загружено искомое файловое свойство.
            DataObject srcDataObject = dataObject;
            if (dataObject.GetStatus() != ObjectStatus.Created && !dataObject.CheckLoadedProperty(dataObjectFilePropertyName))
            {
                var view = new View { DefineClassType = dataObjectType, Name = "FilePropertyView" };
                view.AddProperty(dataObjectFilePropertyName);

                srcDataObject = (DataObject)Activator.CreateInstance(dataObjectType);
                srcDataObject.SetExistObjectPrimaryKey(dataObject.__PrimaryKey);

                dataService.LoadObject(view, srcDataObject);
            }

            return Information.GetPropValueByName(srcDataObject, dataObjectFilePropertyName) as T;
        }

        /// <inheritdoc />
        public abstract T GetFileProperty(string filePath);

        /// <inheritdoc />
        public virtual T GetFileProperty(IDataService dataService, FileDescription fileDescription)
        {
            if (fileDescription == null)
            {
                throw new ArgumentNullException(nameof(fileDescription));
            }

            // Если в описании запрашиваемого файла присутствует FileUploadKey,
            // значит файл был загружен на сервер, но еще не был привязан к объекту данных,
            // и нужно сформировать файловое свойство на основе загруженного файла.
            if (!string.IsNullOrEmpty(fileDescription.FileUploadKey))
            {
                string filePath = string.Concat(UploadsDirectoryPath, Path.DirectorySeparatorChar, fileDescription.FileUploadKey, Path.DirectorySeparatorChar, fileDescription.FileName);

                return GetFileProperty(filePath);
            }

            // Если в описании запрашиваемого файла присутствует первичный ключ объекта данных,
            // значит файл уже был связан с объектом данных, и нужно вычитать файловое свойство из него.
            if (!string.IsNullOrEmpty(fileDescription.EntityPrimaryKey))
            {
                Type dataObjectType = Type.GetType(fileDescription.EntityTypeName, true);

                var dataObject = (DataObject)Activator.CreateInstance(dataObjectType);
                dataObject.SetExistObjectPrimaryKey(fileDescription.EntityPrimaryKey);

                return GetFileProperty(dataService, dataObject, fileDescription.EntityPropertyName);
            }

            throw new Exception(
                string.Format(
                    "Both \"{0}\" properties: \"{1}\" & \"{2}\" are undefined.",
                    nameof(fileDescription),
                    nameof(FileDescription.FileUploadKey),
                    nameof(FileDescription.EntityPrimaryKey)));
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> GetFileProperties(IDataService dataService, DataObject dataObject)
        {
            return dataObject
                ?.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == FilePropertyType)
                .Select(x => x.Name)
                .Select(x => GetFileProperty(dataService, dataObject, x))
                .Where(x => x != null);
        }

        /// <inheritdoc />
        public abstract string GetFileName(T fileProperty);

        /// <inheritdoc />
        public virtual string GetFileName(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            return GetFileName(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));
        }

        /// <inheritdoc />
        public virtual string GetFileMimeType(T fileProperty)
        {
            return MimeTypeUtils.GetFileMimeType(GetFileName(fileProperty));
        }

        /// <inheritdoc />
        public virtual string GetFileMimeType(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            return GetFileMimeType(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));
        }

        /// <inheritdoc />
        public abstract long GetFileSize(T fileProperty);

        /// <inheritdoc />
        public virtual long GetFileSize(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            return GetFileSize(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));
        }

        /// <inheritdoc />
        public abstract Stream GetFileStream(T fileProperty);

        /// <inheritdoc />
        public virtual Stream GetFileStream(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            return GetFileStream(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));
        }

        /// <inheritdoc />
        public virtual Stream GetFileStream(IDataService dataService, FileDescription fileDescription)
        {
            return GetFileStream(GetFileProperty(dataService, fileDescription));
        }

        /// <inheritdoc />
        public virtual void RemoveFile(FileDescription fileDescription)
        {
            string fileDirectoryPath = string.Concat(UploadsDirectoryPath, Path.DirectorySeparatorChar, fileDescription.FileUploadKey);

            if (Directory.Exists(fileDirectoryPath))
            {
                Directory.Delete(fileDirectoryPath, true);
            }
        }

        /// <inheritdoc />
        public virtual void RemoveFile(T fileProperty)
        {
            RemoveFile(GetFileDescription(fileProperty));
        }

        /// <inheritdoc />
        public virtual void RemoveFile(IDataService dataService, DataObject dataObject, string dataObjectFilePropertyName)
        {
            RemoveFile(GetFileProperty(dataService, dataObject, dataObjectFilePropertyName));
        }
    }
}
