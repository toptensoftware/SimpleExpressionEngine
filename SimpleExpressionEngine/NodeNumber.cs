using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleExpressionEngine
{
    /// <summary>
    /// Class NodeNumber for literal number in the expression.
    /// </summary>
    public class NodeNumber : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeNumber" /> class.
        /// </summary>
        /// <param name="number">The number.</param>
        public NodeNumber(double number)
        {
            _number = number;
        }

        /// <summary>
        /// The number.
        /// </summary>
        private double _number;

        /// <summary>
        /// Evaluate the operation.
        /// </summary>
        /// <returns></returns>
        public override double Eval()
        {
            // Just return it.  Too easy.
            return _number;
        }
    }
}
