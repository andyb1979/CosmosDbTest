using Microsoft.Azure.Cosmos;
using System.Net;

namespace CosmosDbDemo.Repositories
{
    public class CosmosDbRepository<T> where T : class
    {
        private readonly Container _container;

        public CosmosDbRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<T> CreateItemAsync(T item, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
                Console.WriteLine($"Created item. Cost: {response.RequestCharge} RUs");
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine($"Item already exists. Error: {ex.Message}");
                throw;
            }
        }

        public async Task<T> ReadItemAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                Console.WriteLine($"Read item. Cost: {response.RequestCharge} RUs");
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Item not found. Error: {ex.Message}");
                throw;
            }
        }

        public async Task<T> UpdateItemAsync(string id, T item, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
                Console.WriteLine($"Updated item. Cost: {response.RequestCharge} RUs");
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Item not found for update. Error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteItemAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
                Console.WriteLine($"Deleted item. Cost: {response.RequestCharge} RUs");
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Item not found for deletion. Error: {ex.Message}");
                throw;
            }
        }
    }
}
