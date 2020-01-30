using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBooksRepository : BaseRepository<ComicBook>
    {
        

        public ComicBooksRepository(Context context)
            : base(context)
        {
        }

        public override IList<ComicBook> GetList()
        {
            return Context.ComicBooks
                .Include(cb => cb.Series)
                .OrderBy(cb => cb.Series.Title)
                .ThenBy(cb => cb.IssueNumber)
                .ToList();
        }

        public override ComicBook GetDetailById(int id)
        {
            return Context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .Where(cb => cb.Id == id)
                    .SingleOrDefault();
        }

        public override ComicBook GetById(int id)
        {
            return Context.ComicBooks
                .Where(cb => cb.Id == id)
                .SingleOrDefault();
        }

        public Boolean SeriesHasIssueNumber(int id, int seriesId, int issueNumber)
        {
            return Context.ComicBooks
                    .Any(cb => cb.Id != id &&
                    cb.SeriesId == seriesId &&
                    cb.IssueNumber == issueNumber);
        }

        public Boolean ArtistExists(int comicBookId, int artistId, int roleId)
        {
            return Context.ComicBookArtists
                        .Any(cba => cba.ComicBookId == comicBookId &&
                                cba.ArtistId == artistId &&
                                cba.RoleId == roleId);
        }

    }
}
