using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Plugins.Quinn.Addon.Orb;
using System;
using System.Drawing;

namespace RandomAIO.Plugins.Quinn.Addon
{
    public static class EventHandler
    {
        public static void Load()
        {
            Game.OnTick += OnTick;
            Game.OnUpdate += OnUpdate;
            Orbwalker.OnPostAttack += OnAfterAttack;
            Drawing.OnDraw += OnDraw;
        }

        private static void OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                Laneclear.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                Jungleclear.Get();

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                Flee.Get();
        }

        private static void OnUpdate(EventArgs args)
        {
            Update.KillSteal();
        }

        private static void OnDraw(EventArgs args)
        {
            if (SpellHandler.Q.IsReady())
                SpellHandler.Q.DrawRange(Color.FromArgb(130, Color.LightBlue));

            if (SpellHandler.E.IsReady())
                SpellHandler.E.DrawRange(Color.FromArgb(130, Color.RoyalBlue));
        }

        private static void OnAfterAttack(AttackableUnit unit, EventArgs args)
        {
            if (MenuHandler.combo["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.Q.IsReady())
                {
                    var target = unit as AIHeroClient;
                    if ((target != null) && target.IsInRange(Player.Instance, SpellHandler.Q.Range))
                    {
                        var p = SpellHandler.Q.GetPrediction(target);
                        if ((p != null) && (p.HitChance > HitChance.Medium))
                            SpellHandler.Q.Cast(p.CastPosition);
                    }
                }
            if (MenuHandler.combo["e"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.E.IsReady())
                {
                    var target = unit as AIHeroClient;
                    if ((target != null) && target.IsInRange(Player.Instance, SpellHandler.E.Range))
                        SpellHandler.E.Cast(target);
                }
        }
    }
}