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
    public class TinplatesController : ControllerBase
    {
        private readonly StockManagerContext _context;

        public TinplatesController(StockManagerContext context)
        {
            _context = context;
        }

        public IQueryable<Tinplate> TinplatesQuery(string searchString)
        {
            var tinplates = from t in _context.Tinplates
                            select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                tinplates = tinplates.Where(t => t.Order.OrderNumber.Contains(searchString)
                                            || t.Batch.Contains(searchString)
                                            || t.Brand.Contains(searchString));
            }
            return tinplates;
        }

        // GET: api/Tinplates
        [EnableCors("ProductionPolicy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinplate>>> GetTinplates(string searchString)
        {
            //todo search
            var tinplates = TinplatesQuery(searchString);

            tinplates.Include(t => t.Order)
                .ThenInclude(o => o.Customer);

            return await tinplates.ToListAsync();
        }

        // GET: api/Tinplates/5
        [EnableCors("ProductionPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinplate>> GetTinplate(long id)
        {
            var tinplate = await _context.Tinplates.FindAsync(id);

            if (tinplate == null)
            {
                return NotFound();
            }

            return tinplate;
        }

        // PUT: api/Tinplates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("ProductionPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinplate(long id, TinplateStatus newStatus)
        {
            Tinplate tinplate = _context.Tinplates.Find(id);

            if (tinplate == null)
            {
                return NotFound();
            }

            //prevent double Production/Consumed
            if (tinplate.TinplateStatus >= newStatus)
            {
                return BadRequest();
            }
            var currentUser = "Evans";
            
            tinplate.UpdateTime = DateTime.Now;

            if (newStatus == TinplateStatus.Production)
            {
                //check if there are any Tinplate in plant
                if (_context.Tinplates.Any(t => t.TinplateStatus == newStatus))
                {
                    Tinplate prevTinplate = _context.Tinplates.FirstOrDefault(t => t.TinplateStatus == newStatus);
                    prevTinplate.TinplateStatus = TinplateStatus.Consumed;

                    var preNewNote = $"On {tinplate.UpdateTime}, this pallet of tinplate has been used, operator: {currentUser}.\n";
                    prevTinplate.Note += preNewNote;

                    _context.Entry(prevTinplate).State = EntityState.Modified;
                }

                tinplate.TinplateStatus = TinplateStatus.Production;
                //log the activities history
                var newNote = $"On {tinplate.UpdateTime}, this pallet of tinplate has been using, operator: {currentUser}.\n";
                tinplate.Note += newNote;
            }
            else if (newStatus == TinplateStatus.Consumed)
            {
                tinplate.TinplateStatus = TinplateStatus.Consumed;
                //log the activities history
                var newNote = $"On {tinplate.UpdateTime}, this pallet of tinplate has been used, operator: {currentUser}.\n";
                tinplate.Note += newNote;
            }

            _context.Entry(tinplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinplateExists(id))
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

        // POST: api/Tinplates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("ProductionPolicy")]
        [HttpPost]
        public async Task<ActionResult<Tinplate>> PostTinplate(Tinplate tinplate)
        {
            //check if there is a tinplate with the same batch number already exist
            if (TinplateExists(tinplate.Id))
            {
                return BadRequest();
            }

            if (tinplate.CanType == CanType._502)
            {
                tinplate.TinplateQty = tinplate.TinplateQty * 10 + tinplate.Good;
            }
            else if (tinplate.CanType == CanType._401)
            {
                tinplate.TinplateQty = tinplate.TinplateQty * 18 + tinplate.Good;
            }

            //log the activities history
            var currentUser = "Evans";
            tinplate.UpdateTime = DateTime.Now;
            var newNote = $"On {tinplate.UpdateTime}, this pallet of tinplate was accepted by {currentUser}.\n";
            tinplate.Note += newNote;

            _context.Tinplates.Add(tinplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTinplate), new { id = tinplate.Id }, tinplate);
        }

        // DELETE: api/Tinplates/5
        [EnableCors("ProductionPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tinplate>> DeleteTinplate(long id)
        {
            var tinplate = await _context.Tinplates.FindAsync(id);
            if (tinplate == null)
            {
                return NotFound();
            }

            _context.Tinplates.Remove(tinplate);
            await _context.SaveChangesAsync();

            return tinplate;
        }

        private bool TinplateExists(long id)
        {
            return _context.Tinplates.Any(e => e.Id == id);
        }
    }
}
