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
    /// Лапа.
    /// </summary>
    // *** Start programmer edit section *** (Лапа CustomAttributes)

    // *** End programmer edit section *** (Лапа CustomAttributes)
    [BusinessServer("NewPlatform.Flexberry.ORM.ODataService.Tests.CatsBS, NewPlatform.Flexberry.ORM.OD" +
        "ataService.Tests.BusinessServers", ICSSoft.STORMNET.Business.DataServiceObjectEvents.OnAllEvents)]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("ЛапаE", new string[] {
            "Цвет",
            "Размер",
            "ДатаРождения",
            "БылиЛиПереломы",
            "Сторона",
            "Номер",
            "РазмерСтрокой",
            "РазмерDouble",
            "РазмерFloat",
            "РазмерDecimal",
            "РазмерChar",
            "Кошка.Кличка"})]
    public class Лапа : ICSSoft.STORMNET.DataObject
    {

        private bool fБылиЛиПереломы;

        private ICSSoft.STORMNET.UserDataTypes.NullableDateTime fДатаРождения;

        private int fНомер;

        private int fРазмер;

        private decimal fРазмерDecimal;

        private double fРазмерDouble;

        private float fРазмерFloat;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Сторона fСторона;

        private string fЦвет;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.ТипЛапы fТипЛапы;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfПерелом fПерелом;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Кошка fКошка;

        // *** Start programmer edit section *** (Лапа CustomMembers)

        // *** End programmer edit section *** (Лапа CustomMembers)


        /// <summary>
        /// БылиЛиПереломы.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.БылиЛиПереломы CustomAttributes)

        // *** End programmer edit section *** (Лапа.БылиЛиПереломы CustomAttributes)
        public virtual bool БылиЛиПереломы
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.БылиЛиПереломы Get start)

                // *** End programmer edit section *** (Лапа.БылиЛиПереломы Get start)
                bool result = this.fБылиЛиПереломы;
                // *** Start programmer edit section *** (Лапа.БылиЛиПереломы Get end)

                // *** End programmer edit section *** (Лапа.БылиЛиПереломы Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.БылиЛиПереломы Set start)

                // *** End programmer edit section *** (Лапа.БылиЛиПереломы Set start)
                this.fБылиЛиПереломы = value;
                // *** Start programmer edit section *** (Лапа.БылиЛиПереломы Set end)

                // *** End programmer edit section *** (Лапа.БылиЛиПереломы Set end)
            }
        }

        /// <summary>
        /// ДатаРождения.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.ДатаРождения CustomAttributes)

        // *** End programmer edit section *** (Лапа.ДатаРождения CustomAttributes)
        public virtual ICSSoft.STORMNET.UserDataTypes.NullableDateTime ДатаРождения
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.ДатаРождения Get start)

                // *** End programmer edit section *** (Лапа.ДатаРождения Get start)
                ICSSoft.STORMNET.UserDataTypes.NullableDateTime result = this.fДатаРождения;
                // *** Start programmer edit section *** (Лапа.ДатаРождения Get end)

                // *** End programmer edit section *** (Лапа.ДатаРождения Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.ДатаРождения Set start)

                // *** End programmer edit section *** (Лапа.ДатаРождения Set start)
                this.fДатаРождения = value;
                // *** Start programmer edit section *** (Лапа.ДатаРождения Set end)

                // *** End programmer edit section *** (Лапа.ДатаРождения Set end)
            }
        }

        /// <summary>
        /// Номер.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Номер CustomAttributes)

        // *** End programmer edit section *** (Лапа.Номер CustomAttributes)
        public virtual int Номер
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Номер Get start)

                // *** End programmer edit section *** (Лапа.Номер Get start)
                int result = this.fНомер;
                // *** Start programmer edit section *** (Лапа.Номер Get end)

                // *** End programmer edit section *** (Лапа.Номер Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Номер Set start)

                // *** End programmer edit section *** (Лапа.Номер Set start)
                this.fНомер = value;
                // *** Start programmer edit section *** (Лапа.Номер Set end)

                // *** End programmer edit section *** (Лапа.Номер Set end)
            }
        }

        /// <summary>
        /// Размер.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Размер CustomAttributes)

        // *** End programmer edit section *** (Лапа.Размер CustomAttributes)
        public virtual int Размер
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Размер Get start)

                // *** End programmer edit section *** (Лапа.Размер Get start)
                int result = this.fРазмер;
                // *** Start programmer edit section *** (Лапа.Размер Get end)

                // *** End programmer edit section *** (Лапа.Размер Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Размер Set start)

                // *** End programmer edit section *** (Лапа.Размер Set start)
                this.fРазмер = value;
                // *** Start programmer edit section *** (Лапа.Размер Set end)

                // *** End programmer edit section *** (Лапа.Размер Set end)
            }
        }

        /// <summary>
        /// РазмерСтрокой.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.РазмерСтрокой CustomAttributes)

        // *** End programmer edit section *** (Лапа.РазмерСтрокой CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        [StrLen(255)]
        public virtual string РазмерСтрокой
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.РазмерСтрокой Get)
                return null;
                // *** End programmer edit section *** (Лапа.РазмерСтрокой Get)
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.РазмерСтрокой Set)

                // *** End programmer edit section *** (Лапа.РазмерСтрокой Set)
            }
        }

        /// <summary>
        /// РазмерChar.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.РазмерChar CustomAttributes)

        // *** End programmer edit section *** (Лапа.РазмерChar CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        public virtual char РазмерChar
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.РазмерChar Get)
                return ' ';
                // *** End programmer edit section *** (Лапа.РазмерChar Get)
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.РазмерChar Set)

                // *** End programmer edit section *** (Лапа.РазмерChar Set)
            }
        }

        /// <summary>
        /// РазмерDecimal.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.РазмерDecimal CustomAttributes)

        // *** End programmer edit section *** (Лапа.РазмерDecimal CustomAttributes)
        public virtual decimal РазмерDecimal
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.РазмерDecimal Get start)

                // *** End programmer edit section *** (Лапа.РазмерDecimal Get start)
                decimal result = this.fРазмерDecimal;
                // *** Start programmer edit section *** (Лапа.РазмерDecimal Get end)

                // *** End programmer edit section *** (Лапа.РазмерDecimal Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.РазмерDecimal Set start)

                // *** End programmer edit section *** (Лапа.РазмерDecimal Set start)
                this.fРазмерDecimal = value;
                // *** Start programmer edit section *** (Лапа.РазмерDecimal Set end)

                // *** End programmer edit section *** (Лапа.РазмерDecimal Set end)
            }
        }

        /// <summary>
        /// РазмерDouble.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.РазмерDouble CustomAttributes)

        // *** End programmer edit section *** (Лапа.РазмерDouble CustomAttributes)
        public virtual double РазмерDouble
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.РазмерDouble Get start)

                // *** End programmer edit section *** (Лапа.РазмерDouble Get start)
                double result = this.fРазмерDouble;
                // *** Start programmer edit section *** (Лапа.РазмерDouble Get end)

                // *** End programmer edit section *** (Лапа.РазмерDouble Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.РазмерDouble Set start)

                // *** End programmer edit section *** (Лапа.РазмерDouble Set start)
                this.fРазмерDouble = value;
                // *** Start programmer edit section *** (Лапа.РазмерDouble Set end)

                // *** End programmer edit section *** (Лапа.РазмерDouble Set end)
            }
        }

        /// <summary>
        /// РазмерFloat.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.РазмерFloat CustomAttributes)

        // *** End programmer edit section *** (Лапа.РазмерFloat CustomAttributes)
        public virtual float РазмерFloat
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.РазмерFloat Get start)

                // *** End programmer edit section *** (Лапа.РазмерFloat Get start)
                float result = this.fРазмерFloat;
                // *** Start programmer edit section *** (Лапа.РазмерFloat Get end)

                // *** End programmer edit section *** (Лапа.РазмерFloat Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.РазмерFloat Set start)

                // *** End programmer edit section *** (Лапа.РазмерFloat Set start)
                this.fРазмерFloat = value;
                // *** Start programmer edit section *** (Лапа.РазмерFloat Set end)

                // *** End programmer edit section *** (Лапа.РазмерFloat Set end)
            }
        }

        /// <summary>
        /// Сторона.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Сторона CustomAttributes)

        // *** End programmer edit section *** (Лапа.Сторона CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Сторона Сторона
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Сторона Get start)

                // *** End programmer edit section *** (Лапа.Сторона Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Сторона result = this.fСторона;
                // *** Start programmer edit section *** (Лапа.Сторона Get end)

                // *** End programmer edit section *** (Лапа.Сторона Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Сторона Set start)

                // *** End programmer edit section *** (Лапа.Сторона Set start)
                this.fСторона = value;
                // *** Start programmer edit section *** (Лапа.Сторона Set end)

                // *** End programmer edit section *** (Лапа.Сторона Set end)
            }
        }

        /// <summary>
        /// Цвет.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Цвет CustomAttributes)

        // *** End programmer edit section *** (Лапа.Цвет CustomAttributes)
        [StrLen(255)]
        public virtual string Цвет
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Цвет Get start)

                // *** End programmer edit section *** (Лапа.Цвет Get start)
                string result = this.fЦвет;
                // *** Start programmer edit section *** (Лапа.Цвет Get end)

                // *** End programmer edit section *** (Лапа.Цвет Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Цвет Set start)

                // *** End programmer edit section *** (Лапа.Цвет Set start)
                this.fЦвет = value;
                // *** Start programmer edit section *** (Лапа.Цвет Set end)

                // *** End programmer edit section *** (Лапа.Цвет Set end)
            }
        }

        /// <summary>
        /// Лапа.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.ТипЛапы CustomAttributes)

        // *** End programmer edit section *** (Лапа.ТипЛапы CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.ТипЛапы ТипЛапы
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.ТипЛапы Get start)

                // *** End programmer edit section *** (Лапа.ТипЛапы Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.ТипЛапы result = this.fТипЛапы;
                // *** Start programmer edit section *** (Лапа.ТипЛапы Get end)

                // *** End programmer edit section *** (Лапа.ТипЛапы Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.ТипЛапы Set start)

                // *** End programmer edit section *** (Лапа.ТипЛапы Set start)
                this.fТипЛапы = value;
                // *** Start programmer edit section *** (Лапа.ТипЛапы Set end)

                // *** End programmer edit section *** (Лапа.ТипЛапы Set end)
            }
        }

        /// <summary>
        /// Лапа.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Перелом CustomAttributes)

        // *** End programmer edit section *** (Лапа.Перелом CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfПерелом Перелом
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Перелом Get start)

                // *** End programmer edit section *** (Лапа.Перелом Get start)
                if ((this.fПерелом == null))
                {
                    this.fПерелом = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfПерелом(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfПерелом result = this.fПерелом;
                // *** Start programmer edit section *** (Лапа.Перелом Get end)

                // *** End programmer edit section *** (Лапа.Перелом Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Перелом Set start)

                // *** End programmer edit section *** (Лапа.Перелом Set start)
                this.fПерелом = value;
                // *** Start programmer edit section *** (Лапа.Перелом Set end)

                // *** End programmer edit section *** (Лапа.Перелом Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.Кошка.
        /// </summary>
        // *** Start programmer edit section *** (Лапа.Кошка CustomAttributes)

        // *** End programmer edit section *** (Лапа.Кошка CustomAttributes)
        [Agregator()]
        [NotNull()]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Кошка Кошка
        {
            get
            {
                // *** Start programmer edit section *** (Лапа.Кошка Get start)

                // *** End programmer edit section *** (Лапа.Кошка Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Кошка result = this.fКошка;
                // *** Start programmer edit section *** (Лапа.Кошка Get end)

                // *** End programmer edit section *** (Лапа.Кошка Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Лапа.Кошка Set start)

                // *** End programmer edit section *** (Лапа.Кошка Set start)
                this.fКошка = value;
                // *** Start programmer edit section *** (Лапа.Кошка Set end)

                // *** End programmer edit section *** (Лапа.Кошка Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "ЛапаE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View ЛапаE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("ЛапаE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Лапа));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of Лапа.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfЛапа CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfЛапа CustomAttributes)
    public class DetailArrayOfЛапа : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfЛапа members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfЛапа members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Лапа by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Лапа.
        /// </summary>
        public DetailArrayOfЛапа(NewPlatform.Flexberry.ORM.ODataService.Tests.Кошка fКошка) : 
                base(typeof(Лапа), ((ICSSoft.STORMNET.DataObject)(fКошка)))
        {
        }

        public NewPlatform.Flexberry.ORM.ODataService.Tests.Лапа this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Лапа)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Лапа dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}

