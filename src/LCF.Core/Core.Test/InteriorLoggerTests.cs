using System.Diagnostics;
using System.Threading.Tasks;

using LCF.Core;

using Xunit;

namespace Core.Test
{
    public class InteriorLoggerTests
    {
        [Fact]
        public async Task GeneralTestsAsync()
        {
            IObjectBase _object = new ObjectBase();

            await Task.Delay(500);


            _object.DisableIntriorLogger();
            Task t = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                    _object.LogInteriorInformation($"Log: {i}");
            });
            Task t1 = Task.Run(() =>
            {
                for (int i = 10000; i < 20000; i++)
                    _object.LogInteriorInformation($"Log: {i}");
            });
            await Task.WhenAll(t, t1);
            _object.EnableInteriorLogger();

            _object.Dispose();

            var _objectLogs = _object.GetInteriorLogs();
            Assert.True(_objectLogs.Count == 2);

            Debug.WriteLine(_objectLogs);

            _object.FlushInteriorLogger();
            Assert.True(_object.GetInteriorLogs().Count == 1);
        }
    }
}
