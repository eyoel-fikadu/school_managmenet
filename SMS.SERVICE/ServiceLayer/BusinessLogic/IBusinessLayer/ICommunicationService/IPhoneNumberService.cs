using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommunicationService
{
    public interface IPhoneNumberService
    {
        bool ValidatePhoneNumber(String phoneNumber);
        bool CheckPhoneNumber(String phoneNumber);
        String GetStandardizePhoneNumber(String phoneNumber);
    }
}
