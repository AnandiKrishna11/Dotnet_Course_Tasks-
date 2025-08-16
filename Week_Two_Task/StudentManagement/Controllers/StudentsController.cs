using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Controllers
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
        // Get its public 
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                
                var students = await _context.Students
                    .FromSqlRaw("EXEC GetAllStudents")
                    .ToListAsync();

                if (students == null)
                    return NotFound(new { message = "No students found" });

                return students;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

          [AllowAnonymous]
          [HttpGet("{id}")]
          public async Task<ActionResult<Student>> GetStudent(int id)
          {
           try
            {
           
             var students = await _context.Students
                 .FromSqlRaw("EXEC GetStudentById @StudentId", new SqlParameter("StudentId", id))
                 .ToListAsync();

             
             var student = students.FirstOrDefault(s => s.StudentId == id);

             if (student == null)
               return NotFound(new { message = "Student not found" });

               return student;
             }
             catch (Exception ex)
             {
               return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
             }
         }
        //Post (Protected)
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            
            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Grade))
                return BadRequest(new { message = "Name and Grade are required" });
            
            
            if (student.Age <= 0)
                return BadRequest(new { message = "Age must be a positive number" });

            try
            {
                
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddStudent @Name, @Age, @Grade",
                    new SqlParameter("Name", student.Name),
                    new SqlParameter("Age", student.Age),
                    new SqlParameter("Grade", student.Grade)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }

            
            var newStudent = await _context.Students.OrderByDescending(s => s.StudentId).FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = newStudent?.StudentId }, newStudent);
        }

        //Post (Protected)
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student updatedStudent)
        {
            
            if (id != updatedStudent.StudentId)
                return BadRequest(new { message = "Student ID mismatch" });
            
            
            if (string.IsNullOrWhiteSpace(updatedStudent.Name) || string.IsNullOrWhiteSpace(updatedStudent.Grade))
                return BadRequest(new { message = "Name and Grade are required" });
            
            if (updatedStudent.Age <= 0)
                return BadRequest(new { message = "Age must be a positive number" });

            try
            {
                // Call the UpdateStudent stored procedure
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateStudent @StudentId, @Name, @Age, @Grade",
                    new SqlParameter("StudentId", updatedStudent.StudentId),
                    new SqlParameter("Name", updatedStudent.Name),
                    new SqlParameter("Age", updatedStudent.Age),
                    new SqlParameter("Grade", updatedStudent.Grade)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
            
            var studentExists = await _context.Students.AnyAsync(e => e.StudentId == id);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found after attempted update." });
            }

            return NoContent();
        }

        //Delete (Protected)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteStudent @StudentId",
                    new SqlParameter("StudentId", id)
                );

                if (result == 0) 
                {
                    return NotFound(new { message = "Student not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }

            return NoContent();
        }
    }
}
