namespace Horus.Domain.SeedWork.Commands
{
	public interface ICommandHandler<in TCommand, TResult> :
		IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
	{
	}
}