using System;
using System.IO;
using Granola.Lex;

namespace Granola.Commandline
{
    public class Commandline
    {
        private Lexar _lexar;

        public Commandline()
        {
        }

        public void Invoke(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    RunPrompt();
                    break;
                
                case 1:
                    RunFile(args[0]);
                    break;

                default:
                    Console.WriteLine("Usage: cslox [script]");
                    break;
            }
        }

        public void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Run(bytes.ToString());
        }

        public void RunPrompt()
        {
            while(true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                Run(input);
            }
        }

        public void Run(string source)
        {
            _lexar = new Lexar(source);

            foreach(Token token in _lexar.ScanTokens())
            {
                Console.WriteLine(token);
            }
        }

        public void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private void Report(int line, string where, string message)
        {
            // should really actually print an error...
            Console.WriteLine($"[line {line} ] Error : {where} : {message}");
        }
    }
}