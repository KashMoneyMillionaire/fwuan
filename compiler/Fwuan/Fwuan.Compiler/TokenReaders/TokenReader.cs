using System;

namespace Fwuan.Compiler.TokenReaders
{
    internal abstract class TokenReader
    {
        private TokenReader _successor;
 
        public void SetSuccessor(TokenReader successor)
        {
            _successor = successor;
        }
 
        public abstract TokenReadResult HandleRequest(ReadOnlySpan<char> sequence, int line);

        protected TokenReadResult HandleWithSuccessor(ReadOnlySpan<char> sequence, int line)
        {
            if (_successor != null)
                return _successor.HandleRequest(sequence, line);
            
            throw new InvalidTokenException($"No handler for character: {sequence[0]}");
        }
        
        protected static Token BuildToken(TokenType type, string source, int line, object literal = null) => new Token(type, source, literal, line);
    }
}