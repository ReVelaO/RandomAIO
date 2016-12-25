using EloBuddy;
using EloBuddy.SDK;

namespace RandomAIO.Plugins.Quinn.Addon
{
    public static class DamageHandler
    {
        private static AIHeroClient Quinn => Player.Instance;

        public static float GetQDamage(this Obj_AI_Base obj)
        {
            if (!SpellHandler.Q.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.Q).Level - 1;
            var Base = new float[] { 20, 45, 70, 95, 120 }[index];
            var bonusAd = new[] { 0.80f, 0.90f, 1f, 1.10f, 1.20f }[index] * Quinn.FlatPhysicalDamageMod;
            var bonusAp = 0.50f * Quinn.FlatMagicDamageMod;
            var dmg = Base + bonusAd + bonusAp;

            return Quinn.CalculateDamageOnUnit(obj, DamageType.Physical, dmg);
        }

        public static float GetEDamage(this Obj_AI_Base obj)
        {
            if (!SpellHandler.E.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.E).Level - 1;
            var Base = new float[] { 40, 70, 100, 130, 160 }[index];
            var bonusAd = 0.20f * Quinn.FlatPhysicalDamageMod;
            var dmg = Base + bonusAd;

            return Quinn.CalculateDamageOnUnit(obj, DamageType.Physical, dmg);
        }
    }
}