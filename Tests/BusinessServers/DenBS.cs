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


    // *** Start programmer edit section *** (Using statements)
    using ICSSoft.STORMNET;

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// DenBS.
    /// </summary>
    // *** Start programmer edit section *** (DenBS CustomAttributes)

    // *** End programmer edit section *** (DenBS CustomAttributes)
    [ICSSoft.STORMNET.AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class DenBS : ICSSoft.STORMNET.Business.BusinessServer
    {

        // *** Start programmer edit section *** (DenBS CustomMembers)

        // *** End programmer edit section *** (DenBS CustomMembers)


        // *** Start programmer edit section *** (OnUpdateБерлога CustomAttributes)

        // *** End programmer edit section *** (OnUpdateБерлога CustomAttributes)
        public virtual ICSSoft.STORMNET.DataObject[] OnUpdateБерлога(NewPlatform.Flexberry.ORM.ODataService.Tests.Берлога UpdatedObject)
        {
            // *** Start programmer edit section *** (OnUpdateБерлога)
            if (UpdatedObject.GetStatus() == ObjectStatus.Created)
            {
                UpdatedObject.ПолеБС = "Object created.";
            }
            else if (UpdatedObject.GetStatus() == ObjectStatus.Altered)
            {
                if (UpdatedObject.IsAlteredProperty(x => x.ЛесРасположения) && UpdatedObject.ЛесРасположения != null)
                {
                    UpdatedObject.ПолеБС = $"Берлога расположена в {UpdatedObject.ЛесРасположения.Название}";
                }
            }

            return new DataObject[0];
            // *** End programmer edit section *** (OnUpdateБерлога)
        }
    }
}