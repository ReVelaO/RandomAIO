using EloBuddy;

namespace RandomAIO.Plugins.Riven.Addon.Orb
{
    public static class Flee
    {
        private static bool _casted;
        private static AIHeroClient Riven => Player.Instance;
        private static int Buff => Riven.GetBuffCount("RivenTriCleave");

        public static void Get()
        {
            if (SpellHandler.Q.IsReady())
                if ((Buff != 2) || _casted)
                {
                    SpellHandler.Q.Cast(Game.CursorPos);
                    _casted = false;
                }
            if (SpellHandler.E.IsReady())
                if (Buff == 2 && !_casted)
                {
                    SpellHandler.E.Cast(Game.CursorPos);
                    _casted = true;
                }
        }
    }
}