namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using ICSSoft.STORMNET;

    /// <summary>
    /// Пользовательский тип, сериализующийся в строку
    /// </summary>
    [StoreInstancesInType(typeof(ICSSoft.STORMNET.Business.SQLDataService), typeof(string))]
    [Serializable]
    public class TypeSavedAsString : IComparableType
    {
        public string Text { get; set; }

        public static explicit operator string(TypeSavedAsString value)
        {
            return string.Concat(value.Text, "|Serialized");
        }

        public static explicit operator TypeSavedAsString(string value)
        {
            return new TypeSavedAsString
            {
                Text = value.Split('|')[0]
            };
        }

        public static bool operator !=(TypeSavedAsString l, TypeSavedAsString r)
        {
            return !(l == r);
        }

        public static bool operator ==(TypeSavedAsString l, TypeSavedAsString r)
        {
            return ((string)l).Equals((string)r);
        }

        public int Compare(object x)
        {
            var obj = (TypeSavedAsString)x;
            return this.Text == obj.Text ? 0 : 1;
        }
    }
}
