﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using ICSSoft.STORMNET;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Книга.
    /// </summary>
    // *** Start programmer edit section *** (Книга CustomAttributes)

    // *** End programmer edit section *** (Книга CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("Dкнига", new string[] {
            "Название as \'Название\'",
            "Автор1 as \'Автор\'"})]
    [MasterViewDefineAttribute("Dкнига", "Автор1", ICSSoft.STORMNET.LookupTypeEnum.Standard, "", "Имя")]
    public class Книга : ICSSoft.STORMNET.DataObject
    {

        private string fНазвание;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Автор fАвтор1;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека fБиблиотека1;

        // *** Start programmer edit section *** (Книга CustomMembers)

        // *** End programmer edit section *** (Книга CustomMembers)


        /// <summary>
        /// Название.
        /// </summary>
        // *** Start programmer edit section *** (Книга.Название CustomAttributes)

        // *** End programmer edit section *** (Книга.Название CustomAttributes)
        [StrLen(255)]
        public virtual string Название
        {
            get
            {
                // *** Start programmer edit section *** (Книга.Название Get start)

                // *** End programmer edit section *** (Книга.Название Get start)
                string result = this.fНазвание;
                // *** Start programmer edit section *** (Книга.Название Get end)

                // *** End programmer edit section *** (Книга.Название Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Книга.Название Set start)

                // *** End programmer edit section *** (Книга.Название Set start)
                this.fНазвание = value;
                // *** Start programmer edit section *** (Книга.Название Set end)

                // *** End programmer edit section *** (Книга.Название Set end)
            }
        }

        /// <summary>
        /// Книга.
        /// </summary>
        // *** Start programmer edit section *** (Книга.Автор1 CustomAttributes)

        // *** End programmer edit section *** (Книга.Автор1 CustomAttributes)
        [PropertyStorage(new string[] {
                "Автор1"})]
        [NotNull()]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Автор Автор1
        {
            get
            {
                // *** Start programmer edit section *** (Книга.Автор1 Get start)

                // *** End programmer edit section *** (Книга.Автор1 Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Автор result = this.fАвтор1;
                // *** Start programmer edit section *** (Книга.Автор1 Get end)

                // *** End programmer edit section *** (Книга.Автор1 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Книга.Автор1 Set start)

                // *** End programmer edit section *** (Книга.Автор1 Set start)
                this.fАвтор1 = value;
                // *** Start programmer edit section *** (Книга.Автор1 Set end)

                // *** End programmer edit section *** (Книга.Автор1 Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека.
        /// </summary>
        // *** Start programmer edit section *** (Книга.Библиотека1 CustomAttributes)

        // *** End programmer edit section *** (Книга.Библиотека1 CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "Библиотека1"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека Библиотека1
        {
            get
            {
                // *** Start programmer edit section *** (Книга.Библиотека1 Get start)

                // *** End programmer edit section *** (Книга.Библиотека1 Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека result = this.fБиблиотека1;
                // *** Start programmer edit section *** (Книга.Библиотека1 Get end)

                // *** End programmer edit section *** (Книга.Библиотека1 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Книга.Библиотека1 Set start)

                // *** End programmer edit section *** (Книга.Библиотека1 Set start)
                this.fБиблиотека1 = value;
                // *** Start programmer edit section *** (Книга.Библиотека1 Set end)

                // *** End programmer edit section *** (Книга.Библиотека1 Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "Dкнига" view.
            /// </summary>
            public static ICSSoft.STORMNET.View Dкнига
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("Dкнига", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Книга));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of Книга.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfКнига CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfКнига CustomAttributes)
    public class DetailArrayOfКнига : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfКнига members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfКнига members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Книга by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Книга.
        /// </summary>
        public DetailArrayOfКнига(NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека fБиблиотека) : 
                base(typeof(Книга), ((ICSSoft.STORMNET.DataObject)(fБиблиотека)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.Книга this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Книга)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Книга dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

