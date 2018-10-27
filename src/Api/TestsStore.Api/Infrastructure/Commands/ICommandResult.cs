namespace TestsStore.Api.Infrastructure.Commands
{
	public interface ICommandResult<TValue>
	{
		bool Success { get; set; }
		TValue Result { get; set; }
	}
}