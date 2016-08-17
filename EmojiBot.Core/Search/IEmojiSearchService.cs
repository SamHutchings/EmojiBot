using EmojiBot.Core.Domain;
using System.Collections.Generic;

namespace EmojiBot.Core.Search
{
	public interface IEmojiSearchService
	{
		bool Index(Emoji emoji);

		bool Index(IEnumerable<Emoji> emoji);

		IEnumerable<Emoji> Search(string[] term);
	}
}
