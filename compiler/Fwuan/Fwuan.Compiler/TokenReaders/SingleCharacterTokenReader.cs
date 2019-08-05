using System;
using System.Collections.Generic;

namespace Fwuan.Compiler.TokenReaders
{
    internal class SingleCharacterTokenReader : TokenReader
    {
        private readonly Dictionary<char, TokenType> _tokens;

        public SingleCharacterTokenReader(Dictionary<char, TokenType> tokens)
        {
            _tokens = tokens;
        }

        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            char firstChar = sequence[0];
            if (_tokens.ContainsKey(firstChar))
                return new TokenReadResult
                       {
                           Token = BuildToken(_tokens[firstChar], firstChar.ToString(), line),
                           CharactersUsed = 1,
                           LinesTraversed = 0
                       };

            return HandleWithSuccessor(sequence, line);
        }
    }
}