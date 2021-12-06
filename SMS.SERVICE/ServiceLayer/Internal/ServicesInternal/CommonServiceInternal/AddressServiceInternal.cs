using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.ServiceLayer.IService.ICommonService;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Services.CommonService
{
    public class AddressServiceInternal : IAddressServiceInternal
    {
        #region Address
        public Address AddAddress(AddressModel addressModel)
        {
            Address address = new Address()
            {
                AddressNameSpace = addressModel.AddressNameSpace,
                AddressType = addressModel.AddressType,
                AddressValue = addressModel.AddressValue,
                IsActive = true,
                IsDefault = addressModel.AddressIsDefault,
                ReferenceId = addressModel.ReferenceID
            };
            return address;
        }
        public Address UpdateAddress(AddressModel addressModel)
        {
            Address address = new Address()
            {
                AddressId = addressModel.AddressID,
                AddressNameSpace = addressModel.AddressNameSpace,
                AddressType = addressModel.AddressType,
                AddressValue = addressModel.AddressValue,
                IsActive = addressModel.AddressIsActive,
                IsDefault = addressModel.AddressIsDefault,
                ReferenceId = addressModel.ReferenceID
            };
            return address;
        }
        public List<Address> GetAddressByReferenceAndTableNameSpace(int referenceId, string tableNameSpace)
        {
            using(var context = new SCMSEntities())
            {
                return context.Addresses.Where(x => x.ReferenceId == referenceId && x.AddressNameSpace == tableNameSpace).ToList();
            }
        }
        public List<Address> GetAllAddress()
        {
            using(var context = new SCMSEntities())
            {
                return context.Addresses.ToList();
            }
        }
      
        #endregion

        #region Location
        public Location AddLocation(LocationModel locationModel)
        {
            Location location = new Location
            {
                IsActive = true,
                ReferenceId = locationModel.RefernceId,
                LocationNameSpace = locationModel.LocationNameSpace,
                Country = locationModel.Country,
                City = locationModel.City,
                Area = locationModel.AddressLocation,
                Region = locationModel.Region,
                Latitude = locationModel.Lattitude,
                Longtitude = locationModel.Longtiude
            };
            return location;
        }
        public Location UpdateLocation(LocationModel locationModel)
        {
            Location location = new Location
            {
                LocationId = locationModel.LocationId,
                IsActive = locationModel.IsActive,
                ReferenceId = locationModel.RefernceId,
                LocationNameSpace = locationModel.LocationNameSpace,
                Country = locationModel.Country,
                City = locationModel.City,
                Area = locationModel.AddressLocation,
                Region = locationModel.Region,
                Latitude = locationModel.Lattitude,
                Longtitude = locationModel.Longtiude
            };
            return location;
        }
        public List<Location> GetAllLocation()
        {
            using(var context = new SCMSEntities())
                return context.Locations.ToList();
        }
        public Location GetLocationByReferenceAndTableNameSpace(int referenceId, string tableNameSpace)
        {
            using (var context = new SCMSEntities())
                return context.Locations.Where(x => x.ReferenceId == referenceId && x.LocationNameSpace == tableNameSpace).FirstOrDefault();
        }
       
        #endregion
       
    }
}
