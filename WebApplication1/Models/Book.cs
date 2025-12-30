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
    
    [Required(ErrorMessage = "Цена обязательна")]
    [Range(0.01, 10000, ErrorMessage = "Цена должна быть от 0.01 до 10000")]
    public decimal? Price { get; set; }
    
    [Required (ErrorMessage = "Название категории обязательно")]
    public string Category { get; set; }
    
    [Required (ErrorMessage = "Автора обязателен")]
    public string Author { get; set; }
}