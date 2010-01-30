using System.Collections.Generic;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Fitnesse
    {
        public void Init()
        {
            Current.Context = new StaticContext(new Dictionary<string, object>());
            ApplicationStartup.Run();
        }
    }
}