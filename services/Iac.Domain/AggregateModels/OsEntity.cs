using System.Collections.Generic;
using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class OsEntity : Entity, IAggregateRoot
    {
        #region | Properties |

        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Version { get; set; }
        public long LocationId { get; set; }

        #endregion | Properties |

        #region | Navigations |

        public ICollection<StackEntity> Stacks { get; set; }
        public LocationEntity Location { get; set; }

        #endregion | Navigations |
    }
}