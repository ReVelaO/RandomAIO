using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomAIO.Plugins.Quinn.Addon
{
    public static class MenuHandler
    {
        public static Menu menu, combo, lane, jungle, ks, draw, flee;

        public static void Load()
        {
            menu = MainMenu.AddMenu("Quinn#", "index");

            flee = menu.AddSubMenu("Flee");
            flee.Add("r", new CheckBox("Use R"));

            combo = menu.AddSubMenu("Combo");
            combo.Add("q", new CheckBox("Use Q"));
            combo.Add("e", new CheckBox("Use E"));

            draw = menu.AddSubMenu("Drawings");
            draw.Add("q", new CheckBox("Draw Q"));
            draw.Add("e", new CheckBox("Draw E"));

            lane = menu.AddSubMenu("Laneclear");
            lane.Add("q", new CheckBox("Use Q"));
            lane.Add("e", new CheckBox("Use E"));
            lane.AddLabel("Laneclear is capped at 59% mana. below than it, will stop casting spells");

            ks = menu.AddSubMenu("Kill Steal");
            ks.Add("aa", new CheckBox("Auto AA"));
            ks.Add("q", new CheckBox("Auto Q"));
            ks.Add("e", new CheckBox("Auto E"));

            jungle = menu.AddSubMenu("Jungleclear");
            jungle.Add("q", new CheckBox("Use Q"));
            jungle.Add("e", new CheckBox("Use E"));
        }
    }
}