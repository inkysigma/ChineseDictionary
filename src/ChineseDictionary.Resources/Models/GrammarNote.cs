using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
