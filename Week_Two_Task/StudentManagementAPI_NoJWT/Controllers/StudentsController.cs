using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI_NoJWT.Models;

namespace StudentManagementAPI_NoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDataBaseContext _context;

        public StudentController(StudentDataBaseContext context)
        {
            _context = context;
        }

        // =======================
        // GET: api/Student
        // =======================
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // =======================
        // GET: api/Student/{id}
        // =======================
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound(new { message = "Student not found" });

            return student;
        }

        // =======================
        // POST: api/Student
        // =======================
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Grade))
                return BadRequest(new { message = "Name and Grade are required" });

            if (student.Age <= 0)
                return BadRequest(new { message = "Age must be a positive number" });

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // =======================
        // PUT: api/Student/{id}
        // =======================
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest(new { message = "Student ID mismatch" });

            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Grade))
                return BadRequest(new { message = "Name and Grade are required" });

            if (student.Age <= 0)
                return BadRequest(new { message = "Age must be a positive number" });

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id))
                    return NotFound(new { message = "Student not found" });
                else
                    throw;
            }

            return NoContent();
        }

        // =======================
        // DELETE: api/Student/{id}
        // =======================
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound(new { message = "Student not found" });

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
