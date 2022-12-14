using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.AspNetCore.JsonPatch;
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
            if (uow.ModuleRepository == null) {
                return NotFound();
            }
            var modules = await uow.ModuleRepository.GetAllModules();
            var result = mapper.Map<IEnumerable<ModuleDto>>(modules);

            return Ok(result);
        }

        // GET: api/Modules/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Module>> GetModule(int id) {
        //    if (uow.ModuleRepository == null) {
        //        return NotFound();
        //    }

        //    var @module = await uow.ModuleRepository.GetModule(id);
        //    if (@module == null) {
        //        return NotFound();
        //    }

        //    var @result = mapper.Map<ModuleDto>(@module);

        //    return Ok(@result);
        //}

        [HttpGet("{moduleName}")]
        public async Task<ActionResult<Module>> GetModule(string moduleName) {
            if (uow.ModuleRepository == null) {
                return NotFound();
            }

            var @module = await uow.ModuleRepository.GetModuleByName(moduleName);
            if (@module == null) {
                return NotFound();
            }

            var @result = mapper.Map<ModuleDto>(@module);

            return Ok(@result);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id) {
                return BadRequest();
            }

            //_context.Entry(@module).State = EntityState.Modified;

            try {
                //await _context.SaveChangesAsync();

                // not sure about this
                uow.ModuleRepository.Update(module);
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ModuleExists(id)) {
                    return NotFound();
                }
                else {
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
            if (uow.ModuleRepository == null) {
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
            if (uow.ModuleRepository == null) {
                return NotFound();
            }
            var @module = await uow.ModuleRepository.GetModule(id);

            if (@module == null) {
                return NotFound();
            }

            uow.ModuleRepository.Remove(@module);
            await uow.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int moduleId, JsonPatchDocument<CourseDto> patchDocument) {
            if (uow.CourseRepository == null) {
                return NotFound();
            }

            var module = await uow.CourseRepository.GetCourse(moduleId);
            if (module == null) {
                return NotFound();
            }

            var moduleDto = mapper.Map<CourseDto>(module);
            patchDocument.ApplyTo(moduleDto, ModelState);

            if (ModelState.IsValid == false) {
                return BadRequest(ModelState);
            }

            module = mapper.Map(moduleDto, module);

            await uow.CompleteAsync();

            return Ok(moduleDto);
        }

        private bool ModuleExists(int id) {
            return (_context.Module?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private async Task<bool> ModuleExists2(int id) {
            return await uow.ModuleRepository.AnyAsync(id);
        }


    }
}
