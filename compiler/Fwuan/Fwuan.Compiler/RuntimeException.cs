using System;

namespace Fwuan.Compiler
{
    internal class RuntimeException : Exception
    {
        public RuntimeException(string message) : base(message)
        {
        }
    }
}