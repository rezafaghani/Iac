using Iac.Domain.SeedWork;

namespace Iac.Domain.AggregateModels
{
    public class StackItemVariablesEntity : Entity, IAggregateRoot
    {
        #region | Navigations |

        public StackItemsEntity StackItem { get; set; }

        #endregion | Navigations |

        #region | Properties |

        public string Name { get; set; }
        public string Value { get; set; }
        public long StackItemId { get; set; }

        #endregion | Properties |
    }
}