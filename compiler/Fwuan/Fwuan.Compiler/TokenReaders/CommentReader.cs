using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class CommentReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            if (sequence.Length < 2 || sequence[0] != '/' || sequence[1] != '/')
                return HandleWithSuccessor(sequence, line);

            int endOfLine = sequence.IndexOf('\n');
            return new TokenReadResult
                   {
                       Token = null,
                       CharactersUsed = endOfLine,
                       LinesTraversed = 0
                   };
        }
    }
}