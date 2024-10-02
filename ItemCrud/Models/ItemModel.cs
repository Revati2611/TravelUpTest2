using System.ComponentModel.DataAnnotations;

namespace ItemCrud.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
    }
}
