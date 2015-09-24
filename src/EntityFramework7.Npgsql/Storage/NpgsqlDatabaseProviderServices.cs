// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using EntityFramework.Npgsql3.Metadata;
using EntityFramework.Npgsql3.Migrations;
using EntityFramework.Npgsql3.Query;
using EntityFramework.Npgsql3.Query.ExpressionTranslators;
using EntityFramework.Npgsql3.Query.Sql;
using EntityFramework.Npgsql3.Update;
using EntityFramework.Npgsql3.Utilities;
using EntityFramework.Npgsql3.ValueGeneration;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Query.ExpressionTranslators;
using Microsoft.Data.Entity.Query.Sql;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.ValueGeneration;

namespace EntityFramework.Npgsql3.Storage
{
    public class NpgsqlDatabaseProviderServices : RelationalDatabaseProviderServices
    {
        public NpgsqlDatabaseProviderServices([NotNull] IServiceProvider services)
            : base(services)
        {
        }

        public override string InvariantName => GetType().GetTypeInfo().Assembly.GetName().Name;
        public override IDatabaseCreator Creator => GetService<NpgsqlDatabaseCreator>();
        public override IRelationalConnection RelationalConnection => GetService<NpgsqlDatabaseConnection>();
        public override IRelationalDatabaseCreator RelationalDatabaseCreator => GetService<NpgsqlDatabaseCreator>();
        public override IConventionSetBuilder ConventionSetBuilder => GetService<NpgsqlConventionSetBuilder>();
        public override IMigrationsAnnotationProvider MigrationsAnnotationProvider => GetService<NpgsqlMigrationsAnnotationProvider>();
        public override IHistoryRepository HistoryRepository => GetService<NpgsqlHistoryRepository>();
        public override IMigrationsSqlGenerator MigrationsSqlGenerator => GetService<NpgsqlMigrationsSqlGenerator>();
        public override IModelSource ModelSource => GetService<NpgsqlModelSource>();
        public override IUpdateSqlGenerator UpdateSqlGenerator => GetService<NpgsqlUpdateSqlGenerator>();
        public override IValueGeneratorCache ValueGeneratorCache => GetService<NpgsqlValueGeneratorCache>();
        public override IRelationalTypeMapper TypeMapper => GetService<NpgsqlTypeMapper>();
        public override IModificationCommandBatchFactory ModificationCommandBatchFactory => GetService<NpgsqlModificationCommandBatchFactory>();
        public override IRelationalValueBufferFactoryFactory ValueBufferFactoryFactory => GetService<TypedRelationalValueBufferFactoryFactory>();
        public override IRelationalMetadataExtensionProvider MetadataExtensionProvider => GetService<NpgsqlMetadataExtensionProvider>();
        public override IMethodCallTranslator CompositeMethodCallTranslator => GetService<NpgsqlCompositeMethodCallTranslator>();
        public override IMemberTranslator CompositeMemberTranslator => GetService<NpgsqlCompositeMemberTranslator>();
        public override IExpressionFragmentTranslator CompositeExpressionFragmentTranslator => GetService<NpgsqlCompositeExpressionFragmentTranslator>();
        public override IQueryCompilationContextFactory QueryCompilationContextFactory => GetService<NpgsqlQueryCompilationContextFactory>();
        public override ISqlQueryGeneratorFactory SqlQueryGeneratorFactory => GetService<NpgsqlQuerySqlGeneratorFactory>();
    }
}
