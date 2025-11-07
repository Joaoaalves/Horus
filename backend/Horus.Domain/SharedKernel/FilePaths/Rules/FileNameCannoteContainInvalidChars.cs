using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.FilePaths.Rules
{
	public sealed class FileNameCannotContainInvalidChars(string fileName) : IBusinessRule
	{
		private readonly string _fileName = fileName;
		public string Message => "File Name contains invalid characters.";

		public bool IsBroken()
		{
			var invalidChars = Path.GetInvalidFileNameChars().ToArray();

			return _fileName.Any(c => invalidChars.Contains(c));
		}
	}
}