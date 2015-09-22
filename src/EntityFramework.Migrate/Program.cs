using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#if DNX451
using System.Diagnostics;
#endif

namespace EntityFramework.Migrate
{
    public class Program
    {
        internal ArgumentList List { get; set; }


        public async Task Main(string[] args)
        {
            List = ArgumentParser.Parse(args);

            if (string.IsNullOrEmpty(List.Main))
            {
                Console.WriteLine("No file specified");
            }

            Console.WriteLine("Migrating to temp/" + List.Main);


            StreamReader stream;
            try
            {
                stream = new StreamReader("../ChineseDictionary.Resources/" + List.Main);
            }
            catch (IOException)
            {
                Console.WriteLine("No such file exists");
                return;
            }

            var text = new List<string>();
            while (!stream.EndOfStream)
            {
                text.Add(await stream.ReadLineAsync());
            }
            stream.Dispose();
            
            var namespaceIndexLine = text.IndexOf("namespace ChineseDictionary.Resources");
            text[namespaceIndexLine] = "namespace ChineseDictionary.temp";

            text.Insert(text.Count - 2, "\t\tprotected override void OnConfiguring(DbContextOptionsBuilder builder)");
            text.Insert(text.Count - 2, "\t\t{");
            text.Insert(text.Count - 2, "\t\t\tbuilder.UseNpgsql(\"Server=localhost;Database=chinese;User Id=default;Password=public\");");
            text.Insert(text.Count - 2, "\t\t}");
            var writer = new StreamWriter("temp/DictionaryContext.cs");
            foreach (var t in text)
                writer.WriteLine(t);
            await writer.FlushAsync();
            writer.Dispose();

            if (!List.Exists("--u"))
                return;
        }
    }
}