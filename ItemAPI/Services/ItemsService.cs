using ItemAPI.Models;
using ItemAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace ItemAPI.Services
{
    public class ItemsService : IItemsRepo

    {
        private readonly ItemContext _itemContext;
        public ItemsService(ItemContext itemContext)
        {
            _itemContext = itemContext;
        }
        public async Task<string> DeleteItem(int id)
        {
            var item = new ItemInfoModel { Id = id };
            _itemContext.Entry(item).State = EntityState.Deleted;
            await _itemContext.SaveChangesAsync();
            return "Item Deleted ...";
        }

        public async Task<ItemInfoModel> GetItemById(int id)
        {
            var data = await _itemContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public List<ItemInfoModel> GetItems()
        {
            List<ItemInfoModel> data = _itemContext.Items.ToList();
            return data;
        }

        public async Task<string> SaveItem(ItemInfoModel itemInfoModel)
        {
            try
            {
                _itemContext.Items.Add(itemInfoModel);
                await _itemContext.SaveChangesAsync();
                return "Save successfully...";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> UpdateItem(ItemInfoModel itemInfoModel)
        {
            try
            {
                var entity = _itemContext.Items.FirstOrDefault(item => item.Id == itemInfoModel.Id);
                // Validate entity is not null
                if (entity != null)
                {
                    entity.ItemName = itemInfoModel.ItemName;
                    entity.ItemDescription = itemInfoModel.ItemDescription;
                }
                _itemContext.Items.Update(entity);
                await _itemContext.SaveChangesAsync();
                return "Item updated .....";

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
