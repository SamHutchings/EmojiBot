using EmojiBot.Core.Domain;
using Nest;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmojiBot.Core.Search
{
	public class EmojiSearchService : IEmojiSearchService
	{
		private ElasticClient _client;

		public EmojiSearchService()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("emoji");

			_client = new ElasticClient(settings);
		}

		public bool Index(Emoji emoji)
		{
			return _client.Index(emoji).IsValid;
		}

		public bool Index(IEnumerable<Emoji> emoji)
		{
			return _client.Index(emoji).IsValid;
		}

		public IEnumerable<Emoji> Search(string term)
		{
			return _client.Search<Emoji>(s => s
				.From(0)
				.Size(10)
				.Query(q => q
					.Term("Keywords", term)
				)
			)
			.Documents;
		}
	}
}
