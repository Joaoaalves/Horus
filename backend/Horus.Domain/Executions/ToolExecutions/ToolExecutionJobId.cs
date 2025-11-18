using Horus.Domain.SeedWork;

namespace Horus.Domain.Executions.ToolExecutions
{
	public sealed class ToolExecutionJobId(Guid value) : TypedIdValueBase(value)
	{
		public ToolExecutionJobId() : this(Guid.NewGuid())
		{
		}
	}
}