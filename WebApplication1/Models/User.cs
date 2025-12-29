using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    //TODO: заменить null! на required.
    
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}