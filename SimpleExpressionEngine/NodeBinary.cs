using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleExpressionEngine
{    
    /// <summary>
    /// Class NodeBinary for binary operations such as Add, Subtract
    /// </summary>
    public class NodeBinary : Node
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="NodeBinary" /> class.
        /// </summary>
        /// <param name="lhs">Left node to be operated on</param>
        /// <param name="rhs">Right node to be operated on</param>
        /// <param name="op">Actual operation</param>
        public NodeBinary(Node lhs, Node rhs, Func<double, double, double> op)
        {
            _lhs = lhs;
            _rhs = rhs;
            _op = op;
        }

        /// <summary>
        /// Left hand side of the operation
        /// </summary>
        Node _lhs;
        /// <summary>
        /// Right hand side of the operation
        /// </summary>
        Node _rhs;
        /// <summary>
        /// The callback operator
        /// </summary>
        Func<double, double, double> _op;

        /// <summary>
        /// Evaluate the operation
        /// </summary>
        /// <returns></returns>
        public override double Eval()
        {
            // Evaluate both sides
            var lhsVal = _lhs.Eval();
            var rhsVal = _rhs.Eval();

            // Evaluate and return
            return _op(lhsVal, rhsVal);
        }
    }
}
