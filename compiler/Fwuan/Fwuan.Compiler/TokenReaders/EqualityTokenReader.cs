using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal class EqualityTokenReader : TokenReader
    {
        public override TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line)
        {
            char firstChar = sequence[0];

            switch (firstChar)
            {
                case '!':
                    if (NextIsMatch(sequence, '='))
                        return new TokenReadResult
                               {
                                   Token = BuildToken(TokenType.BangEqual, sequence.Slice(0, 2).ToString(), line),
                                   CharactersUsed = 2,
                                   LinesTraversed = 0
                               };
                    return new TokenReadResult
                           {
                               Token = BuildToken(TokenType.Bang, sequence[0].ToString(), line),
                               CharactersUsed = 1,
                               LinesTraversed = 0
                           };
                case '=': 
                    if (NextIsMatch(sequence, '='))
                        return new TokenReadResult
                               {
                                   Token = BuildToken(TokenType.EqualEqual, sequence.Slice(0, 2).ToString(), line),
                                   CharactersUsed = 2,
                                   LinesTraversed = 0
                               };
                    return new TokenReadResult
                           {
                               Token = BuildToken(TokenType.Equal, sequence[0].ToString(), line),
                               CharactersUsed = 1,
                               LinesTraversed = 0
                           };
                case '>': 
                    if (NextIsMatch(sequence, '='))
                        return new TokenReadResult
                               {
                                   Token = BuildToken(TokenType.GreaterThanEqual, sequence.Slice(0, 2).ToString(), line),
                                   CharactersUsed = 2,
                                   LinesTraversed = 0
                               };
                    return new TokenReadResult
                           {
                               Token = BuildToken(TokenType.GreaterThan, sequence[0].ToString(), line),
                               CharactersUsed = 1,
                               LinesTraversed = 0
                           };
                case '<': 
                    if (NextIsMatch(sequence, '='))
                        return new TokenReadResult
                               {
                                   Token = BuildToken(TokenType.LessThanEqual, sequence.Slice(0, 2).ToString(), line),
                                   CharactersUsed = 2,
                                   LinesTraversed = 0
                               };
                    return new TokenReadResult
                           {
                               Token = BuildToken(TokenType.LessThan, sequence[0].ToString(), line),
                               CharactersUsed = 1,
                               LinesTraversed = 0
                           };

                default: return HandleWithSuccessor(sequence, line);
            }

            bool NextIsMatch(ReadOnlySpan<char> chars, char matchesThis)
            {
                if (chars.Length > 1)
                    return chars[1] == matchesThis;

                return false;
            }
        }
    }
}