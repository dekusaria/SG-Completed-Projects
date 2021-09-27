using DvdLibrary.Models.Tables;
using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvdLibrary.Data.Interfaces
{
    public interface IDvdRepository
    {
        DvdItem GetDvdById(int dvdId);
        IEnumerable<DvdItem> GetAll();
        void Insert(DvdItem dvd);
        void Update(DvdItem dvd);
        void Delete(int dvdId);
        IEnumerable<DvdItem> SearchByTitle(string title);
        IEnumerable<DvdItem> SearchByReleaseYear(int releaseYear);
        IEnumerable<DvdItem> SearchByDirector(string directorName);
        IEnumerable<DvdItem> SearchByRating(string ratingName);
    }
}
