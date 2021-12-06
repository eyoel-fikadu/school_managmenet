using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.ICommunicationServiceInternal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.CommunicationServiceInternal
{
    public class EthioTelecomPhoneNumberService : IPhoneNumberServiceInternal
    {
        private const String ALL_INTERNATIONAL_PHONE_PREFIX = "+";
        private const String ALL_LOCAL_PHONE_PREFIX_ETH = "09";
        private const String INTERNATION_CALL_ETH = "251";
        private const String LOCAL_CALL_ETH = "9";
        public string GetFormattedPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 9 && phoneNumber.StartsWith(LOCAL_CALL_ETH))
            {
                return ALL_INTERNATIONAL_PHONE_PREFIX + INTERNATION_CALL_ETH + phoneNumber;
            }
            else if (phoneNumber.Length == 10 && phoneNumber.StartsWith(ALL_LOCAL_PHONE_PREFIX_ETH))
            {
                phoneNumber = phoneNumber.Remove(0);
                return ALL_INTERNATIONAL_PHONE_PREFIX + INTERNATION_CALL_ETH + phoneNumber;
            }
            else if (phoneNumber.Length == 12 && phoneNumber.StartsWith(INTERNATION_CALL_ETH))
            {
                return ALL_INTERNATIONAL_PHONE_PREFIX + phoneNumber;
            }
            else if (phoneNumber.Length == 13 && phoneNumber.StartsWith(ALL_INTERNATIONAL_PHONE_PREFIX + INTERNATION_CALL_ETH))
            {
                return phoneNumber;
            }
            return "";
        }

        public bool CheckPhoneNumber(String phoneNumber)
        {
            if (phoneNumber.StartsWith("+")) phoneNumber = phoneNumber.Remove(0);

            if (!long.TryParse(phoneNumber, out long val))
            {
                return false;
            }
            return true;
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            String phone = GetFormattedPhoneNumber(phoneNumber);

            if (String.IsNullOrEmpty(phone)) return false;
            return true;
        }
    }
}
