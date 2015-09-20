namespace ChineseDictionary.Models
{
    public class QueryResult
    {
        public int Status { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }

        public QueryResult(int status, string message, string field)
        {
            Field = field;
            Message = message;
            Status = status;
        }

        public static QueryResult Succeded = new QueryResult(QueryStatuses.Succeeded, "Success!", null);

        public static QueryResult EmptyField(string field)
        {
            return new QueryResult(QueryStatuses.EmptyField, field.ToUpperInvariant() + " is emtpy", field);
        }

        public static QueryResult InvalidField(string field)
        {
            return new QueryResult(QueryStatuses.InvalidData, field.ToUpperInvariant() + " is invalid", field);
        }

        public static QueryResult QueryFailed(string message)
        {
            return new QueryResult(QueryStatuses.QueryFailed, message, null);
        }

        public static QueryResult QueryFailed(bool m)
        {
            return m ? Succeded : new QueryResult(QueryStatuses.QueryFailed, "The server was unable to process the query.", null);
        }
    }

    public static class QueryStatuses
    {
        public static int Succeeded = 0;
        public static int EmptyField = 1;
        public static int QueryFailed = 2;
        public static int InvalidData = 3;
        public static int UnknownError = 4;
    }
}
