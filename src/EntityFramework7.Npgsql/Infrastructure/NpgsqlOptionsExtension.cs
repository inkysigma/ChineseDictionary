// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.Entity.Infrastructure
{
    public class NpgsqlOptionsExtension : RelationalOptionsExtension
    {
        public NpgsqlOptionsExtension()
        {
        }

        public NpgsqlOptionsExtension([NotNull] NpgsqlOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.AddNpgsql();
        }
    }
}
