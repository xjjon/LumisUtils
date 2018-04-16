using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Actions
{
    /*
     * Chained Action Handler that will execute each action in the enumerable as long as they return true. Exits and returns false if one fails.
     */
    public class ChainedActionHandler : IActionHandler
    {
        public IEnumerable<IActionHandler> Actions { get; private set; }

        public ChainedActionHandler(IEnumerable<IActionHandler> actionHandlers)
        {
            Actions = actionHandlers;
        }

        public virtual bool Execute()
        {
            return Actions.All(action => action.Execute());
        }
    }
}