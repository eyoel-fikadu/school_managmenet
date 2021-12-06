using SCMS.DataAccess;
using SMS.SERVICE.DTO.EventDTO;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IEventManagmentInternal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.EventManagementInternal
{
    public class CalendarServiceInternal : ICalendarServiceInternal
    {
        public Event AddEvent(EventModel eventModel)
        {
            Event @event = new Event()
            {
                CreatedDate = DateTime.Now,
                EndDate = eventModel.EndDate,
                EndTime = eventModel.EndTime.TimeOfDay,
                EventName = eventModel.EventName,
                EventTypeId = eventModel.EventTypeId,
                StartDate = eventModel.StartDate,
                StartTime = eventModel.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now
            };
            return @event;
        }

        public Event UpdateEvent(EventModel eventModel)
        {
            Event @event = new Event()
            {
                EventId = eventModel.EventId,
                EndDate = eventModel.EndDate,
                EndTime = eventModel.EndTime.TimeOfDay,
                EventName = eventModel.EventName,
                EventTypeId = eventModel.EventTypeId,
                StartDate = eventModel.StartDate,
                StartTime = eventModel.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now
            };
            return @event;
        }
    }
}
