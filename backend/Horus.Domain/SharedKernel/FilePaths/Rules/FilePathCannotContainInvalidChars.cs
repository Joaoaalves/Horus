using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.FilePaths.Rules
{
	public class FilePathCannotContainInvalidChars(string path) : IBusinessRule
	{
		private readonly string _path = path;

		public bool IsBroken()
		{
			var invalidChars = Path.GetInvalidPathChars()
								   .ToArray();

			return _path.Any(c => invalidChars.Contains(c));
		}

		public string Message => "File path contains invalid characters.";
	}
}