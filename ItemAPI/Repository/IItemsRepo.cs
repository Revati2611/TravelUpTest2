using ItemAPI.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ItemAPI.Repository
{
    public interface IItemsRepo
    {
        List<ItemInfoModel> GetItems();
        Task<ItemInfoModel> GetItemById(int id);
        Task<string> SaveItem(ItemInfoModel itemInfoModel);
        Task<string> UpdateItem(ItemInfoModel itemInfoModel);
        Task<string> DeleteItem(int id);

    }
}
