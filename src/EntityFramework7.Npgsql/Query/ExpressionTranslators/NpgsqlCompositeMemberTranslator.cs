﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Data.Entity.Query.ExpressionTranslators;

namespace EntityFramework.Npgsql3.Query.ExpressionTranslators
{
    public class NpgsqlCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        public NpgsqlCompositeMemberTranslator()
        {
            var npgsqlTranslators = new List<IMemberTranslator>
            {
                new StringLengthTranslator(),
                new DateTimeNowTranslator()
            };

            AddTranslators(npgsqlTranslators);
        }
    }
}
