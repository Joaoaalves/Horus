using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.FilePaths.Rules
{
	public class FilePathMustHaveValidLength(string path) : IBusinessRule
	{
		private readonly string _path = path;

		public bool IsBroken() =>
			_path.Length is < 3 or > 260; // Common limit on Windows/Unix

		public string Message => "File path length must be between 3 and 260 characters.";
	}
}