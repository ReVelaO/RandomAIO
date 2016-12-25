using EloBuddy;
using EloBuddy.SDK;

namespace RandomAIO.Plugins.Yasuo.Addon
{
    public static class DamageHandler
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static float GetQDamage(this Obj_AI_Base obj)
        {
            float i = 0;

            if (!SpellHandler.Q.IsReady()) return i;
            var index = Player.GetSpell(SpellSlot.Q).Level - 1;
            var Base = new float[] { 20, 40, 60, 80, 100 }[index];
            var bonusAd = 1f * Yasuo.FlatPhysicalDamageMod;
            var damage = Yasuo.CalculateDamageOnUnit(obj, DamageType.Physical, Base + bonusAd);

            if (Yasuo.PercentCritDamageMod > 79)
            {
                var crit = (Base + bonusAd) * 0.9f;
                var dmg = crit - (Base + bonusAd) * 0.15f;

                i = dmg;
            }
            else
            {
                i = damage;
            }

            return i;
        }

        public static float GetEDamage(this Obj_AI_Base obj)
        {
            float i = 0;

            if (!SpellHandler.E.IsReady() || obj.IsDashed()) return i;

            var index = Player.GetSpell(SpellSlot.E).Level - 1;
            var Base = new float[] { 70, 90, 110, 130, 150 }[index];
            var bonusAp = 0.60f * Yasuo.FlatMagicDamageMod;
            var damage = Yasuo.CalculateDamageOnUnit(obj, DamageType.Magical, Base + bonusAp);

            i = damage;

            return i;
        }

        public static float GetRDamage(this Obj_AI_Base obj)
        {
            float i = 0;

            if (!SpellHandler.R.IsReady()) return i;
            var index = Player.GetSpell(SpellSlot.R).Level - 1;
            var Base = new float[] { 200, 300, 400 }[index];
            var bonusAd = 1.5f * Yasuo.FlatPhysicalDamageMod;
            var damage = Yasuo.CalculateDamageOnUnit(obj, DamageType.Physical, Base + bonusAd);

            i = damage;

            return i;
        }
    }
}