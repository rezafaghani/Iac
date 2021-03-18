using System.Collections.Generic;
using MediatR;

namespace Iac.Api.Infrastructure.ApiResults
{
    public class PagedResult<TResponse> : IRequest<TResponse>
    {
        public List<TResponse> Result { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}