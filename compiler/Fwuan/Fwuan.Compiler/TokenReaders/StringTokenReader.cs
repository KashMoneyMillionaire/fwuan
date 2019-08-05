using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class StringTokenReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (sequence[0] != '"')
                return HandleWithSuccessor(sequence, line);

            int endQuoteIndex = sequence.Slice(1).IndexOf('"') + 1;
            if (endQuoteIndex == -1)
                throw new InvalidTokenException("Unterminated string.");

            return new TokenReadResult
                   {
                       Token = new Token(TokenType.String, sequence.ToString(0, endQuoteIndex + 1), sequence.ToString(1, endQuoteIndex - 1), line),
                       CharactersUsed = endQuoteIndex + 1,
                       LinesTraversed = sequence.Slice(0, endQuoteIndex).Count('\n')
                   };
        }
    }
}