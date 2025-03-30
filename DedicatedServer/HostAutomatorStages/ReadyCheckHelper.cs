using Netcode;
using DedicatedServer.Config;
using DedicatedServer.Utils;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DedicatedServer.HostAutomatorStages
{
    internal class ReadyCheckHelper
    {
        private IModHelper helper;
        private IMonitor monitor;
        private ModConfig config;
        static private ReadyCheckHelper instance;
        static public bool ShouldDoDayStartedActions { get; private set; }

        public ReadyCheckHelper(IModHelper helper, IMonitor monitor, ModConfig config)
        {
            this.helper = helper;
            this.monitor = monitor;
            this.config = config;
            instance = this;
        }

        public void Enable()
        {
            helper.Events.GameLoop.DayStarted += OnDayStarted;
        }

        public void Disable()
        {
            helper.Events.GameLoop.DayStarted -= OnDayStarted;
        }

        public void OnDayStarted(object sender, StardewModdingAPI.Events.DayStartedEventArgs e)
        {
            ShouldDoDayStartedActions = true;
        }

        public static void DoDayStartedActions()
        {
            //Checking mailbox sometimes gives some gold, but it's compulsory to unlock some events
            for (int i = 0; i < 10; ++i) {
                Game1.getFarm().mailbox();
            }

            //Unlocks the sewer
            if (!Game1.player.eventsSeen.Contains("295672") && Game1.netWorldState.Value.MuseumPieces.Count() >= 60) {
                Game1.player.eventsSeen.Add("295672");
            }


            if (instance.config?.UpgradeHouseLevelBasedOnFarmhand ?? false)
            {
                HostHouseUpgrade.NeedsUpgrade();
            }

            ShouldDoDayStartedActions = false;
        }

        /// <summary>
        ///         Get whether all required players are ready to proceed.
        /// </summary>
        /// <param name="checkName">The ready check ID.</param>
        /// <returns>
        ///         true : If all required players are ready
        /// <br/>   false: If one required player is not ready</returns>
        public static bool IsReady(string checkName)
        {
            return Game1.netReady.IsReady(checkName);
        }
    }
}
