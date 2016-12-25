using EloBuddy;
using RandomAIO.Common;
using RandomAIO.Plugins.Quinn.Addon;
using System;
using EventHandler = RandomAIO.Plugins.Quinn.Addon.EventHandler;

namespace RandomAIO.Plugins.Quinn
{
    public static class Setup
    {
        public static void Load(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Quinn) return;

            API.Welcome("RandomAIO: Quinn");

            SpellHandler.Load();
            MenuHandler.Load();
            EventHandler.Load();
        }
    }
}