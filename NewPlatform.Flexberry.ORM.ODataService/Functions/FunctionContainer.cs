namespace NewPlatform.Flexberry.ORM.ODataService.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Default implementation of <see cref="IFunctionContainer"/>.
    /// Uses <see cref="ManagementToken"/> for updating current EDM model after registering OData Service function.
    /// </summary>
    /// <seealso cref="IFunctionContainer" />
    internal class FunctionContainer : IFunctionContainer
    {
        /// <summary>
        /// The registered OData Service functions.
        /// </summary>
        private readonly Dictionary<string, Function> _functions = new Dictionary<string, Function>();

        /// <inheritdoc />
        public ManagementToken Token { get; set; }

        /// <inheritdoc />
        public void Register(Function function)
        {
            _functions.Add(function.Name, function);
            Token.Model.AddUserFunction(function);
        }

        /// <inheritdoc />
        public void Register(Delegate function)
        {
            Register(function, false);
        }

        /// <inheritdoc />
        public void RegisterAction(Delegate function)
        {
            Register(function, true);
        }

        /// <inheritdoc />
        public bool IsRegistered(string functionName)
        {
            return _functions.ContainsKey(functionName);
        }

        /// <inheritdoc />
        public Function GetFunction(string functionName)
        {
            return _functions[functionName];
        }

        /// <inheritdoc />
        public IEnumerable<Function> GetFunctions()
        {
            return _functions.Values;
        }

        private void Register(Delegate function, bool createAction)
        {
            var functionName = function.Method.Name;
            var returnType = function.Method.ReturnType;

            var skip = 0;
            var parameters = function.Method.GetParameters();
            if (parameters.Length > 0 && parameters[0].ParameterType == typeof(QueryParameters))
            {
                skip = 1;
            }

            var arguments = parameters.Skip(skip).ToDictionary(i => i.Name, i => i.ParameterType);

            DelegateODataFunction handler = (queryParameters, objects) =>
            {
                var args = new List<object>();
                if (skip == 1)
                {
                    args.Add(queryParameters);
                }

                return function.DynamicInvoke(args.Concat(objects.Values).ToArray());
            };
            if (createAction)
            {
                Register(new Action(functionName, handler, returnType, arguments));
            }
            else
            {
                Register(new Function(functionName, handler, returnType, arguments));
            }
        }
    }
}