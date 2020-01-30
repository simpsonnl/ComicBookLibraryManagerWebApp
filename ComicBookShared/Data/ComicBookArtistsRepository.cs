using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBookArtistsRepository : BaseRepository<ComicBookArtist>
    {
       

        public ComicBookArtistsRepository(Context context)
            : base(context)
        {
        }

        public override ComicBookArtist GetById(int id)
        {

            return Context.ComicBookArtists
                .Include(a => a.ComicBook.Series)
                .Include(a => a.Artist)
                .Include(a => a.Role)
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override ComicBookArtist GetDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<ComicBookArtist> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
