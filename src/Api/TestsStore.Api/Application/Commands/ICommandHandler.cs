using System.Threading.Tasks;

namespace TestsStore.Api.Application.Commands
{
	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		Task<ICommandResult> ExecuteAsync(TCommand command);
	}

	public interface ICommandHandler<TCommand, TResultValue> where TCommand : ICommand
	{
		Task<ICommandResult<TResultValue>> ExecuteAsync(TCommand command);
	}
}