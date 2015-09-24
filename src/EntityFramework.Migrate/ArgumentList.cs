using System.Collections.Generic;

namespace EntityFramework.Migrate
{
    public class ArgumentList
    {
        public string Main { get; set; }
        public List<string> Modifiers { get; set; } = new List<string>();
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        public void AddParameter(string name, string parameter)
        {
            Parameters.Add(name, parameter);
        }

        public void AddModifier(string modifier)
        {
            Modifiers.Add(modifier);
        }

        public string this[string key]
        {
            get { return Parameters[key]; }
            set { Parameters[key] = value; }
        }

        public bool Exists(string modifier)
        {
            return Modifiers.Contains(modifier);
        }
    }
}
