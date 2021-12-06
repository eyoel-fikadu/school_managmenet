using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.EventDTO
{
    public class EventModel : AddTableModel
    {
        public int EventId { get; set; }
        [Required]
        [NotNull]
        public string EventName { get; set; }
        [Required]
        [NotNull]
        public string EventTypeId { get; set; }
        [Required]
        [NotNull]
        public DateTime StartDate { get; set; }
        [Required]
        [NotNull]
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
    }
}
