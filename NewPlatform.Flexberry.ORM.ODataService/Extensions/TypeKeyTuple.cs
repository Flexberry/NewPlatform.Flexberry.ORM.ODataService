namespace NewPlatform.Flexberry.ORM.ODataService.Extensions
{
    using System;

    /// <summary>
    /// Хранилище информации об обработанных сущностях в <see cref="TypeKeyTuple"/>.
    /// </summary>
    public class TypeKeyTuple
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeKeyTuple"/> class.
        /// </summary>
        /// <param name="objectType">Тип обработанной сущности.</param>
        /// <param name="objectPrimaryKey">Первичный ключ обработанной сущности.</param>
        public TypeKeyTuple(Type objectType, object objectPrimaryKey)
        {
            ObjectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
            ObjectPrimaryKey = objectPrimaryKey ?? throw new ArgumentNullException(nameof(objectPrimaryKey));
        }

        /// <summary>
        /// Тип обработанной сущности.
        /// </summary>
        public Type ObjectType { get; private set; }

        /// <summary>
        /// Первичный ключ обработанной сущности.
        /// </summary>
        public object ObjectPrimaryKey { get; private set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as TypeKeyTuple);
        }

        /// <inheritdoc/>
        public bool Equals(TypeKeyTuple other)
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
            return ObjectType == other.ObjectType && Equals(ObjectPrimaryKey, other.ObjectPrimaryKey);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // С помощью unchecked отключается проверка переполнения (важно для стабильности хеша).
            unchecked
            {
                int hash = 17;
                hash = (hash * 31) + (ObjectType?.GetHashCode() ?? 0);
                hash = (hash * 31) + (ObjectPrimaryKey?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
