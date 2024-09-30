using CrudWebAPITU2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudWebAPITU2.Controllers
{
    public class ItemsController : ApiController
    {

        private ItemContext db;

        public ItemsController() : this(new ItemContext())
        {
        }

        public ItemsController(ItemContext context)
        {
            db = context;
        }
        public IHttpActionResult GetItems()
        {
            return Ok(db.Items.ToList());
        }

        // GET api/CrudItem/5
        public IHttpActionResult GetItem(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                return Ok("Item not found!!!");
            }
            return Ok(item);
        }

        // POST api/CrudItem
        public IHttpActionResult PostItem(ItemModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return Ok("Item Inserted successfully....");
        }

        // PUT: api/CrudItem/5
        public IHttpActionResult PutItem(int id, ItemModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return Ok("Item not found!!!");
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/CrudItem/5
        public IHttpActionResult DeleteItem(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                return Ok("Item not found!!!");
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok("Item Deleted successfully....");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}

