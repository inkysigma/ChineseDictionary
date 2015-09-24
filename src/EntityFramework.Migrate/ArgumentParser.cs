namespace EntityFramework.Migrate
{
    public static class ArgumentParser
    {
        public static ArgumentList Parse(string[] args)
        {
            var list = new ArgumentList {Main = args[0]};
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-") && !args[i].StartsWith("--"))
                    list.AddParameter(args[i], args[i + 1]);
                if (args[i].StartsWith("--"))
                    list.AddModifier(args[i]);
            }
            return list;
        }
    }
}
