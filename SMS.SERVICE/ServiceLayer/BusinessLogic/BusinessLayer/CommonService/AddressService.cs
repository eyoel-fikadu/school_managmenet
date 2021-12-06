using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.IService.ICommonService;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.CommonService
{
    public class AddressService : IAddressService
    {
        private IAddressServiceInternal addressServiceInternal;

        public AddressService()
        {
            addressServiceInternal = Singleton.GetAddressServiceInternal();
        }

        #region Address
        public AddressModel CreateAddress(AddressModel _addressModel)
        {
            using(var context = new SCMSEntities())
            {
                Address address = addressServiceInternal.AddAddress(_addressModel);
                context.Addresses.Add(address);
                context.SaveChanges();
                _addressModel.AddressID = address.AddressId;
                return _addressModel;
            }
        }
        public AddressModel CreateAddress(AddressModel _addressModel, int referenceId)
        {
            using(var context = new SCMSEntities())
            {
                _addressModel.ReferenceID = referenceId;
                Address address = addressServiceInternal.AddAddress(_addressModel);
                context.Addresses.Add(address);
                context.SaveChanges();
                _addressModel.AddressID = address.AddressId;
                return _addressModel;
            }
        }
        public List<AddressModel> CreateAddress(List<AddressModel> _addressModels)
        {
            using(var context = new SCMSEntities())
            {
                foreach (AddressModel addressModel in _addressModels)
                {
                    Address address = addressServiceInternal.AddAddress(addressModel);
                    context.Addresses.Add(address);
                }
                context.SaveChanges();
                return _addressModels;
            }
        }
        public List<AddressModel> CreateAddress(List<AddressModel> _addressModels, int referenceId)
        {
            using (var context = new SCMSEntities())
            {
                foreach (AddressModel addr in _addressModels)
                {
                    addr.ReferenceID = referenceId;
                    Address address = addressServiceInternal.AddAddress(addr);
                    context.Addresses.Add(address);
                }
                context.SaveChanges();
                return _addressModels;
            }
        }
        public AddressModel CreateAddress(AddressModel _addressModel, int referenceId, String NameSpace)
        {
            using(var context = new SCMSEntities())
            {
                _addressModel.ReferenceID = referenceId;
                _addressModel.AddressNameSpace = NameSpace;
                Address address = addressServiceInternal.AddAddress(_addressModel);
                context.Addresses.Add(address);
                context.SaveChanges();
                return _addressModel;
            }
        }
        public List<AddressModel> CreateAddress(List<AddressModel> _addressModels, int referenceId, String NameSpace)
        {
            using (var context = new SCMSEntities())
            {
                foreach (AddressModel addr in _addressModels)
                {
                    addr.ReferenceID = referenceId;
                    addr.AddressNameSpace = NameSpace;
                    Address address = addressServiceInternal.AddAddress(addr);
                    context.Addresses.Add(address);
                }
                context.SaveChanges();
                return _addressModels;
            }
        }
        public AddressModel UpdateAddress(AddressModel _addressModel)
        {
            using(var context = new SCMSEntities())
            {
                Address address = addressServiceInternal.UpdateAddress(_addressModel);
                context.Addresses.AddOrUpdate(address);
                context.SaveChanges();
                return _addressModel;
            }
        }
        public List<AddressModel> UpdateAddress(List<AddressModel> _addressModels)
        {
            using (var context = new SCMSEntities())
            {
                foreach (AddressModel addr in _addressModels)
                {
                    Address address = addressServiceInternal.UpdateAddress(addr);
                    context.Addresses.AddOrUpdate(address);
                }
                context.SaveChanges();
                return _addressModels;
            }
        }
        public List<AddressModel> GetAddress(int referenceId, String tableNameSpace)
        {
            var addr = addressServiceInternal.GetAddressByReferenceAndTableNameSpace(referenceId, tableNameSpace);
            if (addr != null)
            {
                List<Address> addresses = addr.ToList();
                return GetDtoModels.GetAddressModel(addresses);
            }
            return null;
        }
        public List<AddressModel> GetAddress()
        {
            var addr = addressServiceInternal.GetAllAddress();
            if (addr != null)
            {
                List<Address> addresses = addr.ToList();
                return GetDtoModels.GetAddressModel(addresses);
            }
            return null;
        }

        #endregion

        #region Location

        public LocationModel CreateLocation(LocationModel _locationModel)
        {
            using (var context = new SCMSEntities())
            {
                Location location = addressServiceInternal.AddLocation(_locationModel);
                context.Locations.Add(location);
                context.SaveChanges();
                _locationModel.LocationId = location.LocationId;
                return _locationModel;
            }
        }
        public LocationModel CreateLocation(LocationModel _locationModel, int referenceId)
        {
            using (var context = new SCMSEntities())
            {
                _locationModel.RefernceId = referenceId;
                Location location = addressServiceInternal.AddLocation(_locationModel);
                context.Locations.Add(location);
                context.SaveChanges();
                _locationModel.LocationId = location.LocationId;
                return _locationModel;
            }
        }
        public LocationModel CreateLocation(LocationModel _locationModel, int referenceId, String NameSpace)
        {
            using (var context = new SCMSEntities())
            {
                _locationModel.RefernceId = referenceId;
                _locationModel.LocationNameSpace = NameSpace;
                Location location = addressServiceInternal.AddLocation(_locationModel);
                context.Locations.Add(location);
                context.SaveChanges();
                _locationModel.LocationId = location.LocationId;
                return _locationModel;
            }
        }
        public LocationModel UpdateLocation(LocationModel _locationModel)
        {
            using(var context = new SCMSEntities())
            {
                Location location = addressServiceInternal.UpdateLocation(_locationModel);
                context.Locations.AddOrUpdate(location);
                context.SaveChanges();
                return _locationModel;
            }
        }
        public List<LocationModel> GetLocation()
        {
            List<Location> locations = addressServiceInternal.GetAllLocation();
            List<LocationModel> locationModels = new List<LocationModel>();
            foreach (Location location in locations)
            {
                LocationModel locationModel = GetDtoModels.GetLocationModel(location);
                locationModels.Add(locationModel);
            }
            return locationModels;
        }
        public LocationModel GetLocation(int referenceId, String tableNameSpace)
        {
            Location location = addressServiceInternal.GetLocationByReferenceAndTableNameSpace(referenceId,tableNameSpace);
            if (location != null) return GetDtoModels.GetLocationModel(location);
            return null;
        }

        #endregion
    }
}
