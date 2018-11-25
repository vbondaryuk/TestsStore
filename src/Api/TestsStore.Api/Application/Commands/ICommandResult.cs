namespace TestsStore.Api.Application.Commands
{
	public interface ICommandResult
	{
		bool Success { get; }
	}

	public interface ICommandResult<TValue> : ICommandResult
	{
		TValue Result { get; }
	}
}