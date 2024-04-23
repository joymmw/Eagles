using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eagles.EF.Data;
using Eagles.EF.Models;

namespace EaglesAPI.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventorysController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public InventorysController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/Inventorys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventorys()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventorys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(string id)
        {
            var Inventory = await _context.Inventories.FindAsync(id);

            if (Inventory == null)
            {
                return NotFound();
            }

            return Inventory;
        }

        // PUT: api/Inventorys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(string id, Inventory Inventory)
        {
            if (id != Inventory.InventoryId)
            {
                return BadRequest();
            }

            _context.Entry(Inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inventorys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory Inventory)
        {
            _context.Inventories.Add(Inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = Inventory.InventoryId }, Inventory);
        }

        // DELETE: api/Inventorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(string id)
        {
            var Inventory = await _context.Inventories.FindAsync(id);
            if (Inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(Inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(string id)
        {
            return _context.Inventories.Any(e => e.InventoryId == id);
        }
    }
}
