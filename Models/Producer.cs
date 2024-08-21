using JO_MOVIES.Controllers;
using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
	public class Producer : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		public string FullName { get; set; } = string.Empty;
		public string Bio { get; set; } = string.Empty;
		public string PictureURL { get; set; } = string.Empty;

		
		

		
	}
}
