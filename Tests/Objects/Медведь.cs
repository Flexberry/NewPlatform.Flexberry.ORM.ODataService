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
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Audit.Objects;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Медведь
    ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
    /// </summary>
    // *** Start programmer edit section *** (Медведь CustomAttributes)

    // *** End programmer edit section *** (Медведь CustomAttributes)
    [BusinessServer("NewPlatform.Flexberry.ORM.ODataService.Tests.BearBS, NewPlatform.Flexberry.ORM.OD" +
        "ataService.Tests.BusinessServers", ICSSoft.STORMNET.Business.DataServiceObjectEvents.OnAllEvents)]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("LoadTestView", new string[] {
            "ЛесОбитания",
            "ЛесОбитания.Заповедник",
            "Мама",
            "Мама.ЦветГлаз",
            "Вес"})]
    [AssociatedDetailViewAttribute("LoadTestView", "Берлога", "LoadTestView", true, "", "", true, new string[] {
            ""})]
    [View("OrderNumberTest", new string[] {
            "ПорядковыйНомер",
            "ЛесОбитания"})]
    [View("МедведьE", new string[] {
            "ПорядковыйНомер as \'Порядковый номер\'",
            "Вес as \'Вес\'",
            "ЦветГлаз as \'Цвет глаз\'",
            "Пол as \'Пол\'",
            "ДатаРождения as \'Дата рождения\'",
            "Мама as \'Мама\'",
            "Мама.ЦветГлаз as \'Цвет глаз\'",
            "Мама.Вес",
            "Папа as \'Папа\'",
            "Папа.ЦветГлаз as \'Цвет глаз\'",
            "Папа.Вес",
            "ЛесОбитания as \'Лес обитания\'",
            "ЛесОбитания.Название as \'Название\'",
            "ПолеБС",
            "СтранаРождения",
            "СтранаРождения.Название"})]
    [AssociatedDetailViewAttribute("МедведьE", "Берлога", "БерлогаE", true, "", "Берлога", true, new string[] {
            ""})]
    [View("МедведьL", new string[] {
            "ПорядковыйНомер as \'Порядковый номер\'",
            "Вес as \'Вес\'",
            "ЦветГлаз as \'Цвет глаз\'",
            "Пол as \'Пол\'",
            "ДатаРождения as \'Дата рождения\'",
            "Мама.ЦветГлаз as \'Цвет глаз\'",
            "Папа.ЦветГлаз as \'Цвет глаз\'",
            "ЛесОбитания.Название as \'Название\'"})]
    [View("МедведьShort", new string[] {
            "ПорядковыйНомер as \'Порядковый номер\'"})]
    [View("МедведьСДелейломИВычислимымСвойством", new string[] {
            "ПорядковыйНомер as \'Порядковый номер\'",
            "Вес as \'Вес\'",
            "ЦветГлаз as \'Цвет глаз\'",
            "Пол as \'Пол\'",
            "ДатаРождения as \'Дата рождения\'",
            "Мама as \'Мама\'",
            "Мама.ЦветГлаз as \'Цвет глаз\'",
            "Папа as \'Папа\'",
            "Папа.ЦветГлаз as \'Цвет глаз\'",
            "ЛесОбитания as \'Лес обитания\'",
            "ЛесОбитания.Название as \'Название\'",
            "МедведьСтрокой"})]
    [AssociatedDetailViewAttribute("МедведьСДелейломИВычислимымСвойством", "Берлога", "БерлогаE", true, "", "Берлога", true, new string[] {
            ""})]
    public class Медведь : ICSSoft.STORMNET.DataObject, IDataObjectWithAuditFields
    {

        private int fВес;

        private ICSSoft.STORMNET.UserDataTypes.NullableDateTime fДатаРождения;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.tПол fПол;

        private string fПолеБС;

        private int fПорядковыйНомер;

        private string fЦветГлаз;

        private System.Nullable<System.DateTime> fCreateTime;

        private string fCreator;

        private string fEditor;

        private System.Nullable<System.DateTime> fEditTime;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Лес fЛесОбитания;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь fМама;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь fПапа;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.Страна fСтранаРождения;

        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога fБерлога;

        // *** Start programmer edit section *** (Медведь CustomMembers)

        // *** End programmer edit section *** (Медведь CustomMembers)


        /// <summary>
        /// Вес.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Вес CustomAttributes)

        // *** End programmer edit section *** (Медведь.Вес CustomAttributes)
        public virtual int Вес
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Вес Get start)

                // *** End programmer edit section *** (Медведь.Вес Get start)
                int result = this.fВес;
                // *** Start programmer edit section *** (Медведь.Вес Get end)

                // *** End programmer edit section *** (Медведь.Вес Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Вес Set start)

                // *** End programmer edit section *** (Медведь.Вес Set start)
                this.fВес = value;
                // *** Start programmer edit section *** (Медведь.Вес Set end)

                // *** End programmer edit section *** (Медведь.Вес Set end)
            }
        }

        /// <summary>
        /// ВычислимоеПоле.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ВычислимоеПоле CustomAttributes)

        // *** End programmer edit section *** (Медведь.ВычислимоеПоле CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.MSSQLDataService), "@ПорядковыйНомер@ + @Вес@")]
        public virtual int ВычислимоеПоле
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ВычислимоеПоле Get)
                return 0;
                // *** End programmer edit section *** (Медведь.ВычислимоеПоле Get)
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ВычислимоеПоле Set)

                // *** End programmer edit section *** (Медведь.ВычислимоеПоле Set)
            }
        }

        /// <summary>
        /// ДатаРождения.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ДатаРождения CustomAttributes)

        // *** End programmer edit section *** (Медведь.ДатаРождения CustomAttributes)
        public virtual ICSSoft.STORMNET.UserDataTypes.NullableDateTime ДатаРождения
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ДатаРождения Get start)

                // *** End programmer edit section *** (Медведь.ДатаРождения Get start)
                ICSSoft.STORMNET.UserDataTypes.NullableDateTime result = this.fДатаРождения;
                // *** Start programmer edit section *** (Медведь.ДатаРождения Get end)

                // *** End programmer edit section *** (Медведь.ДатаРождения Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ДатаРождения Set start)

                // *** End programmer edit section *** (Медведь.ДатаРождения Set start)
                this.fДатаРождения = value;
                // *** Start programmer edit section *** (Медведь.ДатаРождения Set end)

                // *** End programmer edit section *** (Медведь.ДатаРождения Set end)
            }
        }

        /// <summary>
        /// МедведьСтрокой.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.МедведьСтрокой CustomAttributes)

        // *** End programmer edit section *** (Медведь.МедведьСтрокой CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        [StrLen(255)]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.MSSQLDataService), "\'ПорядковыйНомер:\' + @ПорядковыйНомер@ + \", Цвет глаз мамы:\" + isnull(@Мама.ЦветГ" +
            "лаз@,\'\')")]
        public virtual string МедведьСтрокой
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.МедведьСтрокой Get)
                return null;
                // *** End programmer edit section *** (Медведь.МедведьСтрокой Get)
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.МедведьСтрокой Set)

                // *** End programmer edit section *** (Медведь.МедведьСтрокой Set)
            }
        }

        /// <summary>
        /// Пол.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Пол CustomAttributes)

        // *** End programmer edit section *** (Медведь.Пол CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.tПол Пол
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Пол Get start)

                // *** End programmer edit section *** (Медведь.Пол Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.tПол result = this.fПол;
                // *** Start programmer edit section *** (Медведь.Пол Get end)

                // *** End programmer edit section *** (Медведь.Пол Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Пол Set start)

                // *** End programmer edit section *** (Медведь.Пол Set start)
                this.fПол = value;
                // *** Start programmer edit section *** (Медведь.Пол Set end)

                // *** End programmer edit section *** (Медведь.Пол Set end)
            }
        }

        /// <summary>
        /// ПолеБС.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ПолеБС CustomAttributes)

        // *** End programmer edit section *** (Медведь.ПолеБС CustomAttributes)
        [StrLen(255)]
        public virtual string ПолеБС
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ПолеБС Get start)

                // *** End programmer edit section *** (Медведь.ПолеБС Get start)
                string result = this.fПолеБС;
                // *** Start programmer edit section *** (Медведь.ПолеБС Get end)

                // *** End programmer edit section *** (Медведь.ПолеБС Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ПолеБС Set start)

                // *** End programmer edit section *** (Медведь.ПолеБС Set start)
                this.fПолеБС = value;
                // *** Start programmer edit section *** (Медведь.ПолеБС Set end)

                // *** End programmer edit section *** (Медведь.ПолеБС Set end)
            }
        }

        /// <summary>
        /// ПорядковыйНомер.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ПорядковыйНомер CustomAttributes)

        // *** End programmer edit section *** (Медведь.ПорядковыйНомер CustomAttributes)
        public virtual int ПорядковыйНомер
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ПорядковыйНомер Get start)

                // *** End programmer edit section *** (Медведь.ПорядковыйНомер Get start)
                int result = this.fПорядковыйНомер;
                // *** Start programmer edit section *** (Медведь.ПорядковыйНомер Get end)

                // *** End programmer edit section *** (Медведь.ПорядковыйНомер Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ПорядковыйНомер Set start)

                // *** End programmer edit section *** (Медведь.ПорядковыйНомер Set start)
                this.fПорядковыйНомер = value;
                // *** Start programmer edit section *** (Медведь.ПорядковыйНомер Set end)

                // *** End programmer edit section *** (Медведь.ПорядковыйНомер Set end)
            }
        }

        /// <summary>
        /// ЦветГлаз.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ЦветГлаз CustomAttributes)

        // *** End programmer edit section *** (Медведь.ЦветГлаз CustomAttributes)
        [StrLen(255)]
        public virtual string ЦветГлаз
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ЦветГлаз Get start)

                // *** End programmer edit section *** (Медведь.ЦветГлаз Get start)
                string result = this.fЦветГлаз;
                // *** Start programmer edit section *** (Медведь.ЦветГлаз Get end)

                // *** End programmer edit section *** (Медведь.ЦветГлаз Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ЦветГлаз Set start)

                // *** End programmer edit section *** (Медведь.ЦветГлаз Set start)
                this.fЦветГлаз = value;
                // *** Start programmer edit section *** (Медведь.ЦветГлаз Set end)

                // *** End programmer edit section *** (Медведь.ЦветГлаз Set end)
            }
        }

        /// <summary>
        /// Время создания объекта.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.CreateTime CustomAttributes)

        // *** End programmer edit section *** (Медведь.CreateTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> CreateTime
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.CreateTime Get start)

                // *** End programmer edit section *** (Медведь.CreateTime Get start)
                System.Nullable<System.DateTime> result = this.fCreateTime;
                // *** Start programmer edit section *** (Медведь.CreateTime Get end)

                // *** End programmer edit section *** (Медведь.CreateTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.CreateTime Set start)

                // *** End programmer edit section *** (Медведь.CreateTime Set start)
                this.fCreateTime = value;
                // *** Start programmer edit section *** (Медведь.CreateTime Set end)

                // *** End programmer edit section *** (Медведь.CreateTime Set end)
            }
        }

        /// <summary>
        /// Создатель объекта.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Creator CustomAttributes)

        // *** End programmer edit section *** (Медведь.Creator CustomAttributes)
        [StrLen(255)]
        public virtual string Creator
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Creator Get start)

                // *** End programmer edit section *** (Медведь.Creator Get start)
                string result = this.fCreator;
                // *** Start programmer edit section *** (Медведь.Creator Get end)

                // *** End programmer edit section *** (Медведь.Creator Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Creator Set start)

                // *** End programmer edit section *** (Медведь.Creator Set start)
                this.fCreator = value;
                // *** Start programmer edit section *** (Медведь.Creator Set end)

                // *** End programmer edit section *** (Медведь.Creator Set end)
            }
        }

        /// <summary>
        /// Последний редактор объекта.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Editor CustomAttributes)

        // *** End programmer edit section *** (Медведь.Editor CustomAttributes)
        [StrLen(255)]
        public virtual string Editor
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Editor Get start)

                // *** End programmer edit section *** (Медведь.Editor Get start)
                string result = this.fEditor;
                // *** Start programmer edit section *** (Медведь.Editor Get end)

                // *** End programmer edit section *** (Медведь.Editor Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Editor Set start)

                // *** End programmer edit section *** (Медведь.Editor Set start)
                this.fEditor = value;
                // *** Start programmer edit section *** (Медведь.Editor Set end)

                // *** End programmer edit section *** (Медведь.Editor Set end)
            }
        }

        /// <summary>
        /// Время последнего редактирования объекта.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.EditTime CustomAttributes)

        // *** End programmer edit section *** (Медведь.EditTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> EditTime
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.EditTime Get start)

                // *** End programmer edit section *** (Медведь.EditTime Get start)
                System.Nullable<System.DateTime> result = this.fEditTime;
                // *** Start programmer edit section *** (Медведь.EditTime Get end)

                // *** End programmer edit section *** (Медведь.EditTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.EditTime Set start)

                // *** End programmer edit section *** (Медведь.EditTime Set start)
                this.fEditTime = value;
                // *** Start programmer edit section *** (Медведь.EditTime Set end)

                // *** End programmer edit section *** (Медведь.EditTime Set end)
            }
        }

        /// <summary>
        /// Медведь
        ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.ЛесОбитания CustomAttributes)

        // *** End programmer edit section *** (Медведь.ЛесОбитания CustomAttributes)
        [PropertyStorage(new string[] {
                "ЛесОбитания"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Лес ЛесОбитания
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.ЛесОбитания Get start)

                // *** End programmer edit section *** (Медведь.ЛесОбитания Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Лес result = this.fЛесОбитания;
                // *** Start programmer edit section *** (Медведь.ЛесОбитания Get end)

                // *** End programmer edit section *** (Медведь.ЛесОбитания Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.ЛесОбитания Set start)

                // *** End programmer edit section *** (Медведь.ЛесОбитания Set start)
                this.fЛесОбитания = value;
                // *** Start programmer edit section *** (Медведь.ЛесОбитания Set end)

                // *** End programmer edit section *** (Медведь.ЛесОбитания Set end)
            }
        }

        /// <summary>
        /// Медведь
        ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Мама CustomAttributes)

        // *** End programmer edit section *** (Медведь.Мама CustomAttributes)
        [PropertyStorage(new string[] {
                "Мама"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь Мама
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Мама Get start)

                // *** End programmer edit section *** (Медведь.Мама Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь result = this.fМама;
                // *** Start programmer edit section *** (Медведь.Мама Get end)

                // *** End programmer edit section *** (Медведь.Мама Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Мама Set start)

                // *** End programmer edit section *** (Медведь.Мама Set start)
                this.fМама = value;
                // *** Start programmer edit section *** (Медведь.Мама Set end)

                // *** End programmer edit section *** (Медведь.Мама Set end)
            }
        }

        /// <summary>
        /// Медведь
        ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Папа CustomAttributes)

        // *** End programmer edit section *** (Медведь.Папа CustomAttributes)
        [PropertyStorage(new string[] {
                "Папа"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь Папа
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Папа Get start)

                // *** End programmer edit section *** (Медведь.Папа Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь result = this.fПапа;
                // *** Start programmer edit section *** (Медведь.Папа Get end)

                // *** End programmer edit section *** (Медведь.Папа Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Папа Set start)

                // *** End programmer edit section *** (Медведь.Папа Set start)
                this.fПапа = value;
                // *** Start programmer edit section *** (Медведь.Папа Set end)

                // *** End programmer edit section *** (Медведь.Папа Set end)
            }
        }

        /// <summary>
        /// Медведь
        ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.СтранаРождения CustomAttributes)

        // *** End programmer edit section *** (Медведь.СтранаРождения CustomAttributes)
        [PropertyStorage(new string[] {
                "Страна"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Страна СтранаРождения
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.СтранаРождения Get start)

                // *** End programmer edit section *** (Медведь.СтранаРождения Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Страна result = this.fСтранаРождения;
                // *** Start programmer edit section *** (Медведь.СтранаРождения Get end)

                // *** End programmer edit section *** (Медведь.СтранаРождения Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.СтранаРождения Set start)

                // *** End programmer edit section *** (Медведь.СтранаРождения Set start)
                this.fСтранаРождения = value;
                // *** Start programmer edit section *** (Медведь.СтранаРождения Set end)

                // *** End programmer edit section *** (Медведь.СтранаРождения Set end)
            }
        }

        /// <summary>
        /// Медведь
        ///Аудит включен для тестирования оффлайн-сервиса аудита в OData.
        /// </summary>
        // *** Start programmer edit section *** (Медведь.Берлога CustomAttributes)

        // *** End programmer edit section *** (Медведь.Берлога CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога Берлога
        {
            get
            {
                // *** Start programmer edit section *** (Медведь.Берлога Get start)

                // *** End programmer edit section *** (Медведь.Берлога Get start)
                if ((this.fБерлога == null))
                {
                    this.fБерлога = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога result = this.fБерлога;
                // *** Start programmer edit section *** (Медведь.Берлога Get end)

                // *** End programmer edit section *** (Медведь.Берлога Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Медведь.Берлога Set start)

                // *** End programmer edit section *** (Медведь.Берлога Set start)
                this.fБерлога = value;
                // *** Start programmer edit section *** (Медведь.Берлога Set end)

                // *** End programmer edit section *** (Медведь.Берлога Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// Представление для работы тестов на загрузку объектов.
            /// </summary>
            public static ICSSoft.STORMNET.View LoadTestView
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("LoadTestView", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }

            /// <summary>
            /// Представление для работы теста по использованию порядкового номера.
            /// </summary>
            public static ICSSoft.STORMNET.View OrderNumberTest
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("OrderNumberTest", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }

            /// <summary>
            /// "МедведьE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View МедведьE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("МедведьE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }

            /// <summary>
            /// "МедведьL" view.
            /// </summary>
            public static ICSSoft.STORMNET.View МедведьL
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("МедведьL", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }

            /// <summary>
            /// "МедведьShort" view.
            /// </summary>
            public static ICSSoft.STORMNET.View МедведьShort
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("МедведьShort", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }

            /// <summary>
            /// "МедведьСДелейломИВычислимымСвойством" view.
            /// </summary>
            public static ICSSoft.STORMNET.View МедведьСДелейломИВычислимымСвойством
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("МедведьСДелейломИВычислимымСвойством", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь));
                }
            }
        }

        /// <summary>
        /// Audit class settings.
        /// </summary>
        public class AuditSettings
        {

            /// <summary>
            /// Включён ли аудит для класса.
            /// </summary>
            public static bool AuditEnabled = true;

            /// <summary>
            /// Использовать имя представления для аудита по умолчанию.
            /// </summary>
            public static bool UseDefaultView = true;

            /// <summary>
            /// Включён ли аудит операции чтения.
            /// </summary>
            public static bool SelectAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции чтения.
            /// </summary>
            public static string SelectAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции создания.
            /// </summary>
            public static bool InsertAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции создания.
            /// </summary>
            public static string InsertAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции изменения.
            /// </summary>
            public static bool UpdateAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции изменения.
            /// </summary>
            public static string UpdateAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции удаления.
            /// </summary>
            public static bool DeleteAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции удаления.
            /// </summary>
            public static string DeleteAuditViewName = "AuditView";

            /// <summary>
            /// Путь к форме просмотра результатов аудита.
            /// </summary>
            public static string FormUrl = "";

            /// <summary>
            /// Режим записи данных аудита (синхронный или асинхронный).
            /// </summary>
            public static ICSSoft.STORMNET.Business.Audit.Objects.tWriteMode WriteMode = ICSSoft.STORMNET.Business.Audit.Objects.tWriteMode.Synchronous;

            /// <summary>
            /// Максимальная длина сохраняемого значения поля (если 0, то строка обрезаться не будет).
            /// </summary>
            public static int PrunningLength = 0;

            /// <summary>
            /// Показывать ли пользователям в изменениях первичные ключи.
            /// </summary>
            public static bool ShowPrimaryKey = false;

            /// <summary>
            /// Сохранять ли старое значение.
            /// </summary>
            public static bool KeepOldValue = true;

            /// <summary>
            /// Сжимать ли сохраняемые значения.
            /// </summary>
            public static bool Compress = false;

            /// <summary>
            /// Сохранять ли все значения атрибутов, а не только изменяемые.
            /// </summary>
            public static bool KeepAllValues = false;
        }
    }
}

