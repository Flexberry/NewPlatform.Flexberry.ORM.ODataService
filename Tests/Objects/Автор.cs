﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Xml;
    using ICSSoft.STORMNET;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Автор.
    /// </summary>
    // *** Start programmer edit section *** (Автор CustomAttributes)

    // *** End programmer edit section *** (Автор CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("View", new string[] {
            "Имя as \'Имя\'"})]
    public class Автор : ICSSoft.STORMNET.DataObject
    {

        private string fИмя;

        private TypeSavedAsString fCustomSerializedType;

        // *** Start programmer edit section *** (Автор CustomMembers)

        // *** End programmer edit section *** (Автор CustomMembers)


        /// <summary>
        /// Имя.
        /// </summary>
        // *** Start programmer edit section *** (Автор.Имя CustomAttributes)

        // *** End programmer edit section *** (Автор.Имя CustomAttributes)
        [StrLen(255)]
        public virtual string Имя
        {
            get
            {
                // *** Start programmer edit section *** (Автор.Имя Get start)

                // *** End programmer edit section *** (Автор.Имя Get start)
                string result = this.fИмя;
                // *** Start programmer edit section *** (Автор.Имя Get end)

                // *** End programmer edit section *** (Автор.Имя Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Автор.Имя Set start)

                // *** End programmer edit section *** (Автор.Имя Set start)
                this.fИмя = value;
                // *** Start programmer edit section *** (Автор.Имя Set end)

                // *** End programmer edit section *** (Автор.Имя Set end)
            }
        }

        public virtual TypeSavedAsString CustomSerializedType
        {
            get
            {
                // *** Start programmer edit section *** (File.FileContent Get start)

                // *** End programmer edit section *** (File.FileContent Get start)
                TypeSavedAsString result = this.fCustomSerializedType;
                // *** Start programmer edit section *** (File.FileContent Get end)

                // *** End programmer edit section *** (File.FileContent Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (File.FileContent Set start)

                // *** End programmer edit section *** (File.FileContent Set start)
                this.fCustomSerializedType = value;
                // *** Start programmer edit section *** (File.FileContent Set end)

                // *** End programmer edit section *** (File.FileContent Set end)
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
                    return ICSSoft.STORMNET.Information.GetView("View", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Автор));
                }
            }
        }
    }
}
