using System;
using System.Collections.Generic;

namespace Fwuan.Compiler.TokenReaders
{
    internal class IdentifierTokenReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (!char.IsLetter(sequence[0]) && sequence[0] != '_')
                return HandleWithSuccessor(sequence, line);

            int endOfIdentifierIndex = 1;
            while (char.IsLetterOrDigit(sequence[endOfIdentifierIndex]))
                endOfIdentifierIndex++;

            string identifier = sequence.ToString(0, endOfIdentifierIndex);
            TokenType type = Reserved.Words.GetValueOrDefault(identifier, TokenType.Identifier);

            return new TokenReadResult
                   {
                       Token = new Token(type, identifier, null, line),
                       CharactersUsed = endOfIdentifierIndex,
                       LinesTraversed = 0
                   };
        }
    }
}