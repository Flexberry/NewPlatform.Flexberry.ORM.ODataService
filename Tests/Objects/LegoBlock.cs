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
    /// LegoBlock.
    /// </summary>
    // *** Start programmer edit section *** (LegoBlock CustomAttributes)

    // *** End programmer edit section *** (LegoBlock CustomAttributes)
    [PublishName("LegoBlock")]
    [AutoAltered()]
    [ICSSoft.STORMNET.NotStored(false)]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class LegoBlock : NewPlatform.Flexberry.ORM.ODataService.Tests.BaseLegoBlock
    {
        
        private int fWidth;
        
        private int fHeight;
        
        private int fDepth;
        
        private string fConfiguration;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.LegoMaterial fMaterial;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockBottomPanel fBottomPanels;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockCustomPanel fCustomPanels;
        
        private NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel fTopPanels;

        // *** Start programmer edit section *** (LegoBlock CustomMembers)

        [NotStored]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.PostgresDataService), "'Association'")]
        public override string AssocType
        {
            get
            {
                return base.AssocType;
            }
        }

        // *** End programmer edit section *** (LegoBlock CustomMembers)


        /// <summary>
        /// Width.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.Width CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.Width CustomAttributes)
        public virtual int Width
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.Width Get start)

                // *** End programmer edit section *** (LegoBlock.Width Get start)
                int result = this.fWidth;
                // *** Start programmer edit section *** (LegoBlock.Width Get end)

                // *** End programmer edit section *** (LegoBlock.Width Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.Width Set start)

                // *** End programmer edit section *** (LegoBlock.Width Set start)
                this.fWidth = value;
                // *** Start programmer edit section *** (LegoBlock.Width Set end)

                // *** End programmer edit section *** (LegoBlock.Width Set end)
            }
        }
        
        /// <summary>
        /// Height.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.Height CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.Height CustomAttributes)
        public virtual int Height
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.Height Get start)

                // *** End programmer edit section *** (LegoBlock.Height Get start)
                int result = this.fHeight;
                // *** Start programmer edit section *** (LegoBlock.Height Get end)

                // *** End programmer edit section *** (LegoBlock.Height Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.Height Set start)

                // *** End programmer edit section *** (LegoBlock.Height Set start)
                this.fHeight = value;
                // *** Start programmer edit section *** (LegoBlock.Height Set end)

                // *** End programmer edit section *** (LegoBlock.Height Set end)
            }
        }
        
        /// <summary>
        /// Depth.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.Depth CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.Depth CustomAttributes)
        public virtual int Depth
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.Depth Get start)

                // *** End programmer edit section *** (LegoBlock.Depth Get start)
                int result = this.fDepth;
                // *** Start programmer edit section *** (LegoBlock.Depth Get end)

                // *** End programmer edit section *** (LegoBlock.Depth Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.Depth Set start)

                // *** End programmer edit section *** (LegoBlock.Depth Set start)
                this.fDepth = value;
                // *** Start programmer edit section *** (LegoBlock.Depth Set end)

                // *** End programmer edit section *** (LegoBlock.Depth Set end)
            }
        }
        
        /// <summary>
        /// Configuration.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.Configuration CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.Configuration CustomAttributes)
        [StrLen(255)]
        public virtual string Configuration
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.Configuration Get start)

                // *** End programmer edit section *** (LegoBlock.Configuration Get start)
                string result = this.fConfiguration;
                // *** Start programmer edit section *** (LegoBlock.Configuration Get end)

                // *** End programmer edit section *** (LegoBlock.Configuration Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.Configuration Set start)

                // *** End programmer edit section *** (LegoBlock.Configuration Set start)
                this.fConfiguration = value;
                // *** Start programmer edit section *** (LegoBlock.Configuration Set end)

                // *** End programmer edit section *** (LegoBlock.Configuration Set end)
            }
        }
        
        /// <summary>
        /// LegoBlock.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.Material CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.Material CustomAttributes)
        [PropertyStorage(new string[] {
                "Material"})]
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.LegoMaterial Material
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.Material Get start)

                // *** End programmer edit section *** (LegoBlock.Material Get start)
                NewPlatform.Flexberry.ORM.ODataService.Tests.LegoMaterial result = this.fMaterial;
                // *** Start programmer edit section *** (LegoBlock.Material Get end)

                // *** End programmer edit section *** (LegoBlock.Material Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.Material Set start)

                // *** End programmer edit section *** (LegoBlock.Material Set start)
                this.fMaterial = value;
                // *** Start programmer edit section *** (LegoBlock.Material Set end)

                // *** End programmer edit section *** (LegoBlock.Material Set end)
            }
        }
        
        /// <summary>
        /// LegoBlock.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.BottomPanels CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.BottomPanels CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockBottomPanel BottomPanels
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.BottomPanels Get start)

                // *** End programmer edit section *** (LegoBlock.BottomPanels Get start)
                if ((this.fBottomPanels == null))
                {
                    this.fBottomPanels = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockBottomPanel(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockBottomPanel result = this.fBottomPanels;
                // *** Start programmer edit section *** (LegoBlock.BottomPanels Get end)

                // *** End programmer edit section *** (LegoBlock.BottomPanels Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.BottomPanels Set start)

                // *** End programmer edit section *** (LegoBlock.BottomPanels Set start)
                this.fBottomPanels = value;
                // *** Start programmer edit section *** (LegoBlock.BottomPanels Set end)

                // *** End programmer edit section *** (LegoBlock.BottomPanels Set end)
            }
        }
        
        /// <summary>
        /// LegoBlock.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.CustomPanels CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.CustomPanels CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockCustomPanel CustomPanels
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.CustomPanels Get start)

                // *** End programmer edit section *** (LegoBlock.CustomPanels Get start)
                if ((this.fCustomPanels == null))
                {
                    this.fCustomPanels = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockCustomPanel(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockCustomPanel result = this.fCustomPanels;
                // *** Start programmer edit section *** (LegoBlock.CustomPanels Get end)

                // *** End programmer edit section *** (LegoBlock.CustomPanels Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.CustomPanels Set start)

                // *** End programmer edit section *** (LegoBlock.CustomPanels Set start)
                this.fCustomPanels = value;
                // *** Start programmer edit section *** (LegoBlock.CustomPanels Set end)

                // *** End programmer edit section *** (LegoBlock.CustomPanels Set end)
            }
        }
        
        /// <summary>
        /// LegoBlock.
        /// </summary>
        // *** Start programmer edit section *** (LegoBlock.TopPanels CustomAttributes)

        // *** End programmer edit section *** (LegoBlock.TopPanels CustomAttributes)
        public virtual NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel TopPanels
        {
            get
            {
                // *** Start programmer edit section *** (LegoBlock.TopPanels Get start)

                // *** End programmer edit section *** (LegoBlock.TopPanels Get start)
                if ((this.fTopPanels == null))
                {
                    this.fTopPanels = new NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel(this);
                }
                NewPlatform.Flexberry.ORM.ODataService.Tests.DetailArrayOfLegoBlockTopPanel result = this.fTopPanels;
                // *** Start programmer edit section *** (LegoBlock.TopPanels Get end)

                // *** End programmer edit section *** (LegoBlock.TopPanels Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LegoBlock.TopPanels Set start)

                // *** End programmer edit section *** (LegoBlock.TopPanels Set start)
                this.fTopPanels = value;
                // *** Start programmer edit section *** (LegoBlock.TopPanels Set end)

                // *** End programmer edit section *** (LegoBlock.TopPanels Set end)
            }
        }
    }
}
