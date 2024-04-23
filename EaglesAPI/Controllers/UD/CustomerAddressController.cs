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
    public class CustomerAddressesController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public CustomerAddressesController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> GetCustomerAddresses()
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomerAddress(string id)
        {
            var CustomerAddress = await _context.CustomerAddresses.FindAsync(id);

            if (CustomerAddress == null)
            {
                return NotFound();
            }

            return CustomerAddress;
        }

        // PUT: api/CustomerAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAddress(string id, CustomerAddress CustomerAddress)
        {
            if (id != CustomerAddress.CustomerAddressId)
            {
                return BadRequest();
            }

            _context.Entry(CustomerAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
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

        // POST: api/CustomerAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> PostCustomerAddress(CustomerAddress CustomerAddress)
        {
            _context.CustomerAddresses.Add(CustomerAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerAddress", new { id = CustomerAddress.CustomerAddressId }, CustomerAddress);
        }

        // DELETE: api/CustomerAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAddress(string id)
        {
            var CustomerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (CustomerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(CustomerAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAddressExists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}
