using Horus.Domain.SeedWork;

namespace Horus.Domain.SharedKernel.EntityDescriptions.Rules
{
	public sealed class EntityDescriotionCannotBeNull(string value) : IBusinessRule
	{
		private readonly string _description = value;
		public string Message => "Description cannot be null or empty";

		public bool IsBroken() => String.IsNullOrEmpty(_description);
	}
}