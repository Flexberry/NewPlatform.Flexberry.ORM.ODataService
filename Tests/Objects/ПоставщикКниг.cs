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
    /// ПоставщикКниг.
    /// </summary>
    // *** Start programmer edit section *** (ПоставщикКниг CustomAttributes)

    // *** End programmer edit section *** (ПоставщикКниг CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("View", new string[] {
            "Ссылка as \'Ссылка\'"})]
    [View("ViewFull", new string[] {
            "Ссылка as \'Ссылка\'",
            "Мастер as \'Мастер\'"})]
    public class ПоставщикКниг : ICSSoft.STORMNET.DataObject
    {

        private System.Guid fСсылка;

        // *** Start programmer edit section *** (ПоставщикКниг CustomMembers)

        // *** End programmer edit section *** (ПоставщикКниг CustomMembers)


        /// <summary>
        /// Ссылка.
        /// </summary>
        // *** Start programmer edit section *** (ПоставщикКниг.Ссылка CustomAttributes)

        // *** End programmer edit section *** (ПоставщикКниг.Ссылка CustomAttributes)
        public virtual System.Guid Ссылка
        {
            get
            {
                // *** Start programmer edit section *** (ПоставщикКниг.Ссылка Get start)

                // *** End programmer edit section *** (ПоставщикКниг.Ссылка Get start)
                System.Guid result = this.fСсылка;
                // *** Start programmer edit section *** (ПоставщикКниг.Ссылка Get end)

                // *** End programmer edit section *** (ПоставщикКниг.Ссылка Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (ПоставщикКниг.Ссылка Set start)

                // *** End programmer edit section *** (ПоставщикКниг.Ссылка Set start)
                this.fСсылка = value;
                // *** Start programmer edit section *** (ПоставщикКниг.Ссылка Set end)

                // *** End programmer edit section *** (ПоставщикКниг.Ссылка Set end)
            }
        }

        /// <summary>
        /// ПоставщикКниг.
        /// </summary>
        // *** Start programmer edit section *** (ПоставщикКниг.Мастер CustomAttributes)

        // *** End programmer edit section *** (ПоставщикКниг.Мастер CustomAttributes)
        [PropertyStorage(new string[] {
                "Мастер"})]
        [ICSSoft.STORMNET.NotStored()]
        [NotNull()]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Библиотека Мастер
        {
            get
            {
                // *** Start programmer edit section *** (ПоставщикКниг.Мастер Get)
                return null;
                // *** End programmer edit section *** (ПоставщикКниг.Мастер Get)
            }
            set
            {
                // *** Start programmer edit section *** (ПоставщикКниг.Мастер Set)

                // *** End programmer edit section *** (ПоставщикКниг.Мастер Set)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "View" view.
            /// </summary>
            public static ICSSoft.STORMNET.View View
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("View", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.ПоставщикКниг));
                }
            }

            /// <summary>
            /// "ViewFull" view.
            /// </summary>
            public static ICSSoft.STORMNET.View ViewFull
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("ViewFull", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.ПоставщикКниг));
                }
            }
        }
    }
}

