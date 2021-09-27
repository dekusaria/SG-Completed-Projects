using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class InventorySearchVM
    {
        public IEnumerable<SelectListItem> MinPrice { get; set; }
        public IEnumerable<SelectListItem> MaxPrice { get; set; }
        public IEnumerable<SelectListItem> MinYear { get; set; }
        public IEnumerable<SelectListItem> MaxYear { get; set; }

        public InventorySearchVM()
        {
            MinPrice = new SelectList(
                new List<decimal>()
                {
                    1000M,
                    5000M,
                    10000M,
                    15000M,
                    20000M,
                    25000M,
                    30000M
                });

            MaxPrice = new SelectList(
                new List<decimal>()
                {
                    1000M,
                    5000M,
                    10000M,
                    15000M,
                    20000M,
                    25000M,
                    30000M,
                    35000M,
                    40000M,
                    45000M,
                    50000M
                });

            MinYear = new SelectList(
                new List<int>()
                {
                    1990,
                    1991,
                    1992,
                    1993,
                    1994,
                    1995,
                    1996,
                    1997,
                    1998,
                    1999,
                    2000,
                    2001,
                    2002,
                    2003,
                    2004,
                    2005,
                    2006,
                    2007,
                    2008,
                    2009,
                    2010,
                    2011,
                    2012,
                    2013,
                    2014,
                    2015,
                    2016,
                    2017,
                    2018,
                    2019,
                    2020,
                    2021
                });

            MaxYear = new SelectList(
                new List<int>() 
                {
                    2000,
                    2001,
                    2002,
                    2003,
                    2004,
                    2005,
                    2006,
                    2007,
                    2008,
                    2009,
                    2010,
                    2011,
                    2012,
                    2013,
                    2014,
                    2015,
                    2016,
                    2017,
                    2018,
                    2019,
                    2020,
                    2021
                });
        }
    }
}