using ItemCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemCrud.Controllers
{
    public class ItemsController : Controller
    {
        private static List<ItemModel> items = new List<ItemModel>();

        private readonly HttpClient _httpClient;

        public ItemsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _httpClient.GetFromJsonAsync<List<ItemModel>>("https://localhost:7196/api/items");
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateItems([FromBody] ItemModel item)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7196/api/items", item);
                if (response.IsSuccessStatusCode)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = true });
                    }
                    return RedirectToAction("Index");
                }
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            return View(item);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<ItemModel>($"https://localhost:7196/api/items/{id}");
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(int id,[FromBody]ItemModel item)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7196/api/items/{item.Id}", item);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<ItemModel>($"https://localhost:7196/api/items/{id}");
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7196/api/items/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}