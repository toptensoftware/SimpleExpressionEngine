using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExpressionEngine
{
    // NodeNumber represents a literal number in the expression
    class NodeNumber : Node
    {
        public NodeNumber(double number)
        {
            _number = number;
        }

        double _number;             // The number

        public override double Eval()
        {
            // Just return it.  Too easy.
            return _number;
        }
    }
}
