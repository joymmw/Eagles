﻿using System;
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
    public class GendersController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public GendersController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/Genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            return await _context.Genders.ToListAsync();
        }

        // GET: api/Genders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(string id)
        {
            var Gender = await _context.Genders.FindAsync(id);

            if (Gender == null)
            {
                return NotFound();
            }

            return Gender;
        }

        // PUT: api/Genders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender(string id, Gender Gender)
        {
            if (id != Gender.GenderId)
            {
                return BadRequest();
            }

            _context.Entry(Gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Genders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender Gender)
        {
            _context.Genders.Add(Gender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGender", new { id = Gender.GenderId }, Gender);
        }

        // DELETE: api/Genders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(string id)
        {
            var Gender = await _context.Genders.FindAsync(id);
            if (Gender == null)
            {
                return NotFound();
            }

            _context.Genders.Remove(Gender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenderExists(string id)
        {
            return _context.Genders.Any(e => e.GenderId == id);
        }
    }
}
