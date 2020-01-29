using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBookArtistsRepository
    {
        private Context _context = null;

        public ComicBookArtistsRepository(Context context)
        {
            _context = context;
        }

        public ComicBookArtist GetById(int id)
        {
            return _context.ComicBookArtists
                .Include(a => a.ComicBook.Series)
                .Include(a => a.Artist)
                .Include(a => a.Role)
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public void Add(ComicBookArtist comicBookArtist)
        {
            _context.ComicBookArtists.Add(comicBookArtist);
            _context.SaveChanges();
        }

        public void Delete(ComicBookArtist comicBookArtist)
        {
            _context.Entry(comicBookArtist).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
