using MongoDB.Driver;
using Microsoft.Extensions.Options;

using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> studentCollection;

        public StudentsService(IOptions<StudentDatabaseSettings> studentDatabaseSettings)
        {
            var mongoClient = new MongoClient(studentDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(studentDatabaseSettings.Value.DatabaseName);

            studentCollection = mongoDatabase.GetCollection<Student>(studentDatabaseSettings.Value.StudentsCollectionName);
        }

        public async Task<List<Student>> GetAsync()
        {
            // 학생 목록이 존재하지 않더라도 항상 반환
            return await studentCollection.Find(student => true).ToListAsync() 
                ?? new List<Student>();
        }
        public async Task<Student?> GetAsync(string id) =>
            await studentCollection.Find(x=> x.Id == id).FirstOrDefaultAsync();
        
        public async Task CreateAsync(Student newStudent) =>
            await studentCollection.InsertOneAsync(newStudent);

        public async Task UpdateAsync(string id, Student updatedStudent) =>
            await studentCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);
        
        public async Task RemoveAsync(string id) =>
            await studentCollection.DeleteOneAsync(x => x.Id == id);


        //Add multiple elements at once
        public async Task AddManyAsync(List<Student> students) =>
            await studentCollection.InsertManyAsync(students);
        
        //Filter students by last name (pass a last name parameter by url) 
        public async Task<List<Student>> GetByLastNameAsync(string lastName) =>
            await studentCollection.Find(x => x.LastName == lastName).ToListAsync();

        // Return all students younger than a certain age
        public async Task<List<Student>> GetYoungerThanAsync(int age) =>
            await studentCollection.Find(x => x.Age < age).ToListAsync();
    }
}