using System;

namespace EmojiBot.Core.Extensions
{
	public static class StringExtensions
	{
		public static bool ToBoolean(this string value, bool defaultResult)
		{
			bool result = defaultResult;

			if (Boolean.TryParse(value, out result))
			{
				return result;
			}

			return defaultResult;
		}
	}
}
