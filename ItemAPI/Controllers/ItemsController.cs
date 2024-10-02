using ItemAPI.Models;
using ItemAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepo _itemsRepo;

        public ItemsController(IItemsRepo itemsRepo)
        {
            _itemsRepo = itemsRepo;
        }


        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<ItemInfoModel> Get()
        {
            var data =_itemsRepo.GetItems();
            return data;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ItemInfoModel data =await _itemsRepo.GetItemById(id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public async Task<IActionResult> Post(ItemInfoModel itemInfoModel)
        {
            string result = await _itemsRepo.SaveItem(itemInfoModel);
            return Ok(result);

        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ItemInfoModel itemInfoModel)
        {
            string result = await _itemsRepo.UpdateItem(itemInfoModel);
            return Ok(result);
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await _itemsRepo.DeleteItem(id);
            return Ok(result);
        }
    }
}
