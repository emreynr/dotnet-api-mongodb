using Microsoft.AspNetCore.Mvc;
using student_api_mongodb.models;
using student_api_mongodb.Services;

// mongodb db password : dnCPGFkVAKuQsN41

namespace student_api_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        // Constructor
        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            return studentService.Get();
        }

     
        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            var student = studentService.Get(id);

            if (student == null) 
            {
                return NotFound($"Student with ID = {id} not found..");
            }
            return student;
        }

        
        [HttpPost]
        public ActionResult Post([FromBody] Student student)
        {
            studentService.Create(student);

            return CreatedAtAction(nameof(Get), new { id =  student.Id }, student);
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Student student)
        {
            var existingStudent = studentService.Get(id);

            if (existingStudent == null) 
            {
                return NotFound($"Student with id = {id} is not found");
            }

            studentService.Update(id, student);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = studentService.Get(id);

            if(student == null)
            {
                return NotFound($"student with id = {id} not found..");
            }

            studentService.Remove(student.Id);

            return Ok($"student with id = {id} has been removed");
        }
    }
}
