using SMS.SERVICE.DTO.EventDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IEventManagement
{
    public interface ICalendarService
    {
        #region Event
        EventModel CreateEvent(EventModel eventModel);
        EventModel UpdateEvent(EventModel eventModel);
        #endregion
    }
}
