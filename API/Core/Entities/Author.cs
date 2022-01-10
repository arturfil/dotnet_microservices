using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Core.Entities {

  public class Author {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("lastName")]
    public string Lastname { get; set; }
    
    [BsonElement("eduction")]
    public string Eduction { get; set; }
  }

}