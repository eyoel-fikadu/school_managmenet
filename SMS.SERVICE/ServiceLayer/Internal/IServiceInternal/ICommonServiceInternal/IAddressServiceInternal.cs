using SCMS.DataAccess;
using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.IService.ICommonService
{
    public interface IAddressServiceInternal
    {
        #region Address
        Address AddAddress(AddressModel addressModel);
        Address UpdateAddress(AddressModel addressModel);
        List<Address> GetAddressByReferenceAndTableNameSpace(int referenceId, String tableNameSpace);
        List<Address> GetAllAddress();

        #endregion

        #region Location
        Location AddLocation(LocationModel locationModel);
        Location UpdateLocation(LocationModel locationModel);
        List<Location> GetAllLocation();
        Location GetLocationByReferenceAndTableNameSpace(int referenceId, String tableNameSpace);
        
        #endregion
    }
}
