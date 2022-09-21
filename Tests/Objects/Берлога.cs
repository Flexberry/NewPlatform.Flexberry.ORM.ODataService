﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Xml;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET;
    
    
    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Берлога.
    /// </summary>
    // *** Start programmer edit section *** (Берлога CustomAttributes)

    // *** End programmer edit section *** (Берлога CustomAttributes)
    [BusinessServer("NewPlatform.Flexberry.ORM.ODataService.Tests.DenBS, NewPlatform.Flexberry.ORM.ODa" +
        "taService.Tests.BusinessServers", ICSSoft.STORMNET.Business.DataServiceObjectEvents.OnAllEvents)]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("LoadTestView", new string[] {
            "Наименование"})]
    [View("БерлогаE", new string[] {
            "Наименование as \'Наименование\'",
            "Комфортность as \'Комфортность\'",
            "Заброшена as \'Заброшена\'",
            "ЛесРасположения as \'Лес расположения\'",
            "ЛесРасположения.Название as \'Название\'",
            "Медведь as \'Медведь\'",
            "ПолеБС",
            "Сертификат",
            "СертификатСтрока"}, Hidden=new string[] {
            "ЛесРасположения.Название",
            "Медведь",
            "Сертификат",
            "СертификатСтрока"})]
    [MasterViewDefineAttribute("БерлогаE", "ЛесРасположения", ICSSoft.STORMNET.LookupTypeEnum.Standard, "", "Название")]
    public class Берлога : ICSSoft.STORMNET.DataObject
    {
        
        private string fПолеБС;
        
        private string fНаименование;
        
        private int fКомфортность;
        
        private bool fЗаброшена;
        
        private ICSSoft.STORMNET.UserDataTypes.WebFile fСертификат;
        
        private string fСертификатСтрока;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.Лес fЛесРасположения;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.Порода fПодходитДляПороды;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь fМедведь;
        
        // *** Start programmer edit section *** (Берлога CustomMembers)

        // *** End programmer edit section *** (Берлога CustomMembers)

        
        /// <summary>
        /// ПолеБС.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.ПолеБС CustomAttributes)

        // *** End programmer edit section *** (Берлога.ПолеБС CustomAttributes)
        [StrLen(255)]
        public virtual string ПолеБС
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.ПолеБС Get start)

                // *** End programmer edit section *** (Берлога.ПолеБС Get start)
                string result = this.fПолеБС;
                // *** Start programmer edit section *** (Берлога.ПолеБС Get end)

                // *** End programmer edit section *** (Берлога.ПолеБС Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.ПолеБС Set start)

                // *** End programmer edit section *** (Берлога.ПолеБС Set start)
                this.fПолеБС = value;
                // *** Start programmer edit section *** (Берлога.ПолеБС Set end)

                // *** End programmer edit section *** (Берлога.ПолеБС Set end)
            }
        }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.Наименование CustomAttributes)

        // *** End programmer edit section *** (Берлога.Наименование CustomAttributes)
        [StrLen(255)]
        public virtual string Наименование
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.Наименование Get start)

                // *** End programmer edit section *** (Берлога.Наименование Get start)
                string result = this.fНаименование;
                // *** Start programmer edit section *** (Берлога.Наименование Get end)

                // *** End programmer edit section *** (Берлога.Наименование Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.Наименование Set start)

                // *** End programmer edit section *** (Берлога.Наименование Set start)
                this.fНаименование = value;
                // *** Start programmer edit section *** (Берлога.Наименование Set end)

                // *** End programmer edit section *** (Берлога.Наименование Set end)
            }
        }
        
        /// <summary>
        /// Комфортность.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.Комфортность CustomAttributes)

        // *** End programmer edit section *** (Берлога.Комфортность CustomAttributes)
        public virtual int Комфортность
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.Комфортность Get start)

                // *** End programmer edit section *** (Берлога.Комфортность Get start)
                int result = this.fКомфортность;
                // *** Start programmer edit section *** (Берлога.Комфортность Get end)

                // *** End programmer edit section *** (Берлога.Комфортность Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.Комфортность Set start)

                // *** End programmer edit section *** (Берлога.Комфортность Set start)
                this.fКомфортность = value;
                // *** Start programmer edit section *** (Берлога.Комфортность Set end)

                // *** End programmer edit section *** (Берлога.Комфортность Set end)
            }
        }
        
        /// <summary>
        /// Заброшена.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.Заброшена CustomAttributes)

        // *** End programmer edit section *** (Берлога.Заброшена CustomAttributes)
        public virtual bool Заброшена
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.Заброшена Get start)

                // *** End programmer edit section *** (Берлога.Заброшена Get start)
                bool result = this.fЗаброшена;
                // *** Start programmer edit section *** (Берлога.Заброшена Get end)

                // *** End programmer edit section *** (Берлога.Заброшена Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.Заброшена Set start)

                // *** End programmer edit section *** (Берлога.Заброшена Set start)
                this.fЗаброшена = value;
                // *** Start programmer edit section *** (Берлога.Заброшена Set end)

                // *** End programmer edit section *** (Берлога.Заброшена Set end)
            }
        }
        
        /// <summary>
        /// Сертификат.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.Сертификат CustomAttributes)

        // *** End programmer edit section *** (Берлога.Сертификат CustomAttributes)
        public virtual ICSSoft.STORMNET.UserDataTypes.WebFile Сертификат
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.Сертификат Get start)

                // *** End programmer edit section *** (Берлога.Сертификат Get start)
                ICSSoft.STORMNET.UserDataTypes.WebFile result = this.fСертификат;
                // *** Start programmer edit section *** (Берлога.Сертификат Get end)

                // *** End programmer edit section *** (Берлога.Сертификат Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.Сертификат Set start)

                // *** End programmer edit section *** (Берлога.Сертификат Set start)
                this.fСертификат = value;
                // *** Start programmer edit section *** (Берлога.Сертификат Set end)

                // *** End programmer edit section *** (Берлога.Сертификат Set end)
            }
        }
        
        /// <summary>
        /// СертификатСтрока.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.СертификатСтрока CustomAttributes)

        // *** End programmer edit section *** (Берлога.СертификатСтрока CustomAttributes)
        [PropertyStorage("CertString")]
        public virtual string СертификатСтрока
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.СертификатСтрока Get start)

                // *** End programmer edit section *** (Берлога.СертификатСтрока Get start)
                string result = this.fСертификатСтрока;
                // *** Start programmer edit section *** (Берлога.СертификатСтрока Get end)

                // *** End programmer edit section *** (Берлога.СертификатСтрока Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.СертификатСтрока Set start)

                // *** End programmer edit section *** (Берлога.СертификатСтрока Set start)
                this.fСертификатСтрока = value;
                // *** Start programmer edit section *** (Берлога.СертификатСтрока Set end)

                // *** End programmer edit section *** (Берлога.СертификатСтрока Set end)
            }
        }
        
        /// <summary>
        /// Берлога.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.ЛесРасположения CustomAttributes)

        // *** End programmer edit section *** (Берлога.ЛесРасположения CustomAttributes)
        [PropertyStorage(new string[] {
                "ЛесРасположения"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Лес ЛесРасположения
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.ЛесРасположения Get start)

                // *** End programmer edit section *** (Берлога.ЛесРасположения Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Лес result = this.fЛесРасположения;
                // *** Start programmer edit section *** (Берлога.ЛесРасположения Get end)

                // *** End programmer edit section *** (Берлога.ЛесРасположения Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.ЛесРасположения Set start)

                // *** End programmer edit section *** (Берлога.ЛесРасположения Set start)
                this.fЛесРасположения = value;
                // *** Start programmer edit section *** (Берлога.ЛесРасположения Set end)

                // *** End programmer edit section *** (Берлога.ЛесРасположения Set end)
            }
        }
        
        /// <summary>
        /// Берлога.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.ПодходитДляПороды CustomAttributes)

        // *** End programmer edit section *** (Берлога.ПодходитДляПороды CustomAttributes)
        [PropertyStorage(new string[] {
                "ДляКакойПороды"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Порода ПодходитДляПороды
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.ПодходитДляПороды Get start)

                // *** End programmer edit section *** (Берлога.ПодходитДляПороды Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Порода result = this.fПодходитДляПороды;
                // *** Start programmer edit section *** (Берлога.ПодходитДляПороды Get end)

                // *** End programmer edit section *** (Берлога.ПодходитДляПороды Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.ПодходитДляПороды Set start)

                // *** End programmer edit section *** (Берлога.ПодходитДляПороды Set start)
                this.fПодходитДляПороды = value;
                // *** Start programmer edit section *** (Берлога.ПодходитДляПороды Set end)

                // *** End programmer edit section *** (Берлога.ПодходитДляПороды Set end)
            }
        }
        
        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь.
        /// </summary>
        // *** Start programmer edit section *** (Берлога.Медведь CustomAttributes)

        // *** End programmer edit section *** (Берлога.Медведь CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "Медведь"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь Медведь
        {
            get
            {
                // *** Start programmer edit section *** (Берлога.Медведь Get start)

                // *** End programmer edit section *** (Берлога.Медведь Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь result = this.fМедведь;
                // *** Start programmer edit section *** (Берлога.Медведь Get end)

                // *** End programmer edit section *** (Берлога.Медведь Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Берлога.Медведь Set start)

                // *** End programmer edit section *** (Берлога.Медведь Set start)
                this.fМедведь = value;
                // *** Start programmer edit section *** (Берлога.Медведь Set end)

                // *** End programmer edit section *** (Берлога.Медведь Set end)
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
                    return ICSSoft.STORMNET.Information.GetView("LoadTestView", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога));
                }
            }
            
            /// <summary>
            /// "БерлогаE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View БерлогаE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("БерлогаE", typeof(NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога));
                }
            }
        }
    }
    
    /// <summary>
    /// Detail array of Берлога.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfБерлога CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfБерлога CustomAttributes)
    public class DetailArrayOfБерлога : ICSSoft.STORMNET.DetailArray
    {
        
        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfБерлога members)

        
        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Берлога by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Берлога.
        /// </summary>
        public DetailArrayOfБерлога(NewPlatform.Flexberry.ORM.ODataService.Tests.Медведь fМедведь) : 
                base(typeof(Берлога), ((ICSSoft.STORMNET.DataObject)(fМедведь)))
        {
        }
        
        public NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога)(this.ItemByIndex(index)));
            }
        }
        
        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}
