using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace RandomAIO.Plugins.Riven.Addon
{
    public static class MenuHandler
    {
        public static Menu menu, combo, lane, jungle, draw, ks, flee;

        public static void Load()
        {
            menu = MainMenu.AddMenu("Riven", "index");

            combo = menu.AddSubMenu("Combo");
            combo.Add("q", new CheckBox("Use Q"));
            combo.Add("w", new CheckBox("Use W"));
            combo.Add("e", new CheckBox("Use E"));
            combo.Add("r", new CheckBox("Use R"));
            combo.AddSeparator(14);
            combo.Add("tiamat", new CheckBox("Use Tiamat"));
            combo.Add("hydra", new CheckBox("Use Ravenous Hydra"));

            lane = menu.AddSubMenu("Laneclear");
            lane.Add("q", new CheckBox("Use Q"));
            lane.AddSeparator(8);
            lane.Add("w", new CheckBox("Use W"));
            lane.Add("wmin", new Slider("Use W at {0} Minions", 3, 1, 6));

            jungle = menu.AddSubMenu("Jungleclear");
            jungle.Add("q", new CheckBox("Use Q"));
            jungle.Add("w", new CheckBox("Use W"));

            draw = menu.AddSubMenu("Drawings");
            draw.Add("r2", new CheckBox("Draw R2"));

            ks = menu.AddSubMenu("Kill Steal");
            ks.Add("aa", new CheckBox("Auto AA"));
            ks.Add("w", new CheckBox("Auto W"));
            ks.Add("r2", new CheckBox("Auto R2"));
            ks.Add("i", new CheckBox("Auto Ignite"));

            flee = menu.AddSubMenu("Flee");
            flee.Add("spells", new CheckBox("Use Q + E"));
        }
    }
}