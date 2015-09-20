using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ChineseDictionary.Migrate
{
    public class Program
    {
        public async Task Main(string[] args)
        {
            Console.WriteLine("Migrating to temp/DictionaryContext.cs");
            var stream = new StreamReader("../ChineseDictionary.Resources/DictionaryContext.cs");
            var text = new List<string>();
            while (!stream.EndOfStream)
            {
                text.Add(await stream.ReadLineAsync());
            }
            text.Insert(text.Count - 3, "\t\tprotected override void OnConfiguring(DbContextOptionsBuilder builder)");
            text.Insert(text.Count - 3, "\t\t{");
            text.Insert(text.Count - 3, "\t\t\tbuilder.UseNpgsql(\"Server = localhost; Database = chinese; User Id = default; Password =public\");");
            text.Insert(text.Count - 3, "\t\t}");
            var writer = new StreamWriter("temp/DictionaryContext.cs");
            foreach (var t in text)
            {
                writer.Write(t);
            }
            await writer.FlushAsync();
        }
    }
}