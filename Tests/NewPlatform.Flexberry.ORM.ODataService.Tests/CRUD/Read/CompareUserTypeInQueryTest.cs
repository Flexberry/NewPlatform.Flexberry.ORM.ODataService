namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System.Linq;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using Xunit;

    /// <summary>
    /// Сравнение пользовательского типа в Query
    /// </summary>
    public class CompareUserTypeInQueryTest : BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// ok.
        /// </summary>
        [Fact]
        public void RunTest()
        {
            ActODataService(args =>
            {
                var type = new TypeSavedAsString { Text = "hello world" };
                Автор автор = new Автор { Имя = "Паша", CustomSerializedType = type };
                Библиотека библиотека = new Библиотека { Адрес = "Магнитогорск" };
                var журнал = new Журнал { Автор2 = автор, Библиотека2 = библиотека };

                var объекты = new DataObject[] { автор, библиотека, журнал };
                args.DataService.UpdateObjects(ref объекты);

                var жур = args.DataService.Query<Журнал>("Dжурнал")
                    .Where(x => x.Автор2.CustomSerializedType != null)
                    .FirstOrDefault();
                Assert.NotNull(жур);
                Assert.Equal("hello world", жур.Автор2.CustomSerializedType.Text);
            });
        }
    }
}
