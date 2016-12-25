using EloBuddy.SDK.Menu.Values;

namespace RandomAIO.Plugins.Quinn.Addon.Orb
{
    public static class Flee
    {
        public static void Get()
        {
            if (MenuHandler.flee["r"].Cast<CheckBox>().CurrentValue)
            {
                if (!SpellHandler.R.IsReady()) return;

                if ((SpellHandler.R.Name.ToLower() == "quinnrfinale")
                    || (SpellHandler.R.Name.ToLower() == "quinnrreturntoquinn")) return;

                SpellHandler.R.Cast();
            }
        }
    }
}