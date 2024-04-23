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
    public class InventoryStatusesController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public InventoryStatusesController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/InventoryStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryStatus>>> GetInventoryStatuses()
        {
            return await _context.InventoryStatuses.ToListAsync();
        }

        // GET: api/InventoryStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryStatus>> GetInventoryStatus(string id)
        {
            var InventoryStatus = await _context.InventoryStatuses.FindAsync(id);

            if (InventoryStatus == null)
            {
                return NotFound();
            }

            return InventoryStatus;
        }

        // PUT: api/InventoryStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryStatus(string id, InventoryStatus InventoryStatus)
        {
            if (id != InventoryStatus.InventoryStatusId)
            {
                return BadRequest();
            }

            _context.Entry(InventoryStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryStatusExists(id))
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

        // POST: api/InventoryStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryStatus>> PostInventoryStatus(InventoryStatus InventoryStatus)
        {
            _context.InventoryStatuses.Add(InventoryStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryStatus", new { id = InventoryStatus.InventoryStatusId }, InventoryStatus);
        }

        // DELETE: api/InventoryStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryStatus(string id)
        {
            var InventoryStatus = await _context.InventoryStatuses.FindAsync(id);
            if (InventoryStatus == null)
            {
                return NotFound();
            }

            _context.InventoryStatuses.Remove(InventoryStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryStatusExists(string id)
        {
            return _context.InventoryStatuses.Any(e => e.InventoryStatusId == id);
        }
    }
}
