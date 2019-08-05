using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class WhiteSpaceReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (!char.IsWhiteSpace(sequence[0])) 
                return HandleWithSuccessor(sequence, line);
            
            return new TokenReadResult
                   {
                       Token = null,
                       CharactersUsed = 1,
                       LinesTraversed = sequence[0] == '\n' ? 1 : 0
                   };
        }
    }
}