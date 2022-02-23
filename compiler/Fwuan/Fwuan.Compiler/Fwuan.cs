using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fwuan.Compiler.Syntax;

namespace Fwuan.Compiler
{
    public class Fwuan
    {
        private static readonly AtsPrinter _printer = new AtsPrinter();
        private static readonly Interpreter _interpreter;

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
                Console.Write("> ");
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
            (List<Token> tokens, List<Error> errors) = t.GetTokens();

            if (errors.Any())
            {
                foreach (Error error in errors)
                    Console.WriteLine(error.Report());

                return true;
            }

            tokens.ForEach(Console.WriteLine);

            Parser parser = new Parser(tokens);
            Expr expr = parser.Parse();

            _printer.Print(expr);
            _interpreter.Interpret(expr);

            return false;
        }
    }

    internal class Error
    {
        private readonly int    _line;
        private readonly string _message;

        public Error(int line, string message)
        {
            _line = line;
            _message = message;
        }

        public string Report() => $"[line ${_line}] Error: {_message}";
    }
}