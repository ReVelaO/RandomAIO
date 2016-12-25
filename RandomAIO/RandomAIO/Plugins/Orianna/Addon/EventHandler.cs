using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using RandomAIO.Plugins.Orianna.Addon.Orb;
using System;
using System.Drawing;

namespace RandomAIO.Plugins.Orianna.Addon
{
    public static class EventHandler
    {
        private static AIHeroClient Orianna => Player.Instance;

        public static void Load()
        {
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBasicAttack += OnBasicAttack;
            Spellbook.OnCastSpell += Spellbook_OnCastSpell;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (MenuHandler.mshock["sf"].Cast<KeyBind>().CurrentValue)
            {
                UtilsHandler.WillShock = true;
                UtilsHandler.Shockwave();
            }
            if (UtilsHandler.HasIgnite)
                if (MenuHandler.msum["i"].Cast<CheckBox>().CurrentValue)
                    UtilsHandler.AutoIgnite();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                Combo.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                Laneclear.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                Jungleclear.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                if (MenuHandler.mflee["w"].Cast<CheckBox>().CurrentValue && SpellHandler.W.IsReady())
                    if (Player.Instance.HasBall())
                        SpellHandler.W.Cast();
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Orianna.IsDead) return;

            if (MenuHandler.mdrawings["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.Q.IsReady())
                    SpellHandler.Q.DrawRange(Color.FromArgb(130, Color.OrangeRed));
            if (MenuHandler.mdrawings["ball"].Cast<CheckBox>().CurrentValue)
            {
                BallHandler.Ball.DrawCircle(410, SharpDX.Color.MediumPurple);
                BallHandler.Ball.DrawCircle(250, SharpDX.Color.Purple);
            }
            if (MenuHandler.mshock["sfr"].Cast<CheckBox>().CurrentValue)
                if (UtilsHandler.HasFlash)
                    if (UtilsHandler.Flash.IsReady())
                        Circle.Draw(SharpDX.Color.LightYellow, 800, Player.Instance.Position);
        }

        private static void OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly || sender.IsMe) return;

            if (!SpellHandler.E.IsReady()) return;

            if (sender.IsEnemy)
            {
                if (args.Target.IsMe && (args.Target != null))
                {
                    if (MenuHandler.mshield["b"].Cast<CheckBox>().CurrentValue)
                        if (sender is AIHeroClient)
                            SpellHandler.E.Cast(Orianna);

                    if (MenuHandler.mshield["m"].Cast<CheckBox>().CurrentValue)
                        if (sender.IsMinion && (sender.CountEnemyMinionsInRange(433) > 3))
                            SpellHandler.E.Cast(Orianna);
                }
                if (args.Target.IsAlly && !args.Target.IsMe && (args.Target != null) &&
                    args.Target.IsInRange(Orianna, SpellHandler.E.Range))
                    if (MenuHandler.mshield["ba"].Cast<CheckBox>().CurrentValue)
                        if (sender is AIHeroClient)
                        {
                            var ally = args.Target as Obj_AI_Base;

                            SpellHandler.E.Cast(ally);
                        }
            }
        }

        private static void Spellbook_OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if ((args.Slot == SpellSlot.R)
                && MenuHandler.mcombo["rblock"].Cast<CheckBox>().CurrentValue
                && (BallHandler.RBall.CountEnemyHeroesNear == 0) && !UtilsHandler.WillShock)
                args.Process = false;
        }
    }
}