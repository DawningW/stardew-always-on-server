using DedicatedServer.Chat;
using DedicatedServer.Utils;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServer.HostAutomatorStages
{
    internal class SkipShippingMenuBehaviorLink : BehaviorLink
    {
        private static MethodInfo info = typeof(ShippingMenu).GetMethod("okClicked", BindingFlags.Instance | BindingFlags.NonPublic);

        private static IModHelper helper;
        private static IMonitor monitor;
        private static EventDrivenChatBox chatBox;

        public SkipShippingMenuBehaviorLink(IModHelper helper, IMonitor monitor, EventDrivenChatBox chatBox, BehaviorLink next = null) : base(next)
        {
            SkipShippingMenuBehaviorLink.helper = helper;
            SkipShippingMenuBehaviorLink.monitor = monitor;
            SkipShippingMenuBehaviorLink.chatBox = chatBox;

            helper.Events.GameLoop.DayEnding += OnDayEndingWorker;
        }

        public override void Process(BehaviorState state)
        {
            if (Game1.activeClickableMenu is ShippingMenu sm)
            {
                if (state.HasBetweenShippingMenusWaitTicks())
                {
                    //state.DecrementBetweenShippingMenusWaitTicks(); // Just click one time
                } else
                {
                    SkipShippingMenu();
                    state.SkipShippingMenu();
                }
            } else
            {
                state.ClearBetweenShippingMenusWaitTicks();
                processNext(state);
            }
        }

        public void OnDayEndingWorker(object sender, DayEndingEventArgs e)
        {
            chatBox?.textBoxEnter("Shipping menu Workaroud, if the host does not click Ok, enter 'okay'");
        }

        public static bool SkipShippingMenu()
        {
            if (Game1.activeClickableMenu is ShippingMenu sm)
            {
                info.Invoke(sm, new object[]{});
                monitor?.Log("SkipShippingMenu-OkClicked", LogLevel.Debug);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
