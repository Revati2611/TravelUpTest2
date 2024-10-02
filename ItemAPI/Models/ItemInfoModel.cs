using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class ItemInfoModel
    {
        [Key]
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; } 
    }
}
