using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.FilePaths.Rules
{
	public class FilePathCannotBeNull(string? path) : IBusinessRule
	{
		private readonly string? _path = path;

		public bool IsBroken() => string.IsNullOrWhiteSpace(_path);

		public string Message => "File path cannot be null or empty.";
	}
}