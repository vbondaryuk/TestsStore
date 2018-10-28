namespace TestsStore.Api.Infrastructure.Commands
{
	public interface ICommandResult
	{
		bool Success { get; set; }
	}

	public interface ICommandResult<TValue> : ICommandResult
	{
		TValue Result { get; set; }
	}
}