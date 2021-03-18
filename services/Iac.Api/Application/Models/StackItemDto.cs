using System.Collections.Generic;

namespace Iac.Api.Application.Models
{
    public class StackItemDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public List<StackItemVariableDto> Variables { get; set; }
    }
}