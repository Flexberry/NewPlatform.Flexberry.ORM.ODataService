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
    /// AgrClass1.
    /// </summary>
    // *** Start programmer edit section *** (AgrClass1 CustomAttributes)

    // *** End programmer edit section *** (AgrClass1 CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class AgrClass1 : ICSSoft.STORMNET.DataObject
    {

        private string fAgrCl1Name;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass1 fDetailsClass1;

        // *** Start programmer edit section *** (AgrClass1 CustomMembers)

        // *** End programmer edit section *** (AgrClass1 CustomMembers)


        /// <summary>
        /// AgrCl1Name.
        /// </summary>
        // *** Start programmer edit section *** (AgrClass1.AgrCl1Name CustomAttributes)

        // *** End programmer edit section *** (AgrClass1.AgrCl1Name CustomAttributes)
        [StrLen(255)]
        public virtual string AgrCl1Name
        {
            get
            {
                // *** Start programmer edit section *** (AgrClass1.AgrCl1Name Get start)

                // *** End programmer edit section *** (AgrClass1.AgrCl1Name Get start)
                string result = this.fAgrCl1Name;
                // *** Start programmer edit section *** (AgrClass1.AgrCl1Name Get end)

                // *** End programmer edit section *** (AgrClass1.AgrCl1Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (AgrClass1.AgrCl1Name Set start)

                // *** End programmer edit section *** (AgrClass1.AgrCl1Name Set start)
                this.fAgrCl1Name = value;
                // *** Start programmer edit section *** (AgrClass1.AgrCl1Name Set end)

                // *** End programmer edit section *** (AgrClass1.AgrCl1Name Set end)
            }
        }

        /// <summary>
        /// AgrClass1.
        /// </summary>
        // *** Start programmer edit section *** (AgrClass1.DetailsClass1 CustomAttributes)

        // *** End programmer edit section *** (AgrClass1.DetailsClass1 CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass1 DetailsClass1
        {
            get
            {
                // *** Start programmer edit section *** (AgrClass1.DetailsClass1 Get start)

                // *** End programmer edit section *** (AgrClass1.DetailsClass1 Get start)
                if ((this.fDetailsClass1 == null))
                {
                    this.fDetailsClass1 = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass1(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfDetailsClass1 result = this.fDetailsClass1;
                // *** Start programmer edit section *** (AgrClass1.DetailsClass1 Get end)

                // *** End programmer edit section *** (AgrClass1.DetailsClass1 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (AgrClass1.DetailsClass1 Set start)

                // *** End programmer edit section *** (AgrClass1.DetailsClass1 Set start)
                this.fDetailsClass1 = value;
                // *** Start programmer edit section *** (AgrClass1.DetailsClass1 Set end)

                // *** End programmer edit section *** (AgrClass1.DetailsClass1 Set end)
            }
        }
    }
}

