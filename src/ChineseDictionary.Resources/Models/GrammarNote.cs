using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChineseDictionary.Resources.Models
{
    public class GrammarNote
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string Note { get; set; }
    }
}
