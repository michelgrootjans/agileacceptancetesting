using System.Collections.Generic;
using System.Threading;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public static class Fitnesse
    {
        private static bool fitNesseHasBeenInitialized;

        internal static void Init()
        {
            if(fitNesseHasBeenInitialized) 
                return;

            Thread.Sleep(5000);
            Current.Context = new StaticContext(new Dictionary<string, object>());
            ApplicationStartup.Run();

            fitNesseHasBeenInitialized = true;
        }
    }
}