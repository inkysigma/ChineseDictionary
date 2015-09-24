// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Migrations.Operations;

namespace EntityFramework.Npgsql3.Migrations.Operations
{
    public class DropDatabaseOperation : MigrationOperation
    {
        public virtual string Name { get;[param: NotNull] set; }
    }
}
