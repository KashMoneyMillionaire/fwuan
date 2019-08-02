using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Fwuan.Compiler.TokenType;

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
            (List<Token> tokens, List<Error> errors, List<Warning> warnings) = t.GetTokens();

            if (errors.Any())
            {
                foreach (Error error in errors) 
                    Console.WriteLine(error.Report());

                return true;
            }

            foreach (Warning warning in warnings) 
                Console.WriteLine(warning);
            
            foreach (Token token in tokens) 
                Console.WriteLine(token);

            return false;
        }
    }

    internal class Warning
    {
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

    internal class Tokenizer
    {
        private readonly string        _sourceText;
        private readonly List<Error>   _errors   = new List<Error>();
        private readonly List<Warning> _warnings = new List<Warning>();
        private readonly List<Token>   _tokens   = new List<Token>();

        private int _start;
        private int _current;
        private int _line    = 1;

        private bool IsNotAtEnd => !IsAtEnd;
        private bool IsAtEnd    => _current >= _sourceText.Length;

        public Tokenizer(string sourceText)
        {
            _sourceText = sourceText;
        }

        public (List<Token> tokens, List<Error> errors, List<Warning> warnings) GetTokens()
        {
            while (IsNotAtEnd)
            {
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.EndOfFile, "", null, _line));

            return (_tokens, _errors, _warnings);
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(LeftParentheses);
                    break;
                case ')':
                    AddToken(RightParentheses);
                    break;
                case '{':
                    AddToken(LeftBrace);
                    break;
                case '}':
                    AddToken(RightBrace);
                    break;
                case ',':
                    AddToken(Comma);
                    break;
                case '.':
                    AddToken(Dot);
                    break;
                case '-':
                    AddToken(Minus);
                    break;
                case '+':
                    AddToken(Plus);
                    break;
                case ';':
                    AddToken(Semicolon);
                    break;
                case '*':
                    AddToken(Asterisk);
                    break;
                
                case '!': AddToken( Match('=') ? BangEqual : Bang); break;
                case '=': AddToken( Match('=') ? EqualEqual : Equal); break;
                case '>': AddToken( Match('=') ? GreaterThanEqual : GreaterThan); break;
                case '<': AddToken( Match('=') ? LessThanEqual : LessThan); break;
                
                case '/':
                    if (Match('/'))
                        while (Peek() != '\n' && IsNotAtEnd)
                            Advance();
                    else
                        AddToken(Slash);
                    break;
                
                case ' ':                                    
                case '\r':                                   
                case '\t':                                   
                    // Ignore whitespace.                      
                    break;
                
                case '\n':                                   
                    _line++;                                    
                    break; 
                    
                case '"':
                    ReadString();
                    break;
                
                case 'o':
                    if (Peek() == 'r') AddToken(Or);
                    break;
                
                case '_':
                    ReadIdentifier();
                    break;
                
                default:

                    if (char.IsDigit(c))
                        ReadNumber();
                    else if (char.IsLetter(c))
                        ReadIdentifier();
                    else
                        AddError(_line, "Unexpected character.");
                    break;
            }
        }

        private void ReadIdentifier()
        {
            while (char.IsLetterOrDigit(Peek()))
                Advance();

            TokenType type = ReservedWords.Reserved.GetValueOrDefault(WorkingString, Identifier);
            AddToken(type);
        }

        private void ReadNumber()
        {
            while (char.IsDigit(Peek())) 
                Advance();

            if (Peek() == '.' && char.IsDigit(PeekNext()))
            {
                Advance();

                while (char.IsDigit(Peek())) 
                    Advance();
            }

            AddToken(Number, double.Parse(WorkingString));
        }

        private string WorkingString => _sourceText.Substring(_start, _current - _start);

        private void ReadString()
        {
            while (Peek() != '"' && IsNotAtEnd)
            {
                if (Peek() == '\n')
                    _line++;

                Advance();
            }

            if (IsAtEnd)
            {
                _errors.Add(new Error(_line, "Unterminated string."));
                return;
            }

            Advance();

            string value = _sourceText.Substring(_start + 1, _current - _start - 2);
            AddToken(TokenType.String, value);
        }

        private char Peek() => IsAtEnd ? '\0' : _sourceText[_current];

        private char PeekNext() => _current + 1 >= _sourceText.Length ? '\0' : _sourceText[_current + 1];

        private bool Match(char expected)
        {
            if (IsAtEnd) 
                return false;

            if (_sourceText[_current] != expected)
                return false;

            _current++;
            return true;
        }

        private void AddToken(TokenType type, object literal = null)
        {
            _tokens.Add(new Token(type, WorkingString, literal, _line));
        }

        private char Advance() => _sourceText[_current++];

        private void AddError(int line, string message)
        {
            _errors.Add(new Error(line, message));
        }
    }
}