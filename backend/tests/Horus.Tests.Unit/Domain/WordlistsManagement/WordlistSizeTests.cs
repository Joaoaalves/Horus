using Horus.Domain.SeedWork;
using Horus.Domain.WordlistsManagement.Wordlists.WordlistSizes;
using Horus.Domain.WordlistsManagement.Wordlists.WordlistSizes.Rules;

namespace Horus.Tests.Unit.Domain.WordlistsManagement
{
	public class WordlistSizeTests
	{
		[Fact]
		public void WordlistSize_ShouldThrowBusinessRuleValidation_WhenSizeIsLessThanOrEqualToZero()
		{
			// Arrange
			int invalidSize = 0;
			var rule = new WordlistSizeMustBeGreaterThanZero(invalidSize);

			// Act & Assert
			var exc = Assert.Throws<BusinessRuleValidationException>(() => WordlistSize.Create(invalidSize));
			Assert.Equal(rule.Message, exc.Message);
		}

		[Fact]
		public void WordlistSize_ShouldBeCreated_WhenSizeIsGreaterThanZero()
		{
			// Arrange
			int validSize = 1000;
			long expectedBytes = validSize * sizeof(char);

			// Act
			var wordlistSize = WordlistSize.Create(validSize);

			// Assert
			Assert.NotNull(wordlistSize);
			Assert.Equal(validSize, wordlistSize.Words);
			Assert.Equal(expectedBytes, wordlistSize.Bytes);
		}

		[Fact]
		public void WordlistSize_Bytes_ShouldBeCalculatedCorrectly()
		{
			// Arrange
			int sizeInWords = 500;
			long expectedBytes = sizeInWords * sizeof(char);
			var wordlistSize = WordlistSize.Create(sizeInWords);

			// Act
			long actualBytes = wordlistSize.Bytes;

			// Assert
			Assert.Equal(expectedBytes, actualBytes);
		}
	}
}