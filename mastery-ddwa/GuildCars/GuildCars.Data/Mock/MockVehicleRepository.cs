using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GuildCars.Data.Mock
{
    public class MockVehicleRepository : IVehicleRepository
    {
        private static List<Vehicle> _repo;

        public MockVehicleRepository()
        {
            _repo = new List<Vehicle>()
            {
                new Vehicle()
                {
                    VehicleId = 1,
                    Vin = "AAAA1111BBBB2222",
                    VehicleTypeId = 1,
                    ModelId = 1,
                    ColorId = 2,
                    InteriorId = 4,
                    BodyStyleId = 2,
                    TransmissionId = 1,
                    Year = 2021,
                    Mileage = 0,
                    Msrp = 25000M,
                    SalePrice = 21000M,
                    Description = "This is a mock description!",
                    ImageFileName = "testImage.png",
                    IsFeatured = true,
                    IsSold = false
                },

                new Vehicle()
                {
                    VehicleId = 2,
                    Vin = "CCCC3333DDDD4444",
                    VehicleTypeId = 2,
                    ModelId = 2,
                    ColorId = 5,
                    InteriorId = 3,
                    BodyStyleId = 2,
                    TransmissionId = 2,
                    Year = 2021,
                    Mileage = 2000,
                    Msrp = 32000M,
                    SalePrice = 30000M,
                    Description = "This is another mock description!",
                    ImageFileName = "testImage.png",
                    IsFeatured = false,
                    IsSold = false
                },

                new Vehicle()
                {
                    VehicleId = 3,
                    Vin = "EEEE5555FFFF6666",
                    VehicleTypeId = 1,
                    ModelId = 2,
                    ColorId = 3,
                    InteriorId = 2,
                    BodyStyleId = 2,
                    TransmissionId = 1,
                    Year = 2021,
                    Mileage = 200,
                    Msrp = 25000M,
                    SalePrice = 22000M,
                    Description = "Wow, this vehicle totally doesn't even exist! This is mock data after all...",
                    ImageFileName = "testImage.png",
                    IsFeatured = true,
                    IsSold = true
                },

                new Vehicle()
                {
                    VehicleId = 4,
                    Vin = "FFFF6666GGGG7777",
                    VehicleTypeId = 2,
                    ModelId = 2,
                    ColorId = 6,
                    InteriorId = 5,
                    BodyStyleId = 2,
                    TransmissionId = 2,
                    Year = 2015,
                    Mileage = 60000,
                    Msrp = 15000M,
                    SalePrice = 14000M,
                    Description = "This bad boy comes loaded with SOME features.",
                    ImageFileName = "testImage.png",
                    IsFeatured = false,
                    IsSold = true
                }
            };
        }
        public void Delete(int vehicleId)
        {
            _repo.RemoveAll(v => v.VehicleId == vehicleId);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _repo;
        }

        public Vehicle GetById(int vehicleId)
        {
            return _repo.FirstOrDefault(v => v.VehicleId == vehicleId);
        }

        public IEnumerable<VehicleItem> GetByVehicleType(int vehicleTypeId)
        {
            List<VehicleItem> vehicleItems = new List<VehicleItem>();

            var vehicleTypes = new MockVehicleTypeRepository().GetAll();
            var models = new MockModelRepository().GetAll();
            var makes = new MockMakeRepository().GetAll();
            var colors = new MockColorsRepository().GetAll();
            var interiors = new MockInteriorsRepository().GetAll();
            var bodystyles = new MockBodyStyleRepository().GetAll();
            var transmissions = new MockTransmissionRepository().GetAll();

            var query = from v in _repo
                        join vt in vehicleTypes on v.VehicleTypeId equals vt.VehicleTypeId
                        join m in models on v.ModelId equals m.ModelId
                        join ma in makes on m.MakeId equals ma.MakeId
                        join c in colors on v.ColorId equals c.ColorId
                        join i in interiors on v.InteriorId equals i.InteriorId
                        join b in bodystyles on v.BodyStyleId equals b.BodyStyleId
                        join t in transmissions on v.TransmissionId equals t.TransmissionId
                        where v.VehicleTypeId == vehicleTypeId
                        select new
                        {
                            VehicleId = v.VehicleId,
                            Vin = v.Vin,
                            VehicleTypeName = vt.VehicleTypeName,
                            ModelName = m.ModelName,
                            MakeName = ma.MakeName,
                            ColorName = c.ColorName,
                            InteriorType = i.InteriorType,
                            BodyStyleType = b.BodyStyleType,
                            TransmissionType = t.TransmissionType,
                            Year = v.Year,
                            Mileage = v.Mileage,
                            Msrp = v.Msrp,
                            SalePrice = v.SalePrice,
                            Description = v.Description,
                            ImageFileName = v.ImageFileName,
                            IsFeatured = v.IsFeatured,
                            IsSold = v.IsSold
                        };

            foreach (var q in query)
            {
                VehicleItem row = new VehicleItem();
                row.VehicleId = q.VehicleId;
                row.Vin = q.Vin;
                row.VehicleTypeName = q.VehicleTypeName;
                row.ModelName = q.ModelName;
                row.MakeName = q.MakeName;
                row.ColorName = q.ColorName;
                row.InteriorType = q.InteriorType;
                row.BodyStyleType = q.BodyStyleType;
                row.TransmissionType = q.TransmissionType;
                row.Year = q.Year;
                row.Mileage = q.Mileage;
                row.Msrp = q.Msrp;
                row.SalePrice = q.SalePrice;
                row.Description = q.Description;
                row.ImageFileName = q.ImageFileName;
                row.IsFeatured = q.IsFeatured;
                row.IsSold = q.IsSold;

                vehicleItems.Add(row);
            }

            return vehicleItems;
        }

        public VehicleItem GetDetails(int vehicleId)
        {
            VehicleItem details = new VehicleItem();

            var vehicleTypes = new MockVehicleTypeRepository().GetAll();
            var models = new MockModelRepository().GetAll();
            var makes = new MockMakeRepository().GetAll();
            var colors = new MockColorsRepository().GetAll();
            var interiors = new MockInteriorsRepository().GetAll();
            var bodystyles = new MockBodyStyleRepository().GetAll();
            var transmissions = new MockTransmissionRepository().GetAll();

            var query = from v in _repo
                        join vt in vehicleTypes on v.VehicleTypeId equals vt.VehicleTypeId
                        join m in models on v.ModelId equals m.ModelId
                        join ma in makes on m.MakeId equals ma.MakeId
                        join c in colors on v.ColorId equals c.ColorId
                        join i in interiors on v.InteriorId equals i.InteriorId
                        join b in bodystyles on v.BodyStyleId equals b.BodyStyleId
                        join t in transmissions on v.TransmissionId equals t.TransmissionId
                        where v.VehicleId == vehicleId
                        select new
                        {
                            VehicleId = v.VehicleId,
                            Vin = v.Vin,
                            VehicleTypeName = vt.VehicleTypeName,
                            ModelName = m.ModelName,
                            MakeName = ma.MakeName,
                            ColorName = c.ColorName,
                            InteriorType = i.InteriorType,
                            BodyStyleType = b.BodyStyleType,
                            TransmissionType = t.TransmissionType,
                            Year = v.Year,
                            Mileage = v.Mileage,
                            Msrp = v.Msrp,
                            SalePrice = v.SalePrice,
                            Description = v.Description,
                            ImageFileName = v.ImageFileName,
                            IsFeatured = v.IsFeatured,
                            IsSold = v.IsSold
                        };

            foreach (var q in query)
            {
                details.VehicleId = q.VehicleId;
                details.Vin = q.Vin;
                details.VehicleTypeName = q.VehicleTypeName;
                details.ModelName = q.ModelName;
                details.MakeName = q.MakeName;
                details.ColorName = q.ColorName;
                details.InteriorType = q.InteriorType;
                details.BodyStyleType = q.BodyStyleType;
                details.TransmissionType = q.TransmissionType;
                details.Year = q.Year;
                details.Mileage = q.Mileage;
                details.Msrp = q.Msrp;
                details.SalePrice = q.SalePrice;
                details.Description = q.Description;
                details.ImageFileName = q.ImageFileName;
                details.IsFeatured = q.IsFeatured;
                details.IsSold = q.IsSold;
            }

            return details;
        }

        public IEnumerable<VehicleShortItem> GetFeaturedVehicles()
        {
            List<VehicleShortItem> featured = new List<VehicleShortItem>();

            var models = new MockModelRepository().GetAll();
            var makes = new MockMakeRepository().GetAll();

            var query = from v in _repo
                        join m in models on v.ModelId equals m.ModelId
                        join ma in makes on m.MakeId equals ma.MakeId
                        where v.IsFeatured
                        select new
                        {
                            VehicleId = v.VehicleId,
                            Vin = v.Vin,
                            Year = v.Year,
                            MakeName = ma.MakeName,
                            ModelName = m.ModelName,
                            SalePrice = v.SalePrice,
                            ImageFileName = v.ImageFileName
                        };

            foreach (var q in query)
            {
                VehicleShortItem row = new VehicleShortItem();
                row.VehicleId = q.VehicleId;
                row.Vin = q.Vin;
                row.Year = q.Year;
                row.MakeName = q.MakeName;
                row.ModelName = q.ModelName;
                row.SalePrice = q.SalePrice;
                row.ImageFileName = q.ImageFileName;

                featured.Add(row);
            }

            return featured;
        }

        public void Insert(Vehicle vehicle)
        {
            vehicle.VehicleId = _repo.Max(v => v.VehicleId) + 1;
        }

        public IEnumerable<VehicleItem> SearchVehicles(VehicleSearchParameters param)
        {
            List<VehicleItem> vehicleItems = new List<VehicleItem>();

            var vehicleTypes = new MockVehicleTypeRepository().GetAll();
            var models = new MockModelRepository().GetAll();
            var makes = new MockMakeRepository().GetAll();
            var colors = new MockColorsRepository().GetAll();
            var interiors = new MockInteriorsRepository().GetAll();
            var bodystyles = new MockBodyStyleRepository().GetAll();
            var transmissions = new MockTransmissionRepository().GetAll();

            var query = from v in _repo
                        join vt in vehicleTypes on v.VehicleTypeId equals vt.VehicleTypeId
                        join m in models on v.ModelId equals m.ModelId
                        join ma in makes on m.MakeId equals ma.MakeId
                        join c in colors on v.ColorId equals c.ColorId
                        join i in interiors on v.InteriorId equals i.InteriorId
                        join b in bodystyles on v.BodyStyleId equals b.BodyStyleId
                        join t in transmissions on v.TransmissionId equals t.TransmissionId
                        where v.IsSold == false && (param.VehicleTypeId == v.VehicleTypeId || param.VehicleTypeId == 0)
                        select new
                        {
                            VehicleId = v.VehicleId,
                            Vin = v.Vin,
                            VehicleTypeName = vt.VehicleTypeName,
                            ModelName = m.ModelName,
                            MakeName = ma.MakeName,
                            ColorName = c.ColorName,
                            InteriorType = i.InteriorType,
                            BodyStyleType = b.BodyStyleType,
                            TransmissionType = t.TransmissionType,
                            Year = v.Year,
                            Mileage = v.Mileage,
                            Msrp = v.Msrp,
                            SalePrice = v.SalePrice,
                            Description = v.Description,
                            ImageFileName = v.ImageFileName,
                            IsFeatured = v.IsFeatured,
                            IsSold = v.IsSold
                        };

            if (param.MinPrice.HasValue)
            {
                query = query.Where(v => v.SalePrice > param.MinPrice);
            }

            if (param.MaxPrice.HasValue)
            {
                query = query.Where(v => v.SalePrice < param.MaxPrice);
            }

            if (param.MinYear.HasValue)
            {
                query = query.Where(v => v.Year > param.MinYear);
            }

            if (param.MaxYear.HasValue)
            {
                query = query.Where(v => v.Year < param.MaxYear);
            }

            if (!string.IsNullOrEmpty(param.SearchTerm))
            {
                if(int.TryParse(param.SearchTerm, out int searchYear))
                {
                    query = query.Where(v => v.Year == searchYear);
                }
                else if (makes.All(m => m.MakeName.Contains(param.SearchTerm)))
                {
                    query = query.Where(m => m.MakeName == param.SearchTerm);
                }
                else if (models.All(m => m.ModelName.Contains(param.SearchTerm)))
                {
                    query = query.Where(m => m.ModelName == param.SearchTerm);
                }
            }
            

            foreach (var q in query)
            {
                VehicleItem row = new VehicleItem();

                row.VehicleId = q.VehicleId;
                row.Vin = q.Vin;
                row.VehicleTypeName = q.VehicleTypeName;
                row.VehicleTypeName = q.VehicleTypeName;
                row.ModelName = q.ModelName;
                row.MakeName = q.MakeName;
                row.ColorName = q.ColorName;
                row.InteriorType = q.InteriorType;
                row.BodyStyleType = q.BodyStyleType;
                row.TransmissionType = q.TransmissionType;
                row.Year = q.Year;
                row.Mileage = q.Mileage;
                row.Msrp = q.Msrp;
                row.SalePrice = q.SalePrice;
                row.Description = q.Description;
                row.ImageFileName = q.ImageFileName;
                row.IsFeatured = q.IsFeatured;
                row.IsSold = q.IsSold;

                vehicleItems.Add(row);
            }

            return vehicleItems;
        }

        public void Update(Vehicle vehicle)
        {
            _repo.RemoveAll(v => v.VehicleId == vehicle.VehicleId);
            _repo.Add(vehicle); 
        }

        public void VehicleIsSold(int vehicleId)
        {
            var vehicle = _repo.FirstOrDefault(m => m.VehicleId == vehicleId);

            vehicle.IsSold = true;

            _repo.RemoveAll(m => m.VehicleId == vehicle.VehicleId);

            _repo.Add(vehicle);
        }
    }
}
