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
    /// Детейл.
    /// </summary>
    // *** Start programmer edit section *** (Детейл CustomAttributes)
    [PublishName("NewPlatform.Flexberry.ORM.ODataService.Tests.DetailAlias", "DetailAliases")]
    // *** End programmer edit section *** (Детейл CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("ДетейлE", new string[] {
            "prop1"})]
    public class Детейл : ICSSoft.STORMNET.DataObject
    {

        private int fprop1;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл2 fДетейл2;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс fБазовыйКласс;

        // *** Start programmer edit section *** (Детейл CustomMembers)

        // *** End programmer edit section *** (Детейл CustomMembers)


        /// <summary>
        /// prop1.
        /// </summary>
        // *** Start programmer edit section *** (Детейл.prop1 CustomAttributes)

        // *** End programmer edit section *** (Детейл.prop1 CustomAttributes)
        public virtual int prop1
        {
            get
            {
                // *** Start programmer edit section *** (Детейл.prop1 Get start)

                // *** End programmer edit section *** (Детейл.prop1 Get start)
                int result = this.fprop1;
                // *** Start programmer edit section *** (Детейл.prop1 Get end)

                // *** End programmer edit section *** (Детейл.prop1 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Детейл.prop1 Set start)

                // *** End programmer edit section *** (Детейл.prop1 Set start)
                this.fprop1 = value;
                // *** Start programmer edit section *** (Детейл.prop1 Set end)

                // *** End programmer edit section *** (Детейл.prop1 Set end)
            }
        }

        /// <summary>
        /// Детейл.
        /// </summary>
        // *** Start programmer edit section *** (Детейл.Детейл2 CustomAttributes)
        [PublishName("Detail2Alias")]
        // *** End programmer edit section *** (Детейл.Детейл2 CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл2 Детейл2
        {
            get
            {
                // *** Start programmer edit section *** (Детейл.Детейл2 Get start)

                // *** End programmer edit section *** (Детейл.Детейл2 Get start)
                if ((this.fДетейл2 == null))
                {
                    this.fДетейл2 = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл2(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл2 result = this.fДетейл2;
                // *** Start programmer edit section *** (Детейл.Детейл2 Get end)

                // *** End programmer edit section *** (Детейл.Детейл2 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Детейл.Детейл2 Set start)

                // *** End programmer edit section *** (Детейл.Детейл2 Set start)
                this.fДетейл2 = value;
                // *** Start programmer edit section *** (Детейл.Детейл2 Set end)

                // *** End programmer edit section *** (Детейл.Детейл2 Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс.
        /// </summary>
        // *** Start programmer edit section *** (Детейл.БазовыйКласс CustomAttributes)

        // *** End programmer edit section *** (Детейл.БазовыйКласс CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage("БазовыйКласс")]
        [TypeUsage(new string[] {
                "NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс",
                "NewPlatform.Flexberry.ORM.ODataService.Tests.Наследник"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс БазовыйКласс
        {
            get
            {
                // *** Start programmer edit section *** (Детейл.БазовыйКласс Get start)

                // *** End programmer edit section *** (Детейл.БазовыйКласс Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс result = this.fБазовыйКласс;
                // *** Start programmer edit section *** (Детейл.БазовыйКласс Get end)

                // *** End programmer edit section *** (Детейл.БазовыйКласс Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Детейл.БазовыйКласс Set start)

                // *** End programmer edit section *** (Детейл.БазовыйКласс Set start)
                this.fБазовыйКласс = value;
                // *** Start programmer edit section *** (Детейл.БазовыйКласс Set end)

                // *** End programmer edit section *** (Детейл.БазовыйКласс Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "ДетейлE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View ДетейлE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("ДетейлE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Детейл));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of Детейл.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfДетейл CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfДетейл CustomAttributes)
    public class DetailArrayOfДетейл : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Детейл by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Детейл.
        /// </summary>
        public DetailArrayOfДетейл(NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс fБазовыйКласс) : 
                base(typeof(Детейл), ((ICSSoft.STORMNET.DataObject)(fБазовыйКласс)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.Детейл this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Детейл)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Детейл dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

