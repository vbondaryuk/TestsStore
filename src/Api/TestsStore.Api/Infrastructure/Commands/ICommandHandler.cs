using System.Threading.Tasks;

namespace TestsStore.Api.Infrastructure.Commands
{
	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		Task<ICommandResult> ExecuteAsync(TCommand command);
	}

	public interface ICommandHandler<TCommand, TValue> where TCommand : ICommand
	{
		Task<ICommandResult<TValue>> ExecuteAsync(TCommand command);
	}
}