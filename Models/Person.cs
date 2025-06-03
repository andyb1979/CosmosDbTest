using Newtonsoft.Json;

namespace CosmosDbDemo.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
