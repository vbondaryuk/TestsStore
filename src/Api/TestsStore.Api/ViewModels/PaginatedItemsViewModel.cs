using System.Collections.Generic;

namespace TestsStore.Api.ViewModels
{
	public class PaginatedItemsViewModel<T>
	{
		public int PageIndex { get; }

		public int PageSize { get; }

		public long Count { get; }

		public IEnumerable<T> Data { get; }

		public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<T> data)
		{
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
			this.Count = count;
			this.Data = data;
		}
	}
}