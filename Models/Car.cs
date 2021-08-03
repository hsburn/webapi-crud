using telstra.demo.Attributes;

namespace telstra.demo.Models
{
    [BsonCollection("cars")]
    public class Car : Document
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string BodyType { get; set; }
        public int EnginePower { get; set; }
    }
}