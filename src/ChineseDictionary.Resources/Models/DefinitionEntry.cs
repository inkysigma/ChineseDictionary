using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
