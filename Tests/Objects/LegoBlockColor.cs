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
    /// LegoBlockColor.
    /// </summary>
    // *** Start programmer edit section *** (LegoBlockColor CustomAttributes)

    // *** End programmer edit section *** (LegoBlockColor CustomAttributes)
    [PublishName("LegoBlockColor")]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class LegoBlockColor : ICSSoft.STORMNET.DataObject
    {

        private int fColorNumber;

        private string fName;

        // *** Start programmer edit section *** (LegoBlockColor CustomMembers)

        // *** End programmer edit section *** (LegoBlockColor CustomMembers)


        /// <summary>
        /// ColorNumber.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockColor.ColorNumber CustomAttributes)

        // *** End programmer edit section *** (LegoBlockColor.ColorNumber CustomAttributes)
        public virtual int ColorNumber
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockColor.ColorNumber Get start)

                // *** End programmer edit section *** (LegoBlockColor.ColorNumber Get start)
                int result = this.fColorNumber;
                // *** Start programmer edit section *** (LegoBlockColor.ColorNumber Get end)

                // *** End programmer edit section *** (LegoBlockColor.ColorNumber Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockColor.ColorNumber Set start)

                // *** End programmer edit section *** (LegoBlockColor.ColorNumber Set start)
                this.fColorNumber = value;
                // *** Start programmer edit section *** (LegoBlockColor.ColorNumber Set end)

                // *** End programmer edit section *** (LegoBlockColor.ColorNumber Set end)
            }
        }

        /// <summary>
        /// Name.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockColor.Name CustomAttributes)

        // *** End programmer edit section *** (LegoBlockColor.Name CustomAttributes)
        [StrLen(255)]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockColor.Name Get start)

                // *** End programmer edit section *** (LegoBlockColor.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (LegoBlockColor.Name Get end)

                // *** End programmer edit section *** (LegoBlockColor.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockColor.Name Set start)

                // *** End programmer edit section *** (LegoBlockColor.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (LegoBlockColor.Name Set end)

                // *** End programmer edit section *** (LegoBlockColor.Name Set end)
            }
        }
    }
}

