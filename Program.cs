using CosmosDbDemo.Models;
using CosmosDbDemo.Repositories;
using Microsoft.Azure.Cosmos;

namespace CosmosDbDemo
{
    class Program
    {
        // Cosmos DB configuration
        private static readonly string EndpointUri = "https://localhost:8081";
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private static readonly string DatabaseId = "DemoDatabase";
        private static readonly string ContainerId = "People";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Cosmos DB CRUD Demo");
            Console.WriteLine("-------------------");

            try
            {
                // Initialize Cosmos client
                CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway
                });

                // Create database and container if they don't exist
                await SetupCosmosDbAsync(cosmosClient);

                // Create repository
                var repository = new CosmosDbRepository<Person>(cosmosClient, DatabaseId, ContainerId);

                // Create a new person
                Console.WriteLine("\nCreating a new person...");
                var person = new Person
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com"
                };

                // Create operation
                var createdPerson = await repository.CreateItemAsync(person, person.Id);
                Console.WriteLine($"Created: {createdPerson}");

                // Read operation
                Console.WriteLine("\nReading the person...");
                var readPerson = await repository.ReadItemAsync(createdPerson.Id, createdPerson.Id);
                Console.WriteLine($"Read: {readPerson}");

                // Update operation
                Console.WriteLine("\nUpdating the person...");
                readPerson.Email = "john.updated@example.com";
                var updatedPerson = await repository.UpdateItemAsync(readPerson.Id, readPerson, readPerson.Id);
                Console.WriteLine($"Updated: {updatedPerson}");

                // Delete operation
                Console.WriteLine("\nDeleting the person...");
                await repository.DeleteItemAsync(updatedPerson.Id, updatedPerson.Id);
                Console.WriteLine("Person deleted");

                Console.WriteLine("\nDemo completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static async Task SetupCosmosDbAsync(CosmosClient cosmosClient)
        {
            // Create database if it doesn't exist
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            Console.WriteLine($"Database {DatabaseId} created or exists");

            // Create container if it doesn't exist
            ContainerResponse container = await database.Database.CreateContainerIfNotExistsAsync(ContainerId, "/id");
            Console.WriteLine($"Container {ContainerId} created or exists");
        }
    }
}
