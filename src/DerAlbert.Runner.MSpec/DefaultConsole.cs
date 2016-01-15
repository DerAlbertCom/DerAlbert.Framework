using System;

namespace DerAlbert.Runner.MSpec
{
    public class DefaultConsole : IConsole
    {
        public DefaultConsole()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }
        public void Write(string line)
        {
            Console.Write(line);
        }

        public void Write(string line, params object[] parameters)
        {
            Console.Write(line, parameters);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void WriteLine(string line, params object[] parameters)
        {
            Console.WriteLine(line, parameters);
        }
    }
}
