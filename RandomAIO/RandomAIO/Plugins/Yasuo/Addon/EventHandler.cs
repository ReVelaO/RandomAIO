using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using RandomAIO.Plugins.Yasuo.Addon.Orb;
using System;
using System.Drawing;

namespace RandomAIO.Plugins.Yasuo.Addon
{
    public static class EventHandler
    {
        private static bool _eSended;

        public static void Load()
        {
            Game.OnTick += OnTick;
            Game.OnTick += OnUpdate;
            Drawing.OnDraw += OnDraw;
            Spellbook.OnCastSpell += OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnBasicAttack += OnBasicAttack;
        }

        private static void OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                Combo.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                LastHit.Get();
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
            Update.Misc();
        }

        private static void OnDraw(EventArgs args)
        {
            if (MenuHandler.draw["q"].Cast<CheckBox>().CurrentValue)
            {
                var spell = SpellHandler.IsQ2Ready ? SpellHandler.Q2 : SpellHandler.Q;
                if (spell.IsReady())
                    spell.DrawRange(Color.SteelBlue);
            }
            if (MenuHandler.draw["e"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.E.IsReady())
                    SpellHandler.E.DrawRange(Color.LightSkyBlue);
            if (MenuHandler.draw["r"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.R.IsReady())
                    SpellHandler.R.DrawRange(Color.RoyalBlue);
        }

        private static void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (!sender.Owner.IsMe) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if ((args.Slot == SpellSlot.E) && MenuHandler.lane["et"].Cast<CheckBox>().CurrentValue)
                    if (args.EndPosition.IsUnderTurret())
                        args.Process = false;

                if ((args.Slot == SpellSlot.E) && MenuHandler.lane["qaoe"].Cast<CheckBox>().CurrentValue)
                {
                    if (SpellHandler.Qaoe.IsReady()
                        &&
                        (args.EndPosition.CountEnemyMinionsInRange(SpellHandler.Qaoe.Range) >=
                         MenuHandler.lane["qaoesli"].Cast<Slider>().CurrentValue))
                        _eSended = true;
                    if (SpellHandler.Qaoe.IsReady() &&
                        (args.EndPosition.CountEnemyMinionsInRange(SpellHandler.Qaoe.Range) <
                         MenuHandler.lane["qaoesli"].Cast<Slider>().CurrentValue))
                        _eSended = false;
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (_eSended && sender.IsMe)
            {
                SpellHandler.Qaoe.Cast();
                _eSended = false;
            }
            if (sender.IsEnemy && sender is AIHeroClient && MenuHandler.wall["s"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.IsSkillCollisionable(350)
                    && SpellHandler.W.IsReady() && (Player.Instance.Distance(sender.ServerPosition) > 265))
                    SpellHandler.W.Cast(Player.Instance.Position.Extend(args.Start, SpellHandler.W.Range).To3D());
        }

        private static void OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe || sender.IsAlly) return;

            if (MenuHandler.wall["aa"].Cast<CheckBox>().CurrentValue)
                if (sender.IsEnemy)
                {
                    var pleb = sender as AIHeroClient;

                    if ((pleb != null) && args.Target.IsMe && (args.Target != null))
                        if (Player.Instance.Distance(pleb.ServerPosition) >= 393)
                            SpellHandler.W.Cast(pleb.Position);
                }
        }
    }
}