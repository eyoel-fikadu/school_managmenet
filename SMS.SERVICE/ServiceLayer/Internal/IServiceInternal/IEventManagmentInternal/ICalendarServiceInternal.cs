using System;
using System.Collections.Generic;
using System.Text;
using SCMS.DataAccess;
using SMS.SERVICE.DTO.EventDTO;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IEventManagmentInternal
{
    public interface ICalendarServiceInternal
    {
        #region Event
        Event AddEvent(EventModel eventModel);
        Event UpdateEvent(EventModel eventModel);
        #endregion
    }
}
