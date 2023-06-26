namespace MagicVilla_API.Models
{
	public class APIResponse
	{
		public bool IsSuccessful { get; set; }
		public List<string> ErrorMessages { get; set; }
		public object Result { get; set; }
	}
}

