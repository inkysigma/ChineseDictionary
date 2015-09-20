using System;
using System.Collections.Generic;
#if DNX451
using System.Diagnostics;
#endif
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
            stream.Dispose();
            var namespaceIndexLine = text.IndexOf("namespace ChineseDictionary.Resources");
            text[namespaceIndexLine] = "namespace ChineseDictionary.temp";
            text.Insert(text.Count - 3, "\tprotected override void OnConfiguring(DbContextOptionsBuilder builder)");
            text.Insert(text.Count - 3, "\t{");
            text.Insert(text.Count - 3, "\t\tbuilder.UseNpgsql(\"Server = localhost; Database = chinese; User Id = default; Password =public\");");
            text.Insert(text.Count - 3, "\t}");
            var writer = new StreamWriter("temp/DictionaryContext.cs");
            foreach (var t in text)
            {
                writer.WriteLine(t);
            }
            await writer.FlushAsync();
            writer.Dispose();

            Console.WriteLine("Enter the name of the migration:");
            var migrationName = Console.ReadLine();
            var info = new ProcessStartInfo("dnx", "ef migrations add " + migrationName);
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            var add = Process.Start(info);
            while (!add.StandardError.EndOfStream)
                Console.WriteLine(await add.StandardError.ReadLineAsync());
            add.WaitForExit();
            info = new ProcessStartInfo("dnx", "ef database update");
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            var update = Process.Start(info);
            while (!update.StandardOutput.EndOfStream || !update.StandardError.EndOfStream)
            {
                if (!update.StandardOutput.EndOfStream)
                {
                    Console.WriteLine(await update.StandardOutput.ReadLineAsync());
                }

                if (!update.StandardError.EndOfStream)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(await update.StandardError.ReadLineAsync());
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            update.WaitForExit();
        }
    }
}