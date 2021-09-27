using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvdLibrary.Data.Interfaces
{
    public interface IDirectorRepository
    {
        Director GetDirectorById(int directorId);
        Director GetDirectorByName(string directorName);
        IEnumerable<Director> GetAll();
    }
}
