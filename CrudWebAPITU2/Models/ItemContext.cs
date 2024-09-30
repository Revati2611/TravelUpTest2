using System.Collections.Generic;
using System.Data.Entity;


namespace CrudWebAPITU2.Models
{
   
    public class ItemContext: DbContext
    {
        public readonly object ItemModel;

        public ItemContext() : base("name=ItemDatabase")
        {
        }
        public DbSet<ItemModel> Items { get; set; }

       
    }
}