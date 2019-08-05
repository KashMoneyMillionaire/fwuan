using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class NumberTokenReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (!char.IsDigit(sequence[0]))
                return HandleWithSuccessor(sequence, line);

            int endOfNumberIndex = 1;
            while (char.IsDigit(sequence[endOfNumberIndex])) 
                endOfNumberIndex++;

            if (sequence[endOfNumberIndex] == '.')
            {
                if (sequence.Length > endOfNumberIndex && char.IsDigit(sequence[endOfNumberIndex]))
                    endOfNumberIndex++;

                while (char.IsDigit(sequence[endOfNumberIndex])) 
                    endOfNumberIndex++;
            }

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