using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBooksRepository
    {
        private Context _context = null;

        public ComicBooksRepository(Context context)
        {
            _context = context;
        }

        public IList<ComicBook> GetList()
        {
            return _context.ComicBooks
                .Include(cb => cb.Series)
                .OrderBy(cb => cb.Series.Title)
                .ThenBy(cb => cb.IssueNumber)
                .ToList();
        }

        public ComicBook GetDetailById(int? id)
        {
            return _context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .Where(cb => cb.Id == (int)id)
                    .SingleOrDefault();
        }

        public ComicBook GetById(int id)
        {
            return _context.ComicBooks
                .Where(cb => cb.Id == id)
                .SingleOrDefault();
        }

        public void Add(ComicBook comicBook)
        {
            _context.ComicBooks.Add(comicBook);
            _context.SaveChanges();
        }

        public void Update(ComicBook comicBook)
        {
            _context.Entry(comicBook).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var comicBook = new ComicBook() { Id = id };
            _context.Entry(comicBook).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public Boolean SeriesHasIssueNumber(int id, int seriesId, int issueNumber)
        {
            return _context.ComicBooks
                    .Any(cb => cb.Id != id &&
                    cb.SeriesId == seriesId &&
                    cb.IssueNumber == issueNumber);
        }

        public Boolean ArtistExists(int comicBookId, int artistId, int roleId)
        {
            return _context.ComicBookArtists
                        .Any(cba => cba.ComicBookId == comicBookId &&
                                cba.ArtistId == artistId &&
                                cba.RoleId == roleId);
        }

    }
}
