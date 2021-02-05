using System.Reflection;
using System.Runtime.InteropServices;
using RT.Json;
using RT.PropellerApi;
using RT.Servers;

[assembly: AssemblyTitle("KtaneTheBurntCounter")]
[assembly: AssemblyProduct("KtaneTheBurntCounter")]
[assembly: AssemblyCopyright("Copyright © BakersDozenBagels 2021")]
[assembly: ComVisible(false)]
[assembly: Guid("48f717f6-ab9f-4dff-9796-218cce1311b3")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace KtaneTheBurnt
{
    public sealed class KtaneTheBurntCounter : PropellerModuleBase<KtaneTheBurntCounterSettings>
    {
        public int count = 0;
        public object lockObject = new object();

        public override string Name { get { return "KTANE The Burnt Counter"; } }

        public override HttpResponse Handle(HttpRequest req)
        {
            lock (lockObject)
            {
                if (req.Headers.ContainsKey("SIMPLEDBADD"))
                    if (req.Headers["SIMPLEDBADD"] == "TRUE")
                        count++;

                if (req.Headers.ContainsKey("SIMPLEDBSUB"))
                    if (req.Headers["SIMPLEDBSUB"] == "TRUE")
                        count--;

                if (count < 0)
                    count = 0;

                return HttpResponse.Json(new JsonDict { ["Count"] = count });
            }
        }
    }

    /// <summary>Contains the settings for the KtaneTheBurntCounter Propeller module.</summary>
    public sealed class KtaneTheBurntCounterSettings
    {
        //Nothing here yet.
    }
}
