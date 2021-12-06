using SCMS.DataAccess;
using SMS.SERVICE.DTO.EventDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IEventManagement;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IEventManagmentInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.EventManagement
{
    public class CalendarService : ICalendarService
    {
        private ICalendarServiceInternal calendarServiceInternal;

        public CalendarService()
        {
            calendarServiceInternal = Singleton.GetCalanderServiceInternal();
        }
        public EventModel CreateEvent(EventModel eventModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Event @event = calendarServiceInternal.AddEvent(eventModel);
                context.Events.Add(@event);
                context.SaveChanges();
                eventModel.EventId = @event.EventId;
                return eventModel;
            }
        }

        public EventModel UpdateEvent(EventModel eventModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Event @event = calendarServiceInternal.AddEvent(eventModel);
                context.Events.AddOrUpdate(@event);
                context.SaveChanges();
                return eventModel;
            }
        }
    }
}
