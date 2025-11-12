using Horus.Domain.SeedWork;

namespace Horus.Domain.WordlistsManagement.Wordlists.WordlistSizes
{
	public sealed class WordlistSize : ValueObject
	{
		public long Words { get; }
		public long Bytes => Words * sizeof(char);

		private WordlistSize(uint value)
		{
			Words = value;
		}

		public static WordlistSize Create(int value)
		{
			CheckRule(new Rules.WordlistSizeMustBeGreaterThanZero(value));
			return new WordlistSize((uint)value);
		}
	}
}