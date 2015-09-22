using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.ModelExtentions
{
    public static class IdiomExtentions
    {
        public static bool Validate(this Idiom idiom)
        {
            return string.IsNullOrEmpty(idiom.Pronunciation) && string.IsNullOrEmpty(idiom.Word) && idiom.Definitions.Any();
        }
    }
}
