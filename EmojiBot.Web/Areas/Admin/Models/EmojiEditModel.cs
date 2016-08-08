using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmojiBot.Web.Areas.Admin.Models
{
	public class EmojiEditModel
	{
		/// <summary>
		/// The actual characters for the emoji
		/// </summary>
		[Display(Name = "Characters")]
		public string Characters { get; set; }

		/// <summary>
		/// The name of the emoji, e.g. crying laughing emoji
		/// </summary>
		public string Name { get; set; }

		public string Description { get; set; }

		/// <summary>
		/// Search keywords for the emoji
		/// </summary>
		public string Keywords { get; set; }
	}
}