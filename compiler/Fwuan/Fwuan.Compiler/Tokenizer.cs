using System;
using System.Collections.Generic;
using Fwuan.Compiler.TokenReaders;

namespace Fwuan.Compiler
{
    internal class TokenReadResult
    {
        public Token Token          { get; set; }
        public int   CharactersUsed { get; set; }
        public int   LinesTraversed { get; set; }
        public bool  IgnoreToken    => Token == null;
    }

    internal class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }

    internal class Tokenizer
    {
        private readonly string _sourceText;

        public Tokenizer(string sourceText)
        {
            _sourceText = sourceText;
        }

        public (List<Token> tokens, List<Error> errors) GetTokens()
        {
            TokenReader tokenReaderChain = TokenReaderBuilder.BuildChainOfResponsibility();
            int line = 0;
            int currentCharIndex = 0;
            ReadOnlySpan<char> source = _sourceText.AsSpan();
            int sourceLength = source.Length;
            List<Token> tokens = new List<Token>();
            List<Error> errors = new List<Error>();

            while (currentCharIndex < sourceLength)
            {
                try
                {
                    TokenReadResult result = tokenReaderChain.HandleRequest(source.Slice(currentCharIndex), line);
                    line += result.LinesTraversed;
                    currentCharIndex += result.CharactersUsed;

                    if (!result.IgnoreToken)
                        tokens.Add(result.Token);
                }
                catch (Exception e)
                {
                    errors.Add(new Error(line, e.Message));
                    currentCharIndex++;
                }
            }

            return (tokens, errors);
        }
    }
}