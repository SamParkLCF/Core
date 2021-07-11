using System.Diagnostics;

using Humanizer;

using LCF.Core;

using Xunit;

namespace Core.Test
{
    public class ObjectMemoryTests
    {
        [Fact]
        public void Test()
        {
            IObjectBase _object = new ObjectBase();

            Debug.WriteLine(_object.GetMyGeneration());
            Debug.WriteLine(_object.GetAllocatedBytesForCurrentThread());
            Debug.WriteLine(_object.GetTotalMemory_WITH_ForceFullCollection());
            Debug.WriteLine(_object.GetTotalMemory_WITHOUT_ForceFullCollection());
            Debug.WriteLine(_object.GetTotalAllocatedBytes_WITH_Precise());
            Debug.WriteLine(_object.GetTotalAllocatedBytes_WITHOUT_Precise());

            Debug.WriteLine(_object.GetMyGeneration());
            Debug.WriteLine(_object.GetAllocatedBytesForCurrentThread().Humanize());
            Debug.WriteLine(_object.GetTotalMemory_WITH_ForceFullCollection().Humanize());
            Debug.WriteLine(_object.GetTotalMemory_WITHOUT_ForceFullCollection().Humanize());
            Debug.WriteLine(_object.GetTotalAllocatedBytes_WITH_Precise().Humanize());
            Debug.WriteLine(_object.GetTotalAllocatedBytes_WITHOUT_Precise().Humanize());

            _object.Dispose();
            Debug.WriteLine(_object.GetObjectSummary());
        }
    }
}
