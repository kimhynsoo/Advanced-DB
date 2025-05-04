using Microsoft.AspNetCore.Mvc;

using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsService studentsService;

        public StudentController(StudentsService StudentsService)
        {
            studentsService = StudentsService;
        }
        [HttpGet]

        public async Task<List<Student>> Get() =>
            await studentsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await studentsService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student newStudent)
        {
            await studentsService.CreateAsync(newStudent);

            return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Student updatedStudent)
        {
            var Student = await studentsService.GetAsync(id);

            if (Student is null)
            {
                return NotFound();
            }

            updatedStudent.Id= Student.Id;

            await studentsService.UpdateAsync(id, updatedStudent);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Student = await studentsService.GetAsync(id);

            if (Student is null)
            {
                return NotFound();
            }

            await studentsService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPost("many")]
        public async Task<ActionResult> AddMany(List<Student> students)
        {
            await studentsService.AddManyAsync(students);

            return Ok();
        }

        [HttpGet("lastname/{lastName}")]
        public async Task<List<Student>> GetByLastName(string lastName) =>
            await studentsService.GetByLastNameAsync(lastName);

        [HttpGet("youngerthan/{age}")]
        public async Task<List<Student>> GetYoungerThan(int age) =>
            await studentsService.GetYoungerThanAsync(age);

        

    }
}