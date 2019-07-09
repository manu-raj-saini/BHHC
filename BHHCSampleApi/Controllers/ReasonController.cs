using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHHCSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonController : ControllerBase
    {
        private readonly Models.ReasonContext _context;
        private readonly Reason _reason;

        public ReasonController(Models.ReasonContext context)
        {
            _context = context;
            _reason = new Reason(context);
            _reason.CreateReasons();

        }
        // GET: api/ReasonItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ReasonItem>>> GetReasonItems()
        {
            return await _reason.GetReasons().ToListAsync();
        }

        // GET: api/ReasonItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ReasonItem>> GetReasonItem(long id)
        {
            var reasonItem = await _reason.GetReasons().FindAsync(id);
            //var reasonItem = _reason.GetReason(13);

            if (reasonItem == null)
            {
                return NotFound();
            }

            return reasonItem;
        }
        // POST: api/ReasonItem
        [HttpPost]
        public async Task<ActionResult<Models.ReasonItem>> PostReasonItem(Models.ReasonItem item)
        {
            _reason.CreateReason(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReasonItem), new { id = item.Id }, item);
        }
        // PUT: api/ReasonItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReasonItem(long id, Models.ReasonItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/ReasonItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReasonItem(long id)
        {
            var reasonItem = await _reason.GetReasons().FindAsync(id);

            if (reasonItem == null)
            {
                return NotFound();
            }

            _reason.GetReasons().Remove(reasonItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
