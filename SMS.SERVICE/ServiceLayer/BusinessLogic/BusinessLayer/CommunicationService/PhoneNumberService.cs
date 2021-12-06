using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommunicationService;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.ICommunicationServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.CommunicationServiceInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.CommunicationService
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private IPhoneNumberServiceInternal phoneNumberServiceInternal;
        public PhoneNumberService()
        {
            phoneNumberServiceInternal = new EthioTelecomPhoneNumberService();
        }

        public bool CheckPhoneNumber(string phoneNumber)
        {
            return phoneNumberServiceInternal.CheckPhoneNumber(phoneNumber);
        }

        public string GetStandardizePhoneNumber(string phoneNumber)
        {
            String phone = phoneNumberServiceInternal.GetFormattedPhoneNumber(phoneNumber);

            if (String.IsNullOrEmpty(phone)) return null;
            return phone;
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            return phoneNumberServiceInternal.ValidatePhoneNumber(phoneNumber);
        }
    }
}
