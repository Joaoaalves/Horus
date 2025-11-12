using Horus.Domain.SeedWork;

namespace Horus.Domain.WordlistsManagement.Wordlists.WordlistSizes.Rules
{
	public sealed class WordlistSizeMustBeGreaterThanZero(int size) : IBusinessRule
	{
		private readonly int _size = size;
		public string Message => "Wordlist size must be greather than zero.";
		public bool IsBroken() => _size < 1;
	}
}