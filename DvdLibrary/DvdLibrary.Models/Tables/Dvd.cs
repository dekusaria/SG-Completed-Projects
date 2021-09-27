using System;
using System.Collections.Generic;
using System.Text;

namespace DvdLibrary.Models.Tables
{
    public class Dvd
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int? DirectorId { get; set; }
        public int? RatingId { get; set; }
        public string Notes { get; set; }
    }
}
