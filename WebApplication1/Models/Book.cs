using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    
    [Required (ErrorMessage = "Название книги обязательно")]
    public string BookName { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Author { get; set; }
}