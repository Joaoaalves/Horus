namespace Horus.Tests.Unit.Builders
{
	public static class StringBuilder
	{
		private static readonly Random _rand = new();

		private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		private const string Numbers = "0123456789";
		private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?/~`áäãâç";

		/// <summary>
		/// Generates a random string with a specified length
		/// </summary>
		/// <param name="length">Length of String.</param>
		/// <param name="includeSpecialChars">Defines if special characters should be included.</param>
		public static string Build(int length = 1, bool includeSpecialChars = false)
		{
			string allowedChars = Letters + Numbers;

			if (includeSpecialChars)
				allowedChars += SpecialChars;

			char[] chars = new char[length];

			for (int i = 0; i < length; i++)
				chars[i] = allowedChars[_rand.Next(0, allowedChars.Length)];

			return new string(chars);
		}
	}
}