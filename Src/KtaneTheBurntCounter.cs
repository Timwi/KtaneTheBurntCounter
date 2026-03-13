using RT.Json;
using RT.PropellerApi;
using RT.Servers;

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
