using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.FilePaths
{
	public sealed class FilePath : ValueObject
	{
		public string Value { get; }

		private FilePath(string value)
		{
			Value = value;
		}

		public static FilePath FromString(string value)
		{
			CheckRule(new Rules.FilePathCannotBeNull(value));
			CheckRule(new Rules.FilePathMustHaveValidLength(value));

			// Validate directory
			var directory = Path.GetDirectoryName(value);
			CheckRule(new Rules.FilePathCannotContainInvalidChars(directory!));

			// Validate filename
			var filename = Path.GetFileName(value);
			CheckRule(new Rules.FileNameCannotContainInvalidChars(filename));

			return new FilePath(Normalize(value));
		}

		public string GetExtension() => Path.GetExtension(Value);

		public string GetFileName() => Path.GetFileName(Value);

		public string GetDirectoryName() => Path.GetDirectoryName(Value) ?? string.Empty;

		public override string ToString() => Value;

		private static string Normalize(string path)
		{
			// Removes redundant slashes, trims spaces, normalizes separators
			path = path.Trim();
			path = path.Replace("\\", "/");

			// Removes double slashes except protocol (not relevant here)
			while (path.Contains("//"))
				path = path.Replace("//", "/");

			return path;
		}
	}
}