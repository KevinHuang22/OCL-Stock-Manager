using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCL_Apis.Models;

namespace OCL_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly StockManagerContext _context;

        public ResourcesController(StockManagerContext context)
        {
            _context = context;
        }

        public IQueryable<Resource> ResourcesQuery(string searchString)
        {
            var resources = from r in _context.Resources
                            select r;

            if (!string.IsNullOrEmpty(searchString))
            {
                resources = resources.Where(r => r.Order.Contains(searchString) 
                                            || r.Batch.Contains(searchString)
                                            || r.Brand.Contains(searchString));
            }
            return resources;
        }

        // GET: api/Resources
        [EnableCors("ProductionPolicy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            //todo search
            return await _context.Resources.ToListAsync();
        }

        // GET: api/Resources/5
        [EnableCors("ProductionPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(long id)
        {
            var resource = await _context.Resources.FindAsync(id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // PUT: api/Resources/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("ProductionPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(long id, ResourceStatus resourceStatus)
        {
            Resource resource = _context.Resources.Find(id);

            var currentUser = "Evans";
            var date = DateTime.Now;

            if (resourceStatus == ResourceStatus.Production)
            {
                //check if there are any resource in plant

                if (_context.Resources.Any(r => r.ResourceStatus == resourceStatus))
                {
                    Resource prevResource = _context.Resources.FirstOrDefault(r => r.ResourceStatus == resourceStatus);
                    prevResource.ResourceStatus = ResourceStatus.Consumed;

                    var preNewNote = $"On {date}, this pallet of resource has been used, operator: {currentUser}.\n";
                    prevResource.Note += preNewNote;

                    _context.Entry(prevResource).State = EntityState.Modified;
                }

                resource.ResourceStatus = ResourceStatus.Production;
                //log the activities history
                var newNote = $"On {date}, this pallet of resource has been using, operator: {currentUser}.\n";
                resource.Note += newNote;
            }
            else if (resourceStatus == ResourceStatus.Consumed)
            {
                resource.ResourceStatus = ResourceStatus.Consumed;
                //log the activities history
                var newNote = $"On {date}, this pallet of resource has been used, operator: {currentUser}.\n";
                resource.Note += newNote;
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);
        }

        // POST: api/Resources
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("ProductionPolicy")]
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {

            if (resource.CanType == CanType._502)
            {
                resource.Tinplate = resource.Tinplate * 10 + resource.Good;
            }
            else if (resource.CanType == CanType._401)
            {
                resource.Tinplate = resource.Tinplate * 18 + resource.Good;
            }

            //log the activities history
            var currentUser = "Evans";
            var date = DateTime.Now;
            var newNote = $"On {date}, this pallet of resource was accepted by {currentUser}.\n";
            resource.Note += newNote;

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);
        }

        // DELETE: api/Resources/5
        [EnableCors("ProductionPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Resource>> DeleteResource(long id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return resource;
        }

        private bool ResourceExists(long id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
