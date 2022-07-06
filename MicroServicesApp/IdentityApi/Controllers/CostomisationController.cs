using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostomisationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public CostomisationController(AppDbContext context)
        {
            _context = context;
        }
        //[Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> TestF()
        {
            return Ok("or");
        }

        // GET: api/Costomisation
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Getcostomisations()
        {
            return Ok();
            //return await _context.costomisations.ToListAsync();
        }

        // GET: api/Costomisation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CostomisationModel>> GetCostomisationModel(int id)
        {
            var costomisationModel = await _context.costomisations.FindAsync(id);

            if (costomisationModel == null)
            {
                return NotFound();
            }

            return costomisationModel;
        }

        // PUT: api/Costomisation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostomisationModel(int id, CostomisationModel costomisationModel)
        {
            if (id != costomisationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(costomisationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostomisationModelExists(id))
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

        // POST: api/Costomisation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CostomisationModel>> PostCostomisationModel(CostomisationModel costomisationModel)
        {
            _context.costomisations.Add(costomisationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCostomisationModel", new { id = costomisationModel.Id }, costomisationModel);
        }

        // DELETE: api/Costomisation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostomisationModel(int id)
        {
            var costomisationModel = await _context.costomisations.FindAsync(id);
            if (costomisationModel == null)
            {
                return NotFound();
            }

            _context.costomisations.Remove(costomisationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CostomisationModelExists(int id)
        {
            return _context.costomisations.Any(e => e.Id == id);
        }
    }
}
