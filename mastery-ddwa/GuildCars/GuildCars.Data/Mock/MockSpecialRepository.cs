using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockSpecialRepository : ISpecialRepository
    {
        private static List<Special> _repo;

        public MockSpecialRepository()
        {
            _repo = new List<Special>()
            {
                new Special
                {
                    SpecialId = 1,
                    SpecialTitle = "Summer Sales Event",
                    SpecialDescription = "Bacon ipsum dolor amet rump pariatur non hamburger tri-tip," +
                    " in biltong. Voluptate porchetta bresaola short loin veniam, shankle dolor ribeye laboris." +
                    " Esse aliquip turducken, ex pancetta culpa leberkas rumpfrankfurter venison sirloin jowl laboris."
                },

                new Special
                {
                    SpecialId = 2,
                    SpecialTitle = "Everything MUST Go!",
                    SpecialDescription = "Bacon ipsum pariatur anim, occaecat tenderloin bresaola non excepteur" +
                    " ham hock labore picanha. Pariatur pork chop jerky kielbasa. Boudin chislic mollit flank cupidatat" +
                    " porchetta, adipisicing cupim filet mignon culpa in deserunt strip steak. Qui enim andouille laboris brisket culpa."
                },

                new Special
                {
                    SpecialId = 3,
                    SpecialTitle = "Infinite Test Drive!",
                    SpecialDescription = "Bacon ipsum aliqua pork loin, dolor alcatra cupidatat prosciutto ea. Ham pork" +
                    " loin aute salami meatloaf dolor pariatur dolore minim nulla. Eiusmod biltong velit est meatball leberkas ullamco." +
                    " Aute ut brisket, dolore rump kevin ea esse jerky et burgdoggen elit cillum."
                }
            };
        }
        public void Delete(int specialId)
        {
            _repo.RemoveAll(m => m.SpecialId == specialId);
        }

        public IEnumerable<Special> GetAll()
        {
            return _repo;
        }

        public Special GetById(int specialId)
        {
            return _repo.FirstOrDefault(m => m.SpecialId == specialId);
        }

        public void Insert(Special special)
        {
            _repo.Add(special);
        }

        public void Update(Special special)
        {
            _repo.RemoveAll(m => m.SpecialId == special.SpecialId);
            _repo.Add(special);
        }
    }
}
