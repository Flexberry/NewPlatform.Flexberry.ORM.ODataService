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
    /// DetailsClass2.
    /// </summary>
    // *** Start programmer edit section *** (DetailsClass2 CustomAttributes)

    // *** End programmer edit section *** (DetailsClass2 CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class DetailsClass2 : ICSSoft.STORMNET.DataObject
    {

        private string fDetailCl2Name;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.AgrClass2 fAgrClass2;

        // *** Start programmer edit section *** (DetailsClass2 CustomMembers)

        // *** End programmer edit section *** (DetailsClass2 CustomMembers)


        /// <summary>
        /// DetailCl2Name.
        /// </summary>
        // *** Start programmer edit section *** (DetailsClass2.DetailCl2Name CustomAttributes)

        // *** End programmer edit section *** (DetailsClass2.DetailCl2Name CustomAttributes)
        [StrLen(255)]
        public virtual string DetailCl2Name
        {
            get
            {
                // *** Start programmer edit section *** (DetailsClass2.DetailCl2Name Get start)

                // *** End programmer edit section *** (DetailsClass2.DetailCl2Name Get start)
                string result = this.fDetailCl2Name;
                // *** Start programmer edit section *** (DetailsClass2.DetailCl2Name Get end)

                // *** End programmer edit section *** (DetailsClass2.DetailCl2Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DetailsClass2.DetailCl2Name Set start)

                // *** End programmer edit section *** (DetailsClass2.DetailCl2Name Set start)
                this.fDetailCl2Name = value;
                // *** Start programmer edit section *** (DetailsClass2.DetailCl2Name Set end)

                // *** End programmer edit section *** (DetailsClass2.DetailCl2Name Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.AgrClass2.
        /// </summary>
        // *** Start programmer edit section *** (DetailsClass2.AgrClass2 CustomAttributes)

        // *** End programmer edit section *** (DetailsClass2.AgrClass2 CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "AgrClass2"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.AgrClass2 AgrClass2
        {
            get
            {
                // *** Start programmer edit section *** (DetailsClass2.AgrClass2 Get start)

                // *** End programmer edit section *** (DetailsClass2.AgrClass2 Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.AgrClass2 result = this.fAgrClass2;
                // *** Start programmer edit section *** (DetailsClass2.AgrClass2 Get end)

                // *** End programmer edit section *** (DetailsClass2.AgrClass2 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DetailsClass2.AgrClass2 Set start)

                // *** End programmer edit section *** (DetailsClass2.AgrClass2 Set start)
                this.fAgrClass2 = value;
                // *** Start programmer edit section *** (DetailsClass2.AgrClass2 Set end)

                // *** End programmer edit section *** (DetailsClass2.AgrClass2 Set end)
            }
        }
    }

    /// <summary>
    /// Detail array of DetailsClass2.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfDetailsClass2 CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfDetailsClass2 CustomAttributes)
    public class DetailArrayOfDetailsClass2 : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass2 members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass2 members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type DetailsClass2 by index.
        /// </summary>
        /// <summary>
        /// Adds object with type DetailsClass2.
        /// </summary>
        public DetailArrayOfDetailsClass2(NewPlatform.Flexberry.ORM.ODataService.Tests.AgrClass2 fAgrClass2) : 
                base(typeof(DetailsClass2), ((ICSSoft.STORMNET.DataObject)(fAgrClass2)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.DetailsClass2 this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.DetailsClass2)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.DetailsClass2 dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

