namespace NewPlatform.Flexberry.ORM.ODataService.Extensions
{
    using System;

    /// <summary>
    /// Хранилище информации об обработанных сущностях в <see cref="DataServiceExtensions"/>.
    /// </summary>
    public class DataServiceExtensionHelper : IEquatable<DataServiceExtensionHelper>
    {
        /// <summary>
        /// Тип обработанной сущности.
        /// </summary>
        public Type TypeInfo { get; set; }

        /// <summary>
        /// Первичный ключ обработанной сущности.
        /// </summary>
        public object PrimaryKeyInfo { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as DataServiceExtensionHelper);
        }

        /// <inheritdoc/>
        public bool Equals(DataServiceExtensionHelper other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Сравнение Type по ссылке (Type — синглтон, так что это безопасно).
            // PrimaryKeyInfo сравнивается через object.Equals (поддерживается null и value types).
            return TypeInfo == other.TypeInfo && Equals(PrimaryKeyInfo, other.PrimaryKeyInfo);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // С помощью unchecked отключается проверка переполнения (важно для стабильности хеша).
            unchecked
            {
                int hash = 17;
                hash = (hash * 31) + (TypeInfo?.GetHashCode() ?? 0);
                hash = (hash * 31) + (PrimaryKeyInfo?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
