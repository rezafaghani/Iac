using System.Collections.Generic;

namespace Iac.Api.Application.Models
{
    public class StackDto
    {
        public string StackName { get; set; }
        public long OsId { get; set; }
        public List<StackItemDto> Items { get; set; }
        public NetworkDto Network { get; set; }
    }
}