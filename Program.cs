using System;
using System.IO;
using Granola.Lexar;

namespace crafting_interpreters_followalong
{
    public class Program
    {
        private static Boolean _hadError = false;

        public static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Run(bytes.ToString());
        }

        public static void RunPrompt()
        {
            // do something
        }

        public static void Run(string source)
        {
            // do stuff

            Console.WriteLine(source);
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            // should really actually print an error...
            Console.WriteLine($"[line {line} ] Error : {where} : {message}");
            _hadError = true;
        }

        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: cslox [script]");
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }

            // string filepath = "Examples/ScannerExamples.granola";
            // Lexar lexar = new Lexar(filepath);
            
            // Token token;
            // while(lexar.TryGetNextToken(out token))
            // {
            //     token.DisplaySelf();
            // }
        }
    }
}
