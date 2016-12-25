using EloBuddy;
using RandomAIO.Common;
using RandomAIO.Plugins.Orianna.Addon;
using System;
using EventHandler = RandomAIO.Plugins.Orianna.Addon.EventHandler;

namespace RandomAIO.Plugins.Orianna
{
    public static class Setup
    {
        public static void Load(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Orianna) return;

            API.Welcome("RandomAIO: Orianna");

            SpellHandler.Load();
            MenuHandler.Load();
            BallHandler.Load();
            EventHandler.Load();
        }
    }
}