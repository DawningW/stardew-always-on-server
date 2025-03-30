using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServer.HostAutomatorStages
{
    internal class ProcessMailboxBehaviorLink : BehaviorLink
    {

        public override void Process(BehaviorState state)
        {
            if (ReadyCheckHelper.ShouldDoDayStartedActions)
            {
                ReadyCheckHelper.DoDayStartedActions();
            }
            else
            {
                processNext(state);
            }
        }
    }
}
