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
    using ICSSoft.STORMNET;
    
    
    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Child.
    /// </summary>
    // *** Start programmer edit section *** (Child CustomAttributes)

    // *** End programmer edit section *** (Child CustomAttributes)
    [PublishName("Child")]
    [AutoAltered()]
    [ICSSoft.STORMNET.NotStored()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class Child : ICSSoft.STORMNET.DataObject
    {
        
        private string fName;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.Person fParent;
        
        // *** Start programmer edit section *** (Child CustomMembers)

        // *** End programmer edit section *** (Child CustomMembers)

        
        /// <summary>
        /// Name.
        /// </summary>
        // *** Start programmer edit section *** (Child.Name CustomAttributes)

        // *** End programmer edit section *** (Child.Name CustomAttributes)
        [StrLen(255)]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (Child.Name Get start)

                // *** End programmer edit section *** (Child.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (Child.Name Get end)

                // *** End programmer edit section *** (Child.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Child.Name Set start)

                // *** End programmer edit section *** (Child.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (Child.Name Set end)

                // *** End programmer edit section *** (Child.Name Set end)
            }
        }
        
        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.Person.
        /// </summary>
        // *** Start programmer edit section *** (Child.Parent CustomAttributes)

        // *** End programmer edit section *** (Child.Parent CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "Parent"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.Person Parent
        {
            get
            {
                // *** Start programmer edit section *** (Child.Parent Get start)

                // *** End programmer edit section *** (Child.Parent Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.Person result = this.fParent;
                // *** Start programmer edit section *** (Child.Parent Get end)

                // *** End programmer edit section *** (Child.Parent Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Child.Parent Set start)

                // *** End programmer edit section *** (Child.Parent Set start)
                this.fParent = value;
                // *** Start programmer edit section *** (Child.Parent Set end)

                // *** End programmer edit section *** (Child.Parent Set end)
            }
        }
    }
    
    /// <summary>
    /// Detail array of Child.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfChild CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfChild CustomAttributes)
    public class DetailArrayOfChild : ICSSoft.STORMNET.DetailArray
    {
        
        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfChild members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfChild members)

        
        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type Child by index.
        /// </summary>
        /// <summary>
        /// Adds object with type Child.
        /// </summary>
        public DetailArrayOfChild(NewPlatform.Flexberry.ORM.ODataService.Tests.Person fPerson) : 
                base(typeof(Child), ((ICSSoft.STORMNET.DataObject)(fPerson)))
        {
        }
        
        public NewPlatform.Flexberry.ORM.ODataService.Tests.Child this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.Child)(this.ItemByIndex(index)));
            }
        }
        
        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.Child dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}