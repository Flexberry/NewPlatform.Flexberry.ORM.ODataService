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
    /// LegoBlockTopPanel.
    /// </summary>
    // *** Start programmer edit section *** (LegoBlockTopPanel CustomAttributes)

    // *** End programmer edit section *** (LegoBlockTopPanel CustomAttributes)
    [PublishName("LegoBlockTopPanel")]
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class LegoBlockTopPanel : ICSSoft.STORMNET.DataObject
    {
        
        private int fWidthCount;
        
        private int fHeightCount;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.LegoSocketStandard fSocketStandard;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanelHole fHoles;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlock fBlock;
        
        // *** Start programmer edit section *** (LegoBlockTopPanel CustomMembers)

        // *** End programmer edit section *** (LegoBlockTopPanel CustomMembers)

        
        /// <summary>
        /// WidthCount.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockTopPanel.WidthCount CustomAttributes)

        // *** End programmer edit section *** (LegoBlockTopPanel.WidthCount CustomAttributes)
        public virtual int WidthCount
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.WidthCount Get start)

                // *** End programmer edit section *** (LegoBlockTopPanel.WidthCount Get start)
                int result = this.fWidthCount;
                // *** Start programmer edit section *** (LegoBlockTopPanel.WidthCount Get end)

                // *** End programmer edit section *** (LegoBlockTopPanel.WidthCount Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.WidthCount Set start)

                // *** End programmer edit section *** (LegoBlockTopPanel.WidthCount Set start)
                this.fWidthCount = value;
                // *** Start programmer edit section *** (LegoBlockTopPanel.WidthCount Set end)

                // *** End programmer edit section *** (LegoBlockTopPanel.WidthCount Set end)
            }
        }
        
        /// <summary>
        /// HeightCount.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockTopPanel.HeightCount CustomAttributes)

        // *** End programmer edit section *** (LegoBlockTopPanel.HeightCount CustomAttributes)
        public virtual int HeightCount
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.HeightCount Get start)

                // *** End programmer edit section *** (LegoBlockTopPanel.HeightCount Get start)
                int result = this.fHeightCount;
                // *** Start programmer edit section *** (LegoBlockTopPanel.HeightCount Get end)

                // *** End programmer edit section *** (LegoBlockTopPanel.HeightCount Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.HeightCount Set start)

                // *** End programmer edit section *** (LegoBlockTopPanel.HeightCount Set start)
                this.fHeightCount = value;
                // *** Start programmer edit section *** (LegoBlockTopPanel.HeightCount Set end)

                // *** End programmer edit section *** (LegoBlockTopPanel.HeightCount Set end)
            }
        }
        
        /// <summary>
        /// LegoBlockTopPanel.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockTopPanel.SocketStandard CustomAttributes)

        // *** End programmer edit section *** (LegoBlockTopPanel.SocketStandard CustomAttributes)
        [PropertyStorage(new string[] {
                "SocketStandard"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.LegoSocketStandard SocketStandard
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.SocketStandard Get start)

                // *** End programmer edit section *** (LegoBlockTopPanel.SocketStandard Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.LegoSocketStandard result = this.fSocketStandard;
                // *** Start programmer edit section *** (LegoBlockTopPanel.SocketStandard Get end)

                // *** End programmer edit section *** (LegoBlockTopPanel.SocketStandard Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.SocketStandard Set start)

                // *** End programmer edit section *** (LegoBlockTopPanel.SocketStandard Set start)
                this.fSocketStandard = value;
                // *** Start programmer edit section *** (LegoBlockTopPanel.SocketStandard Set end)

                // *** End programmer edit section *** (LegoBlockTopPanel.SocketStandard Set end)
            }
        }
        
        /// <summary>
        /// LegoBlockTopPanel.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockTopPanel.Holes CustomAttributes)

        // *** End programmer edit section *** (LegoBlockTopPanel.Holes CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanelHole Holes
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.Holes Get start)

                // *** End programmer edit section *** (LegoBlockTopPanel.Holes Get start)
                if ((this.fHoles == null))
                {
                    this.fHoles = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanelHole(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanelHole result = this.fHoles;
                // *** Start programmer edit section *** (LegoBlockTopPanel.Holes Get end)

                // *** End programmer edit section *** (LegoBlockTopPanel.Holes Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.Holes Set start)

                // *** End programmer edit section *** (LegoBlockTopPanel.Holes Set start)
                this.fHoles = value;
                // *** Start programmer edit section *** (LegoBlockTopPanel.Holes Set end)

                // *** End programmer edit section *** (LegoBlockTopPanel.Holes Set end)
            }
        }
        
        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlock.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlockTopPanel.Block CustomAttributes)

        // *** End programmer edit section *** (LegoBlockTopPanel.Block CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "Block"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlock Block
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.Block Get start)

                // *** End programmer edit section *** (LegoBlockTopPanel.Block Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlock result = this.fBlock;
                // *** Start programmer edit section *** (LegoBlockTopPanel.Block Get end)

                // *** End programmer edit section *** (LegoBlockTopPanel.Block Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlockTopPanel.Block Set start)

                // *** End programmer edit section *** (LegoBlockTopPanel.Block Set start)
                this.fBlock = value;
                // *** Start programmer edit section *** (LegoBlockTopPanel.Block Set end)

                // *** End programmer edit section *** (LegoBlockTopPanel.Block Set end)
            }
        }
    }
    
    /// <summary>
    /// Detail array of LegoBlockTopPanel.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfLegoBlockTopPanel CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfLegoBlockTopPanel CustomAttributes)
    public class DetailArrayOfLegoBlockTopPanel : ICSSoft.STORMNET.DetailArray
    {
        
        // *** Start programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel members)

        
        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type LegoBlockTopPanel by index.
        /// </summary>
        /// <summary>
        /// Adds object with type LegoBlockTopPanel.
        /// </summary>
        public DetailArrayOfLegoBlockTopPanel(NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlock fLegoBlock) : 
                base(typeof(LegoBlockTopPanel), ((ICSSoft.STORMNET.DataObject)(fLegoBlock)))
        {
        }
        
        public NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlockTopPanel this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlockTopPanel)(this.ItemByIndex(index)));
            }
        }
        
        public virtual void Add(NewPlatform.Flexberry.ORM.ODataService.Tests.LegoBlockTopPanel dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}
