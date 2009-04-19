using System.Collections.Generic;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Init
    {
        private static bool fitNesseHasBeenInitialized;

        public static void FitNesseTests()
        {
            if(fitNesseHasBeenInitialized) 
                return;

            ApplicationStartup.Run();
            Current.Context = new StaticContext(new Dictionary<string, object>());

            fitNesseHasBeenInitialized = true;
        }
    }
}