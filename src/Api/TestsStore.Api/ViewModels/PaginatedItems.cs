using System.Collections.Generic;

namespace TestsStore.Api.ViewModels
{
	public class PaginatedItems<T>
	{
		public int PageIndex { get; }

		public int PageSize { get; }

		public long Count { get; }

		public IEnumerable<T> Data { get; }

		public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<T> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Count = count;
			Data = data;
		}
	}
}