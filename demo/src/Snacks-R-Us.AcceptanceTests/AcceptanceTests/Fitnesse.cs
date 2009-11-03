using System.Collections.Generic;
using System.Threading;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    internal static class Fitnesse
    {
        internal static void Init()
        {
            Current.Context = new StaticContext(new Dictionary<string, object>());
            ApplicationStartup.Run();
        }

        internal static void WaitASecond()
        {
            Thread.Sleep(5000);
        }
    }
}