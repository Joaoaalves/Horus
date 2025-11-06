namespace Horus.Domain.SeedWork.Mediator
{
	public interface IUnitOfWork
	{
		Task<int> CommitAsync(CancellationToken cancellationToken = default);
		Task RevertAsync();
	}
}