using System;
using System.Threading.Tasks;
using GenericDataAccess.Interfaces;
using GenericDataAccess.Repositories;

namespace GenericDataAccess.SampleApp
{
    // Sample entity implementing IEntity
    public class Person : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your actual SQL Server connection string
            var connectionString = "Server=localhost;Database=SampleDb;User Id=sa;Password=your_password;TrustServerCertificate=True;";
            var repository = new GenericRepository<Person>(connectionString);

            // Create a new person
            var newPerson = new Person
            {
                Name = "Alice",
                Age = 30,
                CreatedAt = DateTime.UtcNow
            };
            var inserted = await repository.PostAsync(newPerson);
            Console.WriteLine($"Inserted: {inserted.Id} {inserted.Name}");

            // Get all people
            var allPeople = await repository.GetAllAsync();
            foreach (var person in allPeople)
            {
                Console.WriteLine($"{person.Id}: {person.Name}, {person.Age}");
            }

            // Filter
            var filtered = await repository.FilterAsync("Age > @MinAge", new { MinAge = 25 });
            Console.WriteLine("Filtered:");
            foreach (var person in filtered)
            {
                Console.WriteLine($"{person.Name} ({person.Age})");
            }

            // Cursor pagination
            var paged = await repository.CursorPaginationAsync(2);
            Console.WriteLine("Paged:");
            foreach (var person in paged)
            {
                Console.WriteLine($"{person.Name} ({person.CreatedAt})");
            }
        }
    }
}
