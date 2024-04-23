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
    public class InventoryStatesController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public InventoryStatesController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/InventoryStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryState>>> GetInventoryStates()
        {
            return await _context.InventoryStates.ToListAsync();
        }

        // GET: api/InventoryStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryState>> GetInventoryState(string id)
        {
            var InventoryState = await _context.InventoryStates.FindAsync(id);

            if (InventoryState == null)
            {
                return NotFound();
            }

            return InventoryState;
        }

        // PUT: api/InventoryStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryState(string id, InventoryState InventoryState)
        {
            if (id != InventoryState.InventoryStateId)
            {
                return BadRequest();
            }

            _context.Entry(InventoryState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryStateExists(id))
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

        // POST: api/InventoryStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryState>> PostInventoryState(InventoryState InventoryState)
        {
            _context.InventoryStates.Add(InventoryState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryState", new { id = InventoryState.InventoryStateId }, InventoryState);
        }

        // DELETE: api/InventoryStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryState(string id)
        {
            var InventoryState = await _context.InventoryStates.FindAsync(id);
            if (InventoryState == null)
            {
                return NotFound();
            }

            _context.InventoryStates.Remove(InventoryState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryStateExists(string id)
        {
            return _context.InventoryStates.Any(e => e.InventoryStateId == id);
        }
    }
}
