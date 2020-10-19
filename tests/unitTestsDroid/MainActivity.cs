using System.Reflection;

using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;

namespace unitTestsDroid
{
    [Activity(Label = "unitTestsDroid", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var nUnitAssembly = typeof(NUnitLite.Runner.CommandLineOptions);
            var nUnitAssembly2 = typeof(NUnit.Framework.Api.AttributeDictionary);
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            AddTest (typeof (unitTestsLibrary.MostBasicTests).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}
