using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChineseDictionary.Resources.Models
{
    public class ReviewDateTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        
        public DateTime ReviewTime { get; set; }

        public static implicit operator ReviewDateTime(DateTime dateTime)
        {
            return new ReviewDateTime
            {
                ReviewTime = dateTime
            };
        }
    }
}
