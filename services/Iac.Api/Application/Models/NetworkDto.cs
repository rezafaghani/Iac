using Iac.Domain.Enums;

namespace Iac.Api.Application.Models
{
    public class NetworkDto
    {
        #region | Properties |

        public NetworkEnum NetworkType { get; set; }

        public string Name { get; set; }
        public string Hostname { get; set; }
        public string DomainName { get; set; }
        public string MacAddress { get; set; }
        public string Ipv4Address { get; set; }
        public string Ipv6Address { get; set; }
        public string PrimaryDnsServer { get; set; }
        public string SecondaryDnsServer { get; set; }

        #endregion | Properties |
    }
}