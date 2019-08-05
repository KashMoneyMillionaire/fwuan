namespace Fwuan.Compiler.TokenReaders
{
    internal static class TokenReaderBuilder
    {
        public static TokenReader BuildChainOfResponsibility()
        {
            CommentReader commentReader = new CommentReader();
            WhiteSpaceReader whiteSpaceReader = new WhiteSpaceReader();
            EqualityTokenReader equalityTokenReader = new EqualityTokenReader();
            SingleCharacterTokenReader singleCharacterTokenReader = new SingleCharacterTokenReader(Reserved.Characters);
            StringTokenReader stringTokenReader = new StringTokenReader();
            IdentifierTokenReader identifierTokenReader = new IdentifierTokenReader();
            NumberTokenReader numberTokenReader = new NumberTokenReader();

            commentReader.SetSuccessor(whiteSpaceReader);
            whiteSpaceReader.SetSuccessor(equalityTokenReader);
            equalityTokenReader.SetSuccessor(singleCharacterTokenReader);
            singleCharacterTokenReader.SetSuccessor(stringTokenReader);
            stringTokenReader.SetSuccessor(identifierTokenReader);
            identifierTokenReader.SetSuccessor(numberTokenReader);

            return commentReader;
        }
    }
}