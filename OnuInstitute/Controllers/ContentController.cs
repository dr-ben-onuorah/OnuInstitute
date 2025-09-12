using OnuInstitute.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnuInstitute.Models;
using System;

namespace OnuInstitute.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ContentController : ControllerBase
        {
            private readonly AppDbContext DBContext;

            public ContentController(AppDbContext dbContext)
            {
                this.DBContext = dbContext;
            }

            // GET: api/Content
            // Retrieves a list of all content items.
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Content>>> GetContents()
            {
                return await DBContext.Contents.ToListAsync();
            }

            // GET: api/Content/5
            // Retrieves a single content item by ID.
            [HttpGet("{id}")]
            public async Task<ActionResult<Content>> GetContent(int id)
            {
                var content = await DBContext.Contents.FindAsync(id);

                if (content == null)
                {
                    return NotFound();
                }

                return content;
            }

            // POST: api/Content
            // Creates a new content item.
            [HttpPost]
            public async Task<ActionResult<Content>> PostContent(Content content)
            {
                DBContext.Contents.Add(content);
                await DBContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContent), new { id = content.Id }, content);
            }

            // PUT: api/Content/5
            // Updates an existing content item.
            [HttpPut("{id}")]
            public async Task<IActionResult> PutContent(int id, Content content)
            {
                if (id != content.Id)
                {
                    return BadRequest();
                }

                DBContext.Entry(content).State = EntityState.Modified;

                try
                {
                    await DBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(id))
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

            // DELETE: api/Content/5
            // Deletes a content item.
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteContent(int id)
            {
                var content = await DBContext.Contents.FindAsync(id);
                if (content == null)
                {
                    return NotFound();
                }

                DBContext.Contents.Remove(content);
                await DBContext.SaveChangesAsync();

                return NoContent();
            }

            private bool ContentExists(int id)
            {
                return DBContext.Contents.Any(e => e.Id == id);
            }
        }
}
