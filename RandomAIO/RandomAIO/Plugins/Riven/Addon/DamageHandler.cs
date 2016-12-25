using EloBuddy;
using EloBuddy.SDK;

namespace RandomAIO.Plugins.Riven.Addon
{
    public static class DamageHandler
    {
        private static AIHeroClient Riven => Player.Instance;

        public static float GetQDamage(this Obj_AI_Base obj)
        {
            if (!SpellHandler.Q.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.Q).Level - 1;
            var Base = new float[] { 10, 30, 50, 70, 90 }[index];
            var bonusAd = new[] { 0.40f, 0.45f, 0.50f, 0.55f, 0.60f }[index] * Riven.FlatPhysicalDamageMod;

            return Riven.CalculateDamageOnUnit(obj, DamageType.Physical, Base + bonusAd);
        }

        public static float GetWDamage(this Obj_AI_Base obj)
        {
            if (!SpellHandler.W.IsReady()) return 0;

            var index = Player.GetSpell(SpellSlot.W).Level - 1;
            var Base = new float[] { 50, 80, 110, 140, 170 }[index];
            var bonusAd = 1f * Riven.FlatPhysicalDamageMod;

            return Riven.CalculateDamageOnUnit(obj, DamageType.Physical, Base + bonusAd);
        }

        public static float GetRDamage(this Obj_AI_Base obj)
        {
            if (!SpellHandler.IsR2Ready) return 0;

            var index = Player.GetSpell(SpellSlot.R).Level - 1;
            var Base = new float[] { 100, 150, 200 }[index];
            var bonusAd = 0.60f * Riven.FlatPhysicalDamageMod;
            var missinghealth = (100 - obj.HealthPercent) * 4 / 100f;

            return Riven.CalculateDamageOnUnit(obj, DamageType.Physical,
                Base + bonusAd + (Base + bonusAd) * missinghealth);
        }
    }
}