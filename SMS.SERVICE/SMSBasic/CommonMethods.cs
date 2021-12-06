using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.SMSBasic
{
    public class CommonMethods
    {
        public static int GetValue(Nullable<int> nullInt)
        {
            if (nullInt != null) return nullInt.Value;
            return 0;
        }
        public static int GetErrorCode(CustomResponse value)
        {
            return (int)value;
        }

        public static String GetErrorMessage(int val)
        {
            return Enum.GetName(typeof(CustomResponse), val);
        }

        public static void SetResponse(SCMSResponse response, CustomResponse value)
        {
            response.responseCode = GetErrorCode(value);
            response.responseMessage = GetErrorMessage(response.responseCode);
        }
        public static SCMSException GetException(CustomResponse value)
        {
            SCMSException exception = new SCMSException();
            exception.responseCode = GetErrorCode(value);
            exception.responseMessage = GetErrorMessage(exception.responseCode);
            return exception;
        }
        public static void ThrowError(List<SCMSException> errorMessages)
        {
            if(errorMessages.Count > 0)
            {
                SCSMExceptionList list = new SCSMExceptionList();
                list.errorMessages = errorMessages;
                throw list;
            }
        }

        public static List<SCMSResponse> GetErrorResponse(List<SCMSException> errorMessages)
        {
            List<SCMSResponse> responses = new List<SCMSResponse>(); 
            foreach (var errors in errorMessages)
            {
                responses.Add(new SCMSResponse()
                {
                    responseCode = errors.responseCode,
                    responseMessage = errors.responseMessage
                });
            }
            return responses;
        }
        public static bool CompareList(List<int> mainInts, List<int> ints)
        {
            foreach(int vs in ints)
            {
                if (!mainInts.Contains(vs)) return false;
            }
            return true;
        }
    }
}
