using EloBuddy;
using RandomAIO.Common;
using RandomAIO.Plugins.Riven.Addon;
using System;
using EventHandler = RandomAIO.Plugins.Riven.Addon.EventHandler;

namespace RandomAIO.Plugins.Riven
{
    public static class Setup
    {
        public static void Load(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Riven) return;

            API.Welcome("RandomAIO: Riven");

            SpellHandler.Load();
            MenuHandler.Load();
            EventHandler.Load();
        }
    }
}