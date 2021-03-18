namespace Iac.Api.Application.Queries.OsQuery.Dto
{
    public class OsFilterDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Version { get; set; }
        public string UserName { get; private set; }
        public string UserId { get; private set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}