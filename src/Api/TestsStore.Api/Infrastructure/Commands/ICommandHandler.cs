using System.Threading.Tasks;

namespace TestsStore.Api.Infrastructure.Commands
{
	public interface ICommandHandler<TCommand, TValue> where TCommand : ICommand
	{
		Task<ICommandResult<TValue>> ExecuteAsync(TCommand command);
	}
}