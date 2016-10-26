using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExpressionEngine
{
    public class ReflectionContext : IContext
    {
        public ReflectionContext(object targetObject)
        {
            _targetObject = targetObject;
        }

        object _targetObject;

        public double ResolveVariable(string name)
        {
            // Find property
            var pi = _targetObject.GetType().GetProperty(name);
            if (pi == null)
                throw new InvalidDataException($"Unknown variable: '{name}'");

            // Call the property
            return (double)pi.GetValue(_targetObject);
        }

        public double CallFunction(string name, double[] arguments)
        {
            // Find method
            var mi = _targetObject.GetType().GetMethod(name);
            if (mi == null)
                throw new InvalidDataException($"Unknown function: '{name}'");

            // Convert double array to object array
            var argObjs = arguments.Select(x => (object)x).ToArray();

            // Call the method
            return (double)mi.Invoke(_targetObject, argObjs);
        }
    }
}
