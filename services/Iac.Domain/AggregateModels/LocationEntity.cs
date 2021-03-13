using System.Collections.Generic;
using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class LocationEntity : Entity, IAggregateRoot
    {
        #region | Properties |

        public string Title { get; set; }

        #endregion | Properties |

        #region | Navigations |

        public ICollection<OsEntity> Oses { get; set; }

        #endregion | Navigations |
    }
}