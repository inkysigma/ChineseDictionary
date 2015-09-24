﻿using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Query.ExpressionVisitors;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace EntityFramework.Npgsql3.Query
{
    public class NpgsqlQueryCompilationContext : RelationalQueryCompilationContext
    {
        public NpgsqlQueryCompilationContext(
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            [NotNull] IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory,
            [NotNull] IDatabase database,
            [NotNull] ILinqOperatorProvider linqOpeartorProvider,
            [NotNull] IQueryMethodProvider queryMethodProvider)
            : base(
                Check.NotNull(loggerFactory, nameof(loggerFactory)),
                Check.NotNull(entityQueryModelVisitorFactory, nameof(entityQueryModelVisitorFactory)),
                Check.NotNull(requiresMaterializationExpressionVisitorFactory, nameof(requiresMaterializationExpressionVisitorFactory)),
                Check.NotNull(database, nameof(database)),
                Check.NotNull(linqOpeartorProvider, nameof(linqOpeartorProvider)),
                Check.NotNull(queryMethodProvider, nameof(queryMethodProvider)))
        {
        }

        public override bool IsCrossApplySupported => true;
    }
}
