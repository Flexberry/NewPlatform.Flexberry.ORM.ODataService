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
    /// LegoSocketStandard.
    /// </summary>
    // *** Start programmer edit section *** (LegoSocketStandard CustomAttributes)

    // *** End programmer edit section *** (LegoSocketStandard CustomAttributes)
    [PublishName("LegoSocketStandard")]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class LegoSocketStandard : ICSSoft.STORMNET.DataObject
    {

        private string fName;

        // *** Start programmer edit section *** (LegoSocketStandard CustomMembers)

        // *** End programmer edit section *** (LegoSocketStandard CustomMembers)


        /// <summary>
        /// Name.
        /// </summary>
        // *** Start programmer edit section *** (LegoSocketStandard.Name CustomAttributes)

        // *** End programmer edit section *** (LegoSocketStandard.Name CustomAttributes)
        [StrLen(255)]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (LegoSocketStandard.Name Get start)

                // *** End programmer edit section *** (LegoSocketStandard.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (LegoSocketStandard.Name Get end)

                // *** End programmer edit section *** (LegoSocketStandard.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoSocketStandard.Name Set start)

                // *** End programmer edit section *** (LegoSocketStandard.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (LegoSocketStandard.Name Set end)

                // *** End programmer edit section *** (LegoSocketStandard.Name Set end)
            }
        }
    }
}

