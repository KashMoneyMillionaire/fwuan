using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fwuan.Compiler
{
    public class Fwuan
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: fwuan [script]");
                Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                if (RunFile(args[0]))
                    Environment.Exit(65);
            }
            else
            {
                RunPrompt();
            }
        }

        private static void RunPrompt()
        {
            while (true)
            {
                Console.WriteLine("> ");
                Run(Console.ReadLine());
            }
        }

        private static bool RunFile(string path)
        {
            string text = File.ReadAllText(path);

            return Run(text);
        }

        private static bool Run(string text)
        {
            Tokenizer t = new Tokenizer(text);
            (List<Token> tokens, List<Error> errors, List<Warning> warnings) = t.GetTokens();

            if (errors.Any())
            {
                foreach (Error error in errors)
                {
                    Console.WriteLine(error.Report());
                }

                return true;
            }
            
            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }

            return false;
        }
    }

    internal class Warning
    {
    }

    internal class Error
    {
        private readonly int _line;
        private readonly string _message;

        public Error(int line, string message)
        {
            _line = line;
            _message = message;
        }

        public string Report() => $"[line ${_line}] Error: {_message}";
    }

    internal class Tokenizer
    {
        private readonly string _sourceText;
        private readonly List<Error> _errors = new List<Error>();
        private readonly List<Warning> _warnings = new List<Warning>();
        private readonly List<Token> _tokens = new List<Token>();

        public Tokenizer(string sourceText)
        {
            _sourceText = sourceText.AsSpan();
        }

        public (List<Token> tokens, List<Error> errors, List<Warning> warnings) GetTokens()
        {
            
                

            return (_tokens, _errors, _warnings);
        }

        private void AddError(int line, string message)
        {
            _errors.Add(new Error(line, message));
        }
    }
}