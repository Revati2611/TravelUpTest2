using CrudWebAPITU2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public interface IItemContext
    {
        DbSet<ItemModel> Item { get; set; }
        int SaveChanges();
        DbEntityEntry<ItemModel> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
    }
}
