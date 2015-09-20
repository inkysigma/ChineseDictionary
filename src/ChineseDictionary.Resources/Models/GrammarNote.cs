using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ChineseDictionary.Resources.Models
{
    public class GrammarNote
    {
        [JsonIgnore]
        [Key]
        public int Number { get; set; }

        public string Note { get; set; }
    }
}
