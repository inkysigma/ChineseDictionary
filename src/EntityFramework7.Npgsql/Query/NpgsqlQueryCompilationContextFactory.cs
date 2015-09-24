﻿using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Query.ExpressionVisitors;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace EntityFramework.Npgsql3.Query
{
    public class NpgsqlQueryCompilationContextFactory : IQueryCompilationContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IEntityQueryModelVisitorFactory _entityQueryModelVisitorFactory;
        private readonly IRequiresMaterializationExpressionVisitorFactory _requiresMaterializationExpressionVisitorFactory;

        public NpgsqlQueryCompilationContextFactory(
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            [NotNull] IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory)
        {
            Check.NotNull(loggerFactory, nameof(loggerFactory));
            Check.NotNull(entityQueryModelVisitorFactory, nameof(entityQueryModelVisitorFactory));
            Check.NotNull(requiresMaterializationExpressionVisitorFactory, nameof(requiresMaterializationExpressionVisitorFactory));

            _loggerFactory = loggerFactory;
            _entityQueryModelVisitorFactory = entityQueryModelVisitorFactory;
            _requiresMaterializationExpressionVisitorFactory = requiresMaterializationExpressionVisitorFactory;
        }

        public virtual QueryCompilationContext Create(IDatabase database, bool async)
            => async
                ? new NpgsqlQueryCompilationContext(
                    _loggerFactory,
                    _entityQueryModelVisitorFactory,
                    _requiresMaterializationExpressionVisitorFactory,
                    database,
                    new AsyncLinqOperatorProvider(),
                    new AsyncQueryMethodProvider())
                : new NpgsqlQueryCompilationContext(
                    _loggerFactory,
                    _entityQueryModelVisitorFactory,
                    _requiresMaterializationExpressionVisitorFactory,
                    database,
                    new LinqOperatorProvider(),
                    new QueryMethodProvider());
    }
}
