using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseDictionary.Resources.Models
{
    public class DefinitionEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }
    }
}
