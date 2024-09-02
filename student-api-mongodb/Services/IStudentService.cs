using student_api_mongodb.models;

namespace student_api_mongodb.Services
{
    public interface IStudentService
    {
        List<Student> Get();
        Student Get(string id);
        Student Create(Student student);
        void Update(string id, Student student);
        void Remove(string id);
    }
}
