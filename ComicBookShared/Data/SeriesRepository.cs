using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class SeriesRepository : BaseRepository<Series>
    {
        public SeriesRepository(Context context)
            : base(context)
        {
        }

        public override Series GetById(int id)
        {
            return Context.Series
                .Where(s => s.Id == id)
                .SingleOrDefault();
        }

        public override Series GetDetailById(int id)
        {
            return Context.Series
                .Include(s => s.ComicBooks)
                .Where(s => s.Id == id)
                .SingleOrDefault();
        }

        public override IList<Series> GetList()
        {
            return Context.Series
                .OrderBy(s => s.Title)
                .ToList();
        }

        public bool SeriesExists(Series series)
        {
            return Context.Series
                .Any(s => s.Title == series.Title && s.Id != series.Id);
        }
    }
}
