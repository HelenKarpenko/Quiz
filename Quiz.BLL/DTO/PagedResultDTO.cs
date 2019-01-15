using System;
using System.Collections.Generic;

namespace Quiz.BLL.DTO
{
	public class PagedResultDTO<T>
	{
		public class PagingInfo
		{
			public int Page { get; set; }
			public int PageSize { get; set; }
			public int PageCount { get; set; }
			public long TotalRecordCount { get; set; }
		}
		public List<T> Data { get; private set; }
		public PagingInfo Paging { get; private set; }
		public PagedResultDTO(IEnumerable<T> items,
							  int page,
							  int pageSize,
							  long totalRecordCount)
		{
			Data = new List<T>(items);
			Paging = new PagingInfo
			{
				Page = page,
				PageSize = pageSize,
				TotalRecordCount = totalRecordCount,
				PageCount = totalRecordCount > 0
				  ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
				  : 0
			};
		}
	}
}
