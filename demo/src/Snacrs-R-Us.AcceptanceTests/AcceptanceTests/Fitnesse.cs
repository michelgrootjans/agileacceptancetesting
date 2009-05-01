using System.Collections.Generic;
using System.Threading;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    internal static class Fitnesse
    {
        private static bool fitNesseHasBeenInitialized;

        internal static void Init()
        {
            if (fitNesseHasBeenInitialized)
                return;

            Reset();
        }

        internal static void Reset()
        {
            Current.Context = new StaticContext(new Dictionary<string, object>());
            ApplicationStartup.Run();

            fitNesseHasBeenInitialized = true;
        }

        public static void WaitASecond()
        {
            Thread.Sleep(5000);
        }

        public static void AddDemoData()
        {
            Reset();
            ApplicationStartup.AddDemoData();
        }
    }
}