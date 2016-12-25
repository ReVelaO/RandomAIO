using EloBuddy;
using EloBuddy.SDK.Events;
using System;

namespace RandomAIO
{
    internal class Program
    {
        private static void Main()
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            switch (Player.Instance.Hero)
            {
                case Champion.Riven:
                    Plugins.Riven.Setup.Load(args);
                    break;

                /*case Champion.Orianna:
                    Plugins.Orianna.Setup.Load(args);
                    break;*/

                case Champion.Quinn:
                    Plugins.Quinn.Setup.Load(args);
                    break;

                case Champion.Yasuo:
                    Plugins.Yasuo.Setup.Load(args);
                    break;
            }
        }
    }
}