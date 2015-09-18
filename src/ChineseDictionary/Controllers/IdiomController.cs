using System;
using System.IO;
namespace ChineseDictionary.Controllers {
	public class IdiomController : Controller 
	{
		internal IIdiomManager Manager {get;set;}
		
		public IdiomController(IIdiomManager manager)
		{
			Manager = manager;
		}
		
		[HttpPost]
		public async Task<QueryResult> AddIdiom(Idiom idiom)
		{
			if (idiom == null)
				return QueryResult.Create(QueryResult.Null, nameof(idiom));
			if (!idiom.Validate())
				return QueryResult.Create(QueryResult.Empty, nameof(idiom));
		}
	}
}