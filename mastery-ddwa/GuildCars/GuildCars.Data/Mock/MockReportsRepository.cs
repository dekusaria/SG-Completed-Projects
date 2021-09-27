using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockReportsRepository : IReportsRepository
    {
        public IEnumerable<InventoryReport> GetInventoryByVehicleType(int vehicleTypeId)
        {
            List<InventoryReport> reports = new List<InventoryReport>();

            var vehicleRepo = new MockVehicleRepository().GetAll();
            var modelRepo = new MockModelRepository().GetAll();
            var makeRepo = new MockMakeRepository().GetAll();

            var inventory = from vehicle in vehicleRepo
                            join model in modelRepo on vehicle.ModelId equals model.ModelId
                            join make in makeRepo on model.MakeId equals make.MakeId
                            where vehicle.VehicleTypeId == vehicleTypeId
                            group vehicle by new { vehicle.Year, make.MakeName, model.ModelName } into g
                            select new
                            {
                                Year = g.Key.Year,
                                MakeName = g.Key.MakeName,
                                ModelName = g.Key.ModelName,
                                Count = g.Count(),
                                StockValue = g.Sum(m => m.Msrp)
                            };

            foreach (var item in inventory)
            {
                InventoryReport report = new InventoryReport();

                report.Year = item.Year;
                report.Make = item.MakeName;
                report.Model = item.ModelName;
                report.Count = item.Count;
                report.StockValue = item.StockValue;

                reports.Add(report);
            }

            return reports;
        }

        public IEnumerable<SalesReport> GetSalesReports()
        {
            List<SalesReport> reports = new List<SalesReport>();

            var purchaseRepo = new MockPurchaseRepository().GetAll();

            var results = from purchase in purchaseRepo
                          group purchase by purchase.SoldByEmail into g
                          select new
                          {
                              UserName = g.Key,
                              TotalSales = g.Sum(m => m.PurchasePrice),
                              TotalVehicles = g.Count()
                          };

            foreach (var report in results)
            {
                SalesReport row = new SalesReport();
                row.UserName = report.UserName;
                row.TotalSales = report.TotalSales;
                row.TotalVehicles = report.TotalVehicles;

                reports.Add(row);
            }

            return reports;
        }

        public IEnumerable<SalesReport> SearchSalesReports(SalesSearchParameters param)
        {
            List<SalesReport> reports = new List<SalesReport>();

            var purchaseRepo = new MockPurchaseRepository().GetAll();

            var results = from p in purchaseRepo
                          where (string.IsNullOrEmpty(param.UserEmail) || p.SoldByEmail.Contains(param.UserEmail) &&
                                  (param.MinDate == null || p.PurchaseDate > param.MinDate) &&
                                  (param.MaxDate == null || p.PurchaseDate < param.MaxDate))
                          group p by p.SoldByEmail into g
                          select new
                          {
                              UserName = g.Key,
                              TotalSales = g.Sum(m => m.PurchasePrice),
                              TotalVehicles = g.Count()
                          };

            foreach (var report in results)
            {
                SalesReport row = new SalesReport();
                row.UserName = report.UserName;
                row.TotalSales = report.TotalSales;
                row.TotalVehicles = report.TotalVehicles;

                reports.Add(row);
            }

            return reports;
        }
    }
}
