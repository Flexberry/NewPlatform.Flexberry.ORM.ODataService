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
    /// TestConfiguration.
    /// </summary>
    // *** Start programmer edit section *** (TestConfiguration CustomAttributes)

    // *** End programmer edit section *** (TestConfiguration CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class TestConfiguration : ICSSoft.STORMNET.DataObject
    {

        private string fName;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfFirstLevel fFirstLevel;

        // *** Start programmer edit section *** (TestConfiguration CustomMembers)

        // *** End programmer edit section *** (TestConfiguration CustomMembers)


        /// <summary>
        /// Name.
        /// </summary>
        // *** Start programmer edit section *** (TestConfiguration.Name CustomAttributes)

        // *** End programmer edit section *** (TestConfiguration.Name CustomAttributes)
        [StrLen(255)]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (TestConfiguration.Name Get start)

                // *** End programmer edit section *** (TestConfiguration.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (TestConfiguration.Name Get end)

                // *** End programmer edit section *** (TestConfiguration.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestConfiguration.Name Set start)

                // *** End programmer edit section *** (TestConfiguration.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (TestConfiguration.Name Set end)

                // *** End programmer edit section *** (TestConfiguration.Name Set end)
            }
        }

        /// <summary>
        /// TestConfiguration.
        /// </summary>
        // *** Start programmer edit section *** (TestConfiguration.FirstLevel CustomAttributes)

        // *** End programmer edit section *** (TestConfiguration.FirstLevel CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfFirstLevel FirstLevel
        {
            get
            {
                // *** Start programmer edit section *** (TestConfiguration.FirstLevel Get start)

                // *** End programmer edit section *** (TestConfiguration.FirstLevel Get start)
                if ((this.fFirstLevel == null))
                {
                    this.fFirstLevel = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfFirstLevel(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfFirstLevel result = this.fFirstLevel;
                // *** Start programmer edit section *** (TestConfiguration.FirstLevel Get end)

                // *** End programmer edit section *** (TestConfiguration.FirstLevel Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestConfiguration.FirstLevel Set start)

                // *** End programmer edit section *** (TestConfiguration.FirstLevel Set start)
                this.fFirstLevel = value;
                // *** Start programmer edit section *** (TestConfiguration.FirstLevel Set end)

                // *** End programmer edit section *** (TestConfiguration.FirstLevel Set end)
            }
        }
    }
}

