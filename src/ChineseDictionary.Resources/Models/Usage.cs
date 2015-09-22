using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChineseDictionary.Resources.Models
{
    public class Usage
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public string Sentence { get; set; } 
    }
}