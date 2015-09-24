﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.ExpressionTranslators;

namespace EntityFramework.Npgsql3.Query.ExpressionTranslators
{
    public class StringReplaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo = typeof(string).GetTypeInfo().GetDeclaredMethods(nameof(string.Replace))
            .Where(m => m.GetParameters()[0].ParameterType == typeof(string))
            .Single();

        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method == _methodInfo)
            {
                var sqlArguments = new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments);
                return new SqlFunctionExpression("REPLACE", methodCallExpression.Type, sqlArguments);
            }

            return null;
        }
    }
}
