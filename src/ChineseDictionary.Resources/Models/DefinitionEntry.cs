using System.ComponentModel.DataAnnotations;

namespace ChineseDictionary.Resources.Models
{
    public class DefinitionEntry
    {
        [Key]
        public int Number { get; set; }
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }
    }
}
