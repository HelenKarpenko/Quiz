using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.API.Models
{
  public class PagedResultModel<T>
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

    public PagedResultModel() { }

    public PagedResultModel(IEnumerable<T> items,
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
