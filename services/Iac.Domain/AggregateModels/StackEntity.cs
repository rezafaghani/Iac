using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class StackEntity : Entity, IAggregateRoot
    {
        #region | Properties |

        public string StackName { get; set; }
        public long OsId { get; set; }

        #endregion | Properties |

        #region | Navigations |

        public OsEntity Os { get; set; }

        #endregion | Navigations |
    }
}