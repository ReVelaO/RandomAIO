using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace RandomAIO.Plugins.Yasuo.Addon
{
    public static class MenuHandler
    {
        public static Menu menu, combo, last, lane, jungle, misc, draw, flee, ks, wall;

        public static void Load()
        {
            menu = MainMenu.AddMenu("RandomAIO: Yasuo", "index0");

            misc = menu.AddSubMenu("Misc");
            misc.Add("r", new CheckBox("Auto R"));
            misc.AddSeparator(8);
            misc.Add("rmin", new Slider("Auto R at {0} Heroes", 3, 1, 5));
            misc.AddSeparator(8);
            misc.Add("qst", new CheckBox("Try stack Q2"));

            flee = menu.AddSubMenu("Flee");
            flee.Add("e", new CheckBox("Use E"));

            wall = menu.AddSubMenu("Wall");
            wall.Add("aa", new CheckBox("Auto W against AA"));
            /*wall.Add("s", new CheckBox("Auto W against Spells"));
            wall.AddLabel("soon spells!");*/

            combo = menu.AddSubMenu("Combo");
            combo.Add("q", new CheckBox("Use Q"));
            combo.AddSeparator(8);
            combo.Add("q2", new CheckBox("Use Q2"));
            combo.Add("q2block", new CheckBox("Block Q2 in AA Range"));
            combo.AddSeparator(8);
            combo.Add("qe", new CheckBox("Use Q + E"));
            combo.Add("eblock1", new CheckBox("Block E at Turrent"));
            combo.AddSeparator(8);
            combo.Add("qaoesli", new Slider("Q + E at {0} Heroes", 2, 1, 5));
            combo.AddSeparator(8);
            combo.Add("e", new CheckBox("Use E"));
            combo.Add("eblock2", new CheckBox("Block E at Turrent"));
            combo.AddSeparator(8);
            combo.Add("r", new CheckBox("Use R"));
            combo.Add("rexe", new CheckBox("Use R [Execute]"));
            combo.AddSeparator(8);
            combo.Add("rsli", new Slider("R at {0} Heroes", 2, 1, 5));

            last = menu.AddSubMenu("LastHit");
            last.Add("q", new CheckBox("Use Q"));
            last.Add("qmin", new Slider("Q at {0} Minions", 1, 1, 4));

            draw = menu.AddSubMenu("Drawings");
            draw.Add("q", new CheckBox("Draw Q"));
            draw.Add("e", new CheckBox("Draw E"));
            draw.Add("r", new CheckBox("Draw R"));
            draw.Add("di", new CheckBox("Damage Indicator"));

            lane = menu.AddSubMenu("Laneclear");
            lane.Add("q", new CheckBox("Use Q"));
            lane.Add("q2", new CheckBox("Use Q2"));
            lane.Add("qaoe", new CheckBox("Use Q + E"));
            lane.Add("e", new CheckBox("Use E"));
            lane.Add("et", new CheckBox("Block E at Turrent"));
            lane.AddSeparator(8);
            lane.Add("qsli", new Slider("Q at {0} Minions", 2, 1, 4));
            lane.Add("q2sli", new Slider("Q2 at {0} Minions", 4, 1, 10));
            lane.Add("qaoesli", new Slider("Q AOE at {0} Minions", 4, 1, 6));

            ks = menu.AddSubMenu("Kill Steal");
            ks.Add("aa", new CheckBox("Auto AA"));
            ks.Add("q", new CheckBox("Auto Q"));
            ks.Add("q2", new CheckBox("Auto Q2"));
            ks.Add("i", new CheckBox("Auto Ignite"));

            jungle = menu.AddSubMenu("Jungleclear");
            jungle.Add("q", new CheckBox("Use Q"));
            jungle.Add("q2", new CheckBox("Use Q2"));
            jungle.Add("e", new CheckBox("Use E"));
        }
    }
}