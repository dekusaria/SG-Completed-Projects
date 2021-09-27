using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DvdLibrary.Models.Queries
{
    public class DvdItem
    {
        public int DvdId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        public string DirectorName { get; set; }
        public string RatingName { get; set; }
        public string Notes { get; set; }
    }
}
