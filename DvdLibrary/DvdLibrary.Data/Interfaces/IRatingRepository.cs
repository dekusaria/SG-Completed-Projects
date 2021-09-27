using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvdLibrary.Data.Interfaces
{
    public interface IRatingRepository
    {
        Rating GetRatingById(int ratingId);
        IEnumerable<Rating> GetAll();
    }
}
