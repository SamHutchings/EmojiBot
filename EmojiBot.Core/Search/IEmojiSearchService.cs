using EmojiBot.Core.Domain;
using System.Collections.Generic;

namespace EmojiBot.Core.Search
{
	public interface IEmojiSearchService
	{
		bool Index(Emoji emoji);

		IEnumerable<Emoji> Search(string term);
	}
}
