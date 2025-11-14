using Horus.Domain.SeedWork;
using Horus.Domain.SharedKernel.FilePaths;
using Horus.Domain.SharedKernel.FilePaths.Rules;
using Horus.Domain.SharedKernel.SharedRules;
using Horus.Tests.Unit.Builders;

namespace Horus.Tests.Unit.Domain.SharedKernel
{
	public class FilePathTests
	{
		#region Existing Rule Tests
		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFilePathIsNull()
		{
			// Arrange
			string? path = null;
			var rule = new StringCannotBeEmptyOrNull(path!, nameof(FilePath));

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path!));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFilePathIsEmpty()
		{
			// Arrange
			string path = string.Empty;
			var rule = new StringCannotBeEmptyOrNull(path, nameof(FilePath));

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path!));
			Assert.Equal(rule.Message, exc.Message);
		}
		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFilePathIsShort()
		{
			// Arrange
			string path = StringBuilder.Build(1);
			var rule = new FilePathMustHaveValidLength(path);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path!));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFilePathIsLong()
		{
			// Arrange
			string path = StringBuilder.Build(300);
			var rule = new FilePathMustHaveValidLength(path);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path!));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFilePathIsInvalid()
		{
			// Arrange
			string path = "/inv|alid/path";
			var rule = new FilePathCannotContainInvalidChars(path);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void FilePath_ShouldThrowBusinessRuleValidationException_WhenFileNameIsInvalid()
		{
			// Arrange
			string path = "/valid/path/invalid*name.txt";
			var rule = new FileNameCannotContainInvalidChars(path);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => FilePath.FromString(path));
			Assert.Equal(rule.Message, exc.Message);
		}

		#endregion

		#region Factory Method
		[Fact]
		public void FilePath_FromString_ShouldReturnInstanceWithSamePath()
		{

			// Arrange
			string path = "/some/path/file.txt";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.NotNull(filePath);
			Assert.Equal(path, filePath.Value);
		}

		#endregion

		#region FileName
		[Fact]
		public void FilePath_FromString_ShouldReturnInstanceWithSameFileName()
		{

			// Arrange
			string fileName = "file.txt";
			string path = $"/some/path/{fileName}";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.Equal(fileName, filePath.GetFileName());
		}
		#endregion

		#region Extension
		[Fact]
		public void FilePath_FromString_ShouldReturnInstanceWithSameExtension()
		{

			// Arrange
			string extension = ".txt";
			string fileName = $"file{extension}";
			string path = $"/some/path/{fileName}";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.Equal(extension, filePath.GetExtension());
		}

		#endregion

		#region Accepted Chars
		[Fact]
		public void FilePath_FromString_ShouldAcceptUnicodeChars()
		{

			// Arrange
			string path = "/home/user/文件.txt";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.NotNull(filePath);
			Assert.Equal(path, filePath.Value);
		}

		[Fact]
		public void FilePath_FromString_ShouldAcceptValidSpecialChars()
		{

			// Arrange
			string path = "/mnt/drive/!@#$%^&()_-+=[]{};’’,~.txt";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.NotNull(filePath);
			Assert.Equal(path, filePath.Value);
		}
		[Fact]
		public void FilePath_FromString_ShouldAcceptMultipleSlashes() // UNIX
		{

			// Arrange
			string path = "/home/user/multi///slashes///file.txt";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.NotNull(filePath);
		}

		[Fact]
		public void FilePath_FromString_ShouldAcceptWindowsPaths() // Windows
		{

			// Arrange
			string path = "C:\\path\\to\\.hiddenfile";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.NotNull(filePath);
		}
		#endregion

		#region Normalization
		[Fact]
		public void FilePath_FromString_ShouldNormalizePath()
		{

			// Arrange
			string normalized = "/home/user/multi/slashes/file.txt";
			string path = "/home/user/multi///slashes///file.txt";

			// Act
			var filePath = FilePath.FromString(path);

			// Assert
			Assert.Equal(normalized, filePath.Value);
		}
		#endregion
	}
}