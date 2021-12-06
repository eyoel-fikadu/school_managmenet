using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.ICommunicationServiceInternal
{
    public interface IPhoneNumberServiceInternal
    {
        bool CheckPhoneNumber(String phoneNumber);
        String GetFormattedPhoneNumber(String phoneNumber);
        bool ValidatePhoneNumber(String phoneNumber);
    }
}
