using Microsoft.EntityFrameworkCore;

namespace ItemAPI.Models
{
    public class ItemContext: DbContext
    {
        public readonly object ItemModel;

        public ItemContext(DbContextOptions options) : base(options) { }
        
        public DbSet<ItemInfoModel> Items { get; set; }

        internal void SaveChangesAsyc()
        {
            throw new NotImplementedException();
        }
    }
}
