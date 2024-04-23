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
    public class AddressTypeController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public AddressTypeController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/AddressType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddressType()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddressType(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);

            if (AddressType == null)
            {
                return NotFound();
            }

            return AddressType;
        }

        // PUT: api/AddressType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressType(string id, AddressType AddressType)
        {
            if (id != AddressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(AddressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddressType(AddressType AddressType)
        {
            _context.AddressTypes.Add(AddressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = AddressType.AddressTypeId }, AddressType);
        }

        // DELETE: api/AddressType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressType(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);
            if (AddressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(AddressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressTypeExists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}
