using AutoMapper;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ModulesController(LmsApiContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            uow = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
          if (uow.ModuleRepository == null)
          {
              return NotFound();
          }
            return Ok(await uow.ModuleRepository.GetAllModules());
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
          if (uow.ModuleRepository == null)
          {
              return NotFound();
          }
            var @module = await uow.ModuleRepository.GetModule(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            //_context.Entry(@module).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();

                // not sure about this
                uow.ModuleRepository.Update(module);
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
          if (uow.ModuleRepository == null)
          {
              return Problem("Entity set 'uow.ModuleRepository'  is null.");
          }
            uow.ModuleRepository.Add(@module);
            await uow.CompleteAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            if (uow.ModuleRepository == null)
            {
                return NotFound();
            }
            var @module = await uow.ModuleRepository.GetModule(id);

            if (@module == null)
            {
                return NotFound();
            }

            uow.ModuleRepository.Remove(@module);
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool ModuleExists(int id) {
            return (_context.Module?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private async Task<bool> ModuleExists2(int id) {
            return await uow.ModuleRepository.AnyAsync(id);
        }


    }
}
