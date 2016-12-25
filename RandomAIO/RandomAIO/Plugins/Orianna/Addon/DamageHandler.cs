using EloBuddy;
using EloBuddy.SDK;

namespace RandomAIO.Plugins.Orianna.Addon
{
    public static class DamageHandler
    {
        private static AIHeroClient Orianna => Player.Instance;

        public static float Q(Obj_AI_Base e)
        {
            if (!SpellHandler.Q.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.Q).Level - 1;
            var Base = new float[] { 60, 90, 120, 150, 180 }[index];
            var bonusAp = 0.5f * Orianna.FlatMagicDamageMod;

            return Orianna.CalculateDamageOnUnit(e, DamageType.Magical, Base + bonusAp);
        }

        public static float W(Obj_AI_Base e)
        {
            if (!SpellHandler.W.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.W).Level - 1;
            var Base = new float[] { 75, 115, 160, 205, 250 }[index];
            var bonusAp = 0.7f * Orianna.FlatMagicDamageMod;

            return Orianna.CalculateDamageOnUnit(e, DamageType.Magical, Base + bonusAp);
        }

        public static float R(Obj_AI_Base e)
        {
            if (!SpellHandler.R.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.R).Level - 1;
            var Base = new float[] { 150, 225, 300 }[index];
            var bonusAp = 0.7f * Orianna.FlatMagicDamageMod;

            return Orianna.CalculateDamageOnUnit(e, DamageType.Magical, Base + bonusAp);
        }
    }
}