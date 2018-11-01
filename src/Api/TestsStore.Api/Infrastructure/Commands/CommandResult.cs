namespace TestsStore.Api.Infrastructure.Commands
{
	public class CommandResult : ICommandResult
	{
		public CommandResult(bool success)
		{
			Success = success;
		}

		public bool Success { get; }
	}

	public class CommandResult<TValue> : CommandResult, ICommandResult<TValue>
	{
		public CommandResult(bool success, TValue result) : base(success)
		{
			Result = result;
		}

		public TValue Result { get; }
	}
}