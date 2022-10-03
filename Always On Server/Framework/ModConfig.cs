using StardewModdingAPI;

namespace Always_On_Server.Framework
{
    class ModConfig
    {
        public SButton serverHotKey { get; set; } = SButton.F9;

        public int profitmargin { get; set; } = 100;
        public int upgradeHouse { get; set; } = 0;
        public string petname { get; set; } = "Meow";
        public bool farmcavechoicemushrooms { get; set; } = true;
        public bool communitycenterrun { get; set; } = true;
        public int timeOfDayToSleep { get; set; } = 2200;

        public bool lockPlayerChests { get; set; } = false; // 锁箱似乎全是bug, 最好不要使用
        public bool clientsCanPause { get; set; } = true;
        public bool copyInviteCodeToClipboard { get; set; } = true;


        public bool festivalsOn { get; set; } = true;
        public int eggHuntCountDownConfig { get; set; } = 600;
        public int flowerDanceCountDownConfig { get; set; } = 600;
        public int luauSoupCountDownConfig { get; set; } = 600;
        public int jellyDanceCountDownConfig { get; set; } = 600;
        public int grangeDisplayCountDownConfig { get; set; } = 600;
        public int iceFishingCountDownConfig { get; set; } = 600;
    }
}
