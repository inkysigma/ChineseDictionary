﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.ExpressionTranslators;

namespace EntityFramework.Npgsql3.Query.ExpressionTranslators
{
    public class StringLengthTranslator : IMemberTranslator
    {
        public virtual Expression Translate([NotNull] MemberExpression memberExpression)
        {
            if (memberExpression.Expression != null
                && memberExpression.Expression.Type == typeof(string)
                && memberExpression.Member.Name == "Length")
            {
                return new SqlFunctionExpression("length", memberExpression.Type, new[] { memberExpression.Expression });
            }

            return null;
        }
    }
}
