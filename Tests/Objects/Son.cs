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
    /// Son.
    /// </summary>
    // *** Start programmer edit section *** (Son CustomAttributes)

    // *** End programmer edit section *** (Son CustomAttributes)
    [PublishName("Son")]
    [AutoAltered()]
    [ICSSoft.STORMNET.NotStored(false)]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class Son : NewPlatform.Flexberry.ORM.ODataService.Tests.Child
    {

        private string fSuspendersColor;

        // *** Start programmer edit section *** (Son CustomMembers)

        // *** End programmer edit section *** (Son CustomMembers)


        /// <summary>
        /// SuspendersColor.
        /// </summary>
        // *** Start programmer edit section *** (Son.SuspendersColor CustomAttributes)

        // *** End programmer edit section *** (Son.SuspendersColor CustomAttributes)
        [StrLen(255)]
        public virtual string SuspendersColor
        {
            get
            {
                // *** Start programmer edit section *** (Son.SuspendersColor Get start)

                // *** End programmer edit section *** (Son.SuspendersColor Get start)
                string result = this.fSuspendersColor;
                // *** Start programmer edit section *** (Son.SuspendersColor Get end)

                // *** End programmer edit section *** (Son.SuspendersColor Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Son.SuspendersColor Set start)

                // *** End programmer edit section *** (Son.SuspendersColor Set start)
                this.fSuspendersColor = value;
                // *** Start programmer edit section *** (Son.SuspendersColor Set end)

                // *** End programmer edit section *** (Son.SuspendersColor Set end)
            }
        }
    }
}

