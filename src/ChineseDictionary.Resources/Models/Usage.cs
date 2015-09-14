using System.ComponentModel.DataAnnotations;

namespace ChineseDictionary.Resources.Models
{
    public class Usage
    {
        [Key]
        public int Number { get; set; }
        public string Sentence { get; set; } 
    }
}