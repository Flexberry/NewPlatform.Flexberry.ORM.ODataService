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
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Car.
    /// </summary>
    // *** Start programmer edit section *** (Car CustomAttributes)

    // *** End programmer edit section *** (Car CustomAttributes)
    [BusinessServer("NewPlatform.Flexberry.ORM.ODataService.Tests.CarBS, NewPlatform.Flexberry.ORM.ODa" +
        "taService.Tests.BusinessServers", ICSSoft.STORMNET.Business.DataServiceObjectEvents.OnAllEvents)]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("AllData", new string[] {
            "Number as \'Number\'",
            "Model as \'Model\'",
            "TipCar as \'TipCar\'"})]
    public class Car : ICSSoft.STORMNET.DataObject
    {

        private string fModel;

        private string fNumber;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.tTip fTipCar;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Driver fDriver;

        // *** Start programmer edit section *** (Car CustomMembers)

        // *** End programmer edit section *** (Car CustomMembers)


        /// <summary>
        /// Model.
        /// </summary>
        // *** Start programmer edit section *** (Car.Model CustomAttributes)

        // *** End programmer edit section *** (Car.Model CustomAttributes)
        [StrLen(255)]
        public virtual string Model
        {
            get
            {
                // *** Start programmer edit section *** (Car.Model Get start)

                // *** End programmer edit section *** (Car.Model Get start)
                string result = this.fModel;
                // *** Start programmer edit section *** (Car.Model Get end)

                // *** End programmer edit section *** (Car.Model Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Car.Model Set start)

                // *** End programmer edit section *** (Car.Model Set start)
                this.fModel = value;
                // *** Start programmer edit section *** (Car.Model Set end)

                // *** End programmer edit section *** (Car.Model Set end)
            }
        }

        /// <summary>
        /// Number.
        /// </summary>
        // *** Start programmer edit section *** (Car.Number CustomAttributes)

        // *** End programmer edit section *** (Car.Number CustomAttributes)
        [StrLen(255)]
        public virtual string Number
        {
            get
            {
                // *** Start programmer edit section *** (Car.Number Get start)

                // *** End programmer edit section *** (Car.Number Get start)
                string result = this.fNumber;
                // *** Start programmer edit section *** (Car.Number Get end)

                // *** End programmer edit section *** (Car.Number Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Car.Number Set start)

                // *** End programmer edit section *** (Car.Number Set start)
                this.fNumber = value;
                // *** Start programmer edit section *** (Car.Number Set end)

                // *** End programmer edit section *** (Car.Number Set end)
            }
        }

        /// <summary>
        /// TipCar.
        /// </summary>
        // *** Start programmer edit section *** (Car.TipCar CustomAttributes)

        // *** End programmer edit section *** (Car.TipCar CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.tTip TipCar
        {
            get
            {
                // *** Start programmer edit section *** (Car.TipCar Get start)

                // *** End programmer edit section *** (Car.TipCar Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.tTip result = this.fTipCar;
                // *** Start programmer edit section *** (Car.TipCar Get end)

                // *** End programmer edit section *** (Car.TipCar Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Car.TipCar Set start)

                // *** End programmer edit section *** (Car.TipCar Set start)
                this.fTipCar = value;
                // *** Start programmer edit section *** (Car.TipCar Set end)

                // *** End programmer edit section *** (Car.TipCar Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.Driver.
        /// </summary>
        // *** Start programmer edit section *** (Car.Driver CustomAttributes)

        // *** End programmer edit section *** (Car.Driver CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "driver"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Driver Driver
        {
            get
            {
                // *** Start programmer edit section *** (Car.Driver Get start)

                // *** End programmer edit section *** (Car.Driver Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Driver result = this.fDriver;
                // *** Start programmer edit section *** (Car.Driver Get end)

                // *** End programmer edit section *** (Car.Driver Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Car.Driver Set start)

                // *** End programmer edit section *** (Car.Driver Set start)
                this.fDriver = value;
                // *** Start programmer edit section *** (Car.Driver Set end)

                // *** End programmer edit section *** (Car.Driver Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "AllData" view.
            /// </summary>
            public static ICSSoft.STORMNET.View AllData
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("AllData", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Car));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of Car.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfCar CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfCar CustomAttributes)
    public class DetailArrayOfCar : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfCar members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfCar members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Car by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Car.
        /// </summary>
        public DetailArrayOfCar(NewPlatform.Flexberry.ORM.ODataService.Tests.Driver fDriver) : 
                base(typeof(Car), ((ICSSoft.STORMNET.DataObject)(fDriver)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.Car this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Car)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Car dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

