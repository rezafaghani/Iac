using System.Collections.Generic;
using Iac.Domain.Enums;
using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class NetworkEntity : Entity, IAggregateRoot
    {
        #region | Navigations |

        public ICollection<StackItemsEntity> StackItems { get; set; }

        #endregion | Navigations |

        #region | Properties |

        public NetworkEnum NetworkType { get; set; }

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