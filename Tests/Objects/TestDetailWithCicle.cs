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
    /// TestDetailWithCicle.
    /// </summary>
    // *** Start programmer edit section *** (TestDetailWithCicle CustomAttributes)

    // *** End programmer edit section *** (TestDetailWithCicle CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("TestDetailWithCicleE", new string[] {
            "TestDetailName",
            "TestMaster",
            "TestMaster.TestMasterName",
            "Parent",
            "Parent.TestDetailName"})]
    [View("TestDetaiWithCicleL", new string[] {
            "TestDetailName",
            "TestMaster",
            "TestMaster.TestMasterName",
            "Parent",
            "Parent.TestDetailName"})]
    public class TestDetailWithCicle : ICSSoft.STORMNET.DataObject
    {

        private string fTestDetailName;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle fParent;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.TestMaster fTestMaster;

        // *** Start programmer edit section *** (TestDetailWithCicle CustomMembers)

        // *** End programmer edit section *** (TestDetailWithCicle CustomMembers)


        /// <summary>
        /// TestDetailName.
        /// </summary>
        // *** Start programmer edit section *** (TestDetailWithCicle.TestDetailName CustomAttributes)

        // *** End programmer edit section *** (TestDetailWithCicle.TestDetailName CustomAttributes)
        [StrLen(255)]
        public virtual string TestDetailName
        {
            get
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.TestDetailName Get start)

                // *** End programmer edit section *** (TestDetailWithCicle.TestDetailName Get start)
                string result = this.fTestDetailName;
                // *** Start programmer edit section *** (TestDetailWithCicle.TestDetailName Get end)

                // *** End programmer edit section *** (TestDetailWithCicle.TestDetailName Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.TestDetailName Set start)

                // *** End programmer edit section *** (TestDetailWithCicle.TestDetailName Set start)
                this.fTestDetailName = value;
                // *** Start programmer edit section *** (TestDetailWithCicle.TestDetailName Set end)

                // *** End programmer edit section *** (TestDetailWithCicle.TestDetailName Set end)
            }
        }

        /// <summary>
        /// TestDetailWithCicle.
        /// </summary>
        // *** Start programmer edit section *** (TestDetailWithCicle.Parent CustomAttributes)

        // *** End programmer edit section *** (TestDetailWithCicle.Parent CustomAttributes)
        [PropertyStorage(new string[] {
                "Parent"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle Parent
        {
            get
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.Parent Get start)

                // *** End programmer edit section *** (TestDetailWithCicle.Parent Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle result = this.fParent;
                // *** Start programmer edit section *** (TestDetailWithCicle.Parent Get end)

                // *** End programmer edit section *** (TestDetailWithCicle.Parent Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.Parent Set start)

                // *** End programmer edit section *** (TestDetailWithCicle.Parent Set start)
                this.fParent = value;
                // *** Start programmer edit section *** (TestDetailWithCicle.Parent Set end)

                // *** End programmer edit section *** (TestDetailWithCicle.Parent Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.TestMaster.
        /// </summary>
        // *** Start programmer edit section *** (TestDetailWithCicle.TestMaster CustomAttributes)

        // *** End programmer edit section *** (TestDetailWithCicle.TestMaster CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "TestMaster"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.TestMaster TestMaster
        {
            get
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.TestMaster Get start)

                // *** End programmer edit section *** (TestDetailWithCicle.TestMaster Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.TestMaster result = this.fTestMaster;
                // *** Start programmer edit section *** (TestDetailWithCicle.TestMaster Get end)

                // *** End programmer edit section *** (TestDetailWithCicle.TestMaster Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (TestDetailWithCicle.TestMaster Set start)

                // *** End programmer edit section *** (TestDetailWithCicle.TestMaster Set start)
                this.fTestMaster = value;
                // *** Start programmer edit section *** (TestDetailWithCicle.TestMaster Set end)

                // *** End programmer edit section *** (TestDetailWithCicle.TestMaster Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "TestDetailWithCicleE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View TestDetailWithCicleE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("TestDetailWithCicleE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle));
                }
            }

            /// <summary>
            /// "TestDetaiWithCicleL" view.
            /// </summary>
            public static ICSSoft.STORMNET.View TestDetaiWithCicleL
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("TestDetaiWithCicleL", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of TestDetailWithCicle.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfTestDetailWithCicle CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfTestDetailWithCicle CustomAttributes)
    public class DetailArrayOfTestDetailWithCicle : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfTestDetailWithCicle members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfTestDetailWithCicle members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type TestDetailWithCicle by index.
        /// </summary>
        /// <summary>
        /// Adds object with type TestDetailWithCicle.
        /// </summary>
        public DetailArrayOfTestDetailWithCicle(NewPlatform.Flexberry.ORM.ODataService.Tests.TestMaster fTestMaster) : 
                base(typeof(TestDetailWithCicle), ((ICSSoft.STORMNET.DataObject)(fTestMaster)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.TestDetailWithCicle dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

