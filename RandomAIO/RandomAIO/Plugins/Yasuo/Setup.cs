using EloBuddy;
using RandomAIO.Common;
using RandomAIO.Plugins.Yasuo.Addon;
using System;
using EventHandler = RandomAIO.Plugins.Yasuo.Addon.EventHandler;

namespace RandomAIO.Plugins.Yasuo
{
    public static class Setup
    {
        public static void Load(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Yasuo) return;

            API.Welcome("RandomAIO: Yasuo");

            SpellHandler.Load();
            MenuHandler.Load();
            DamageIndicator.Load();
            //SpellDetector.Load();
            EventHandler.Load();
        }
    }
}