﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class //struct, new() - generic type constraint
    {
        protected Context Context { get; private set; }
        public BaseRepository(Context context)
        {
            Context = context;
        }

        public abstract TEntity GetById(int id);
        public abstract TEntity GetDetailById(int id);

        public abstract IList<TEntity> GetList();

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var set = Context.Set<TEntity>();
            var entity = set.Find(id);
            set.Remove(entity);
            Context.SaveChanges();
        }
    }
}
