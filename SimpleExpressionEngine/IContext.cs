using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExpressionEngine
{
    public interface IContext
    {
        double ResolveVariable(string name);
    }
}
