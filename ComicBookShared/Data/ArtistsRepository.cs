using ComicBookShared.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class ArtistsRepository : BaseRepository<Artist>
    {
        public ArtistsRepository(Context context)
            : base(context)
        {
        }

        public override Artist GetById(int id)
        {
            return Context.Artists
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override Artist GetDetailById(int id)
        {
            return Context.Artists
                .Include(a => a.ComicBooks.Select(s => s.ComicBook.Series))
                .Include(a => a.ComicBooks.Select(s => s.Role))
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists
                .OrderBy(a => a.Name)
                .ToList();
        }

        public bool ArtistExists(Artist artist)
        {
            return Context.Artists
                .Any(a => a.Id != artist.Id && 
                a.Name == artist.Name);
        }
    }
}
