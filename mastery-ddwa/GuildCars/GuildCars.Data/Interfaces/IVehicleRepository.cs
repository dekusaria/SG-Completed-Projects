using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle GetById(int vehicleId);
        VehicleItem GetDetails(int vehicleId);
        IEnumerable<Vehicle> GetAll();
        IEnumerable<VehicleItem> GetByVehicleType(int vehicleTypeId);
        IEnumerable<VehicleShortItem> GetFeaturedVehicles();
        IEnumerable<VehicleItem> SearchVehicles(VehicleSearchParameters param);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(int vehicleId);
        void VehicleIsSold(int vehicleId);
    }
}
