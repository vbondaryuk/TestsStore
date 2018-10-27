namespace TestsStore.Api.Infrastructure.Commands
{
	public class CommandResult<TValue> : ICommandResult<TValue>
	{
		public bool Success { get; set; }
		public TValue Result { get; set; }

		public CommandResult(bool success, TValue result)
		{
			Success = success;
			Result = result;
		}
	}
}