using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExpressionEngine
{
    // Exception for syntax errors
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) 
            : base(message)
        {
        }
    }
}
