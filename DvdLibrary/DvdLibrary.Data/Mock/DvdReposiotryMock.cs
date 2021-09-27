using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DvdLibrary.Data.Mock
{
    public class DvdReposiotryMock : IDvdRepository
    {
        private static List<DvdItem> _dvds;

        public DvdReposiotryMock()
        {
            _dvds = new List<DvdItem>()
            {
                new DvdItem
                {
                    DvdId = 1,
                    Title = "Edward Scissorhands",
                    ReleaseYear = 1990,
                    DirectorName = "Tim Burton",
                    RatingName = "PG-13",
                    Notes = "A touching movie"
                },
                new DvdItem
                {
                    DvdId = 2,
                    Title = "The Lord of the Rings: The Two Towers",
                    ReleaseYear = 2002,
                    DirectorName = "Peter Jackson",
                    RatingName = "PG-13",
                    Notes = "Now is the hour when we draw swords!"
                },
                new DvdItem
                {
                    DvdId = 3,
                    Title = "Beetlejuice",
                    ReleaseYear = 1988,
                    DirectorName = "Tim Burton",
                    RatingName = "PG",
                    Notes = "How is this rated PG? I was terrified of this movie as a child."
                },
                new DvdItem
                {
                    DvdId = 4,
                    Title = "Black Widow",
                    ReleaseYear = 2021,
                    DirectorName = "Cate Shortland",
                    RatingName = "PG-13",
                    Notes = "A decent MCU entry that really should have been made years ago."
                },
                new DvdItem
                {
                    DvdId = 5,
                    Title = "The Neverending Story",
                    ReleaseYear = 1984,
                }
            };
        }
        public void Delete(int dvdId)
        {
            if (_dvds.Any(d=> d.DvdId == dvdId))
            {
                _dvds.Remove(_dvds.FirstOrDefault(d=>d.DvdId == dvdId));
            }
        }

        public IEnumerable<DvdItem> GetAll()
        {
            return _dvds;
        }

        public DvdItem GetDvdById(int dvdId)
        {
            return _dvds.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Insert(DvdItem dvd)
        {
            dvd.DvdId = _dvds.Count() + 1;
            _dvds.Add(dvd);
        }

        public IEnumerable<DvdItem> SearchByDirector(string directorName)
        {
            return _dvds.Where(d => d.DirectorName != null && (d.DirectorName.StartsWith(directorName, StringComparison.OrdinalIgnoreCase) || d.DirectorName.EndsWith(directorName, StringComparison.OrdinalIgnoreCase)));
        }

        public IEnumerable<DvdItem> SearchByRating(string ratingName)
        {
            return _dvds.Where(d => d.RatingName != null && d.RatingName == ratingName);
        }

        public IEnumerable<DvdItem> SearchByReleaseYear(int releaseYear)
        {
            return _dvds.Where(d => d.ReleaseYear == releaseYear);
        }

        public IEnumerable<DvdItem> SearchByTitle(string title)
        {
            return _dvds.Where(d => d.Title != null && (d.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase) || d.Title.EndsWith(title, StringComparison.OrdinalIgnoreCase)));
        }

        public void Update(DvdItem dvd)
        {
            _dvds.Remove(_dvds.FirstOrDefault(d => d.DvdId == dvd.DvdId));

            _dvds.Add(dvd);
        }
    }
}
