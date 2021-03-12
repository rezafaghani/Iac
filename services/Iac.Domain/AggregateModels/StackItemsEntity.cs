using System.Collections.Generic;
using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class StackItemsEntity : Entity, IAggregateRoot
    {
        #region | Properties |

        public string Title { get; set; }
        public string ImageName { get; set; }
        public long StackId { get; set; }
        public long NetworkId { get; set; }

        #endregion | Properties |

        #region | Navigations |

        public ICollection<StackItemVariablesEntity> Variables { get; set; }
        public StackEntity Stack { get; set; }
        public NetworkEntity Network { get; set; }

        #endregion
    }
}