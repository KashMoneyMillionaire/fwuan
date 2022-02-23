using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class NumberTokenReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (!char.IsDigit(sequence[0]))
                return HandleWithSuccessor(sequence, line);

            int endOfNumberIndex = sequence.Contains(" ", StringComparison.OrdinalIgnoreCase)
                                       ? sequence.IndexOf(" ")
                                       : sequence.Length;

            string lexeme = sequence.ToString(0, endOfNumberIndex);

            return new TokenReadResult
                   {
                       Token = new Token(TokenType.Number, lexeme, double.Parse(lexeme), line),
                       CharactersUsed = endOfNumberIndex,
                       LinesTraversed = 0
                   };
        }
    }
}