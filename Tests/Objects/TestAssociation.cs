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
    /// TestAssociation.
    /// </summary>
    // *** Start programmer edit section *** (TestAssociation CustomAttributes)

    // *** End programmer edit section *** (TestAssociation CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class TestAssociation : NewPlatform.Flexberry.ORM.ODataService.Tests.SecondLevel2
    {

        private string fName2;

        // *** Start programmer edit section *** (TestAssociation CustomMembers)

        // *** End programmer edit section *** (TestAssociation CustomMembers)


        /// <summary>
        /// Name2.
        /// </summary>
        // *** Start programmer edit section *** (TestAssociation.Name2 CustomAttributes)

        // *** End programmer edit section *** (TestAssociation.Name2 CustomAttributes)
        [StrLen(255)]
        public virtual string Name2
        {
            get
            {
                // *** Start programmer edit section *** (TestAssociation.Name2 Get start)

                // *** End programmer edit section *** (TestAssociation.Name2 Get start)
                string result = this.fName2;
                // *** Start programmer edit section *** (TestAssociation.Name2 Get end)

                // *** End programmer edit section *** (TestAssociation.Name2 Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestAssociation.Name2 Set start)

                // *** End programmer edit section *** (TestAssociation.Name2 Set start)
                this.fName2 = value;
                // *** Start programmer edit section *** (TestAssociation.Name2 Set end)

                // *** End programmer edit section *** (TestAssociation.Name2 Set end)
            }
        }
    }
}

