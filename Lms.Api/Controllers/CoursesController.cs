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
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CoursesController(LmsApiContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            uow = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse(bool includeModules)
        {
            if (uow.CourseRepository == null) {
                return NotFound();
            }
            var courses = await uow.CourseRepository.GetAllCourses(includeModules);
            var result = mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(result);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            // maybe safe to remove, check is made in repository method
            if (uow.CourseRepository == null) {
                return NotFound();
            }

            var course = await uow.CourseRepository.GetCourse(id);
            if (course == null) {
                return NotFound();
            }

            var result = mapper.Map<CourseDto>(course);

            return Ok(result);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id) {
                return BadRequest();
            }

            //_context.Entry(course).State = EntityState.Modified;

            try {
                //await _context.SaveChangesAsync();

                // not sure about this
                uow.CourseRepository.Update(course);
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CourseExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (uow.CourseRepository == null) {
                return Problem("Entity set 'uow.CourseRepository'  is null.");
            }
            uow.CourseRepository.Add(course);
            await uow.CompleteAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (uow.CourseRepository == null) {
                return NotFound();
            }
            var course = await uow.CourseRepository.GetCourse(id);

            if (course == null) {
                return NotFound();
            }

            uow.CourseRepository.Remove(course);
            await uow.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {
            if(uow.CourseRepository == null) {
                return NotFound();
            }

            var course = await uow.CourseRepository.GetCourse(courseId);
            if (course == null) {
                return NotFound();
            }

            var courseDto = mapper.Map<CourseDto>(course);
            patchDocument.ApplyTo(courseDto, ModelState);

            if(ModelState.IsValid == false) {
                return BadRequest(ModelState);
            }

            course = mapper.Map(courseDto, course);

            await uow.CompleteAsync();

            return Ok(courseDto);
        }

        private bool CourseExists(int id) {
            return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // making this async
        private async Task<bool> CourseExists2(int id) {
            return await uow.CourseRepository.AnyAsync(id);
        }
    }
}
