using SCMS.DataAccess;
using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService
{
    public interface IAddressService
    {
        #region Address
        AddressModel CreateAddress(AddressModel addressModel, int referenceId);
        List<AddressModel> CreateAddress(List<AddressModel> addressModels, int referenceId);
        AddressModel CreateAddress(AddressModel addressModel, int referenceId, String nameSpace);
        List<AddressModel> CreateAddress(List<AddressModel> addressModels, int referenceId, String NameSpace);
        AddressModel CreateAddress(AddressModel addressModel);
        List<AddressModel> CreateAddress(List<AddressModel> addressModels);
        AddressModel UpdateAddress(AddressModel addressModel);
        List<AddressModel> UpdateAddress(List<AddressModel> addressModels);
        List<AddressModel> GetAddress(int referenceId, String tableNameSpace);
        List<AddressModel> GetAddress();
        #region using context

        #endregion

        #endregion

        #region Location
        LocationModel CreateLocation(LocationModel locationModel, int referenceId, String nameSpace);
        LocationModel CreateLocation(LocationModel locationModel, int referenceId);
        LocationModel CreateLocation(LocationModel locationModel);
        LocationModel UpdateLocation(LocationModel locationModel);
        List<LocationModel> GetLocation();
        LocationModel GetLocation(int referenceId, String tableNameSpace);
        
        #endregion
    }
}
