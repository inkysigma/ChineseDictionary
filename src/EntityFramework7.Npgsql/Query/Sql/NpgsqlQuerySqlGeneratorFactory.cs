using EntityFramework.Npgsql3.Utilities;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.Sql;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace EntityFramework.Npgsql3.Query.Sql
{
    public class NpgsqlQuerySqlGeneratorFactory : ISqlQueryGeneratorFactory
    {
        private readonly IParameterNameGeneratorFactory _parameterNameGeneratorFactory;

        public NpgsqlQuerySqlGeneratorFactory(
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory)
        {
            Check.NotNull(parameterNameGeneratorFactory, nameof(parameterNameGeneratorFactory));

            _parameterNameGeneratorFactory = parameterNameGeneratorFactory;
        }

        public virtual ISqlQueryGenerator CreateGenerator([NotNull] SelectExpression selectExpression)
            => new NpgsqlQuerySqlGenerator(
                _parameterNameGeneratorFactory,
                Check.NotNull(selectExpression, nameof(selectExpression)));

        public virtual ISqlQueryGenerator CreateRawCommandGenerator(
            [NotNull] SelectExpression selectExpression,
            [NotNull] string sql,
            [NotNull] object[] parameters)
            => new RawSqlQueryGenerator(
                _parameterNameGeneratorFactory,
                Check.NotNull(selectExpression, nameof(selectExpression)),
                Check.NotNull(sql, nameof(sql)),
                Check.NotNull(parameters, nameof(parameters)));
    }
}
