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
    /// БазовыйКласс.
    /// </summary>
    // *** Start programmer edit section *** (БазовыйКласс CustomAttributes)

    // *** End programmer edit section *** (БазовыйКласс CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("БазовыйКлассE", new string[] {
            "Свойство1"})]
    [AssociatedDetailViewAttribute("БазовыйКлассE", "Детейл", "ДетейлE", true, "", "", true, new string[] {
            ""})]
    public class БазовыйКласс : ICSSoft.STORMNET.DataObject
    {

        private string fСвойство1;

        private int fСвойство2;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл fДетейл;

        // *** Start programmer edit section *** (БазовыйКласс CustomMembers)

        // *** End programmer edit section *** (БазовыйКласс CustomMembers)


        /// <summary>
        /// Свойство1.
        /// </summary>
        // *** Start programmer edit section *** (БазовыйКласс.Свойство1 CustomAttributes)
        [PublishName("Property1Alias")]
        // *** End programmer edit section *** (БазовыйКласс.Свойство1 CustomAttributes)
        [StrLen(255)]
        public virtual string Свойство1
        {
            get
            {
                // *** Start programmer edit section *** (БазовыйКласс.Свойство1 Get start)

                // *** End programmer edit section *** (БазовыйКласс.Свойство1 Get start)
                string result = this.fСвойство1;
                // *** Start programmer edit section *** (БазовыйКласс.Свойство1 Get end)

                // *** End programmer edit section *** (БазовыйКласс.Свойство1 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (БазовыйКласс.Свойство1 Set start)

                // *** End programmer edit section *** (БазовыйКласс.Свойство1 Set start)
                this.fСвойство1 = value;
                // *** Start programmer edit section *** (БазовыйКласс.Свойство1 Set end)

                // *** End programmer edit section *** (БазовыйКласс.Свойство1 Set end)
            }
        }

        /// <summary>
        /// Свойство2.
        /// </summary>
        // *** Start programmer edit section *** (БазовыйКласс.Свойство2 CustomAttributes)
        [PublishName("Property2Alias")]
        // *** End programmer edit section *** (БазовыйКласс.Свойство2 CustomAttributes)
        public virtual int Свойство2
        {
            get
            {
                // *** Start programmer edit section *** (БазовыйКласс.Свойство2 Get start)

                // *** End programmer edit section *** (БазовыйКласс.Свойство2 Get start)
                int result = this.fСвойство2;
                // *** Start programmer edit section *** (БазовыйКласс.Свойство2 Get end)

                // *** End programmer edit section *** (БазовыйКласс.Свойство2 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (БазовыйКласс.Свойство2 Set start)

                // *** End programmer edit section *** (БазовыйКласс.Свойство2 Set start)
                this.fСвойство2 = value;
                // *** Start programmer edit section *** (БазовыйКласс.Свойство2 Set end)

                // *** End programmer edit section *** (БазовыйКласс.Свойство2 Set end)
            }
        }

        /// <summary>
        /// БазовыйКласс.
        /// </summary>
        // *** Start programmer edit section *** (БазовыйКласс.Детейл CustomAttributes)
        [PublishName("DetailAlias")]
        // *** End programmer edit section *** (БазовыйКласс.Детейл CustomAttributes)
        [TypeUsage(new System.Type[] {
                typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Детейл),
                typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.ДетейлНаследник)})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл Детейл
        {
            get
            {
                // *** Start programmer edit section *** (БазовыйКласс.Детейл Get start)

                // *** End programmer edit section *** (БазовыйКласс.Детейл Get start)
                if ((this.fДетейл == null))
                {
                    this.fДетейл = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfДетейл result = this.fДетейл;
                // *** Start programmer edit section *** (БазовыйКласс.Детейл Get end)

                // *** End programmer edit section *** (БазовыйКласс.Детейл Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (БазовыйКласс.Детейл Set start)

                // *** End programmer edit section *** (БазовыйКласс.Детейл Set start)
                this.fДетейл = value;
                // *** Start programmer edit section *** (БазовыйКласс.Детейл Set end)

                // *** End programmer edit section *** (БазовыйКласс.Детейл Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "БазовыйКлассE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View БазовыйКлассE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("БазовыйКлассE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.БазовыйКласс));
                }
            }
        }
    }
}

