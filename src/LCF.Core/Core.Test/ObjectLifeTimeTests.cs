using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using LCF.Core;

using Xunit;

namespace Core.Test
{
    public class ObjectLifeTimeTests
    {
        [Fact]
        internal IObjectBase ObjectInitializingTest()
        {
            IObjectBase _object = new ObjectBase();

            Assert.NotNull(_object);
            Assert.True(_object.ObjectId != Guid.Empty);
            Assert.True(Guid.TryParse(_object.ObjectId.ToString(), out _));

            return _object;
        }

        [Fact]
        internal async Task<IObjectBase> ObjectBornTestAsync()
        {
            IObjectBase _object = ObjectInitializingTest();

            Assert.True(_object.IsObjectAlive);
            Assert.True(_object.ObjectBirthTime != DateTime.MinValue);
            Assert.True(_object.ObjectBirthTime < DateTime.Now);
            await Task.Delay(500);
            Assert.True(_object.ObjectLiveTime > TimeSpan.FromMilliseconds(500));
            Assert.True(_object.ObjectDeathTime == DateTime.MinValue);

            return _object;
        }

        [Fact]
        internal async Task<IObjectBase> ObjectUsingDisposeTestAsync()
        {
            IObjectBase _object = await ObjectBornTestAsync();

            _object.Dispose();

            return _object;
        }
        [Fact]
        internal async Task<IObjectBase> ObjectUsingDisposeAsyncTestAsync()
        {
            IObjectBase _object = await ObjectBornTestAsync();

            await _object.DisposeAsync();

            return _object;
        }
        [Fact]
        internal async Task ObjectDeathTestAsync()
        {
            IObjectBase _objectUsingDispose = await ObjectUsingDisposeTestAsync();
            IObjectBase _objectUsingDisposeAsync = await ObjectUsingDisposeAsyncTestAsync();

            AssertDispose(_objectUsingDispose);
            AssertDispose(_objectUsingDisposeAsync);

            static void AssertDispose(IObjectBase objectBase)
            {
                Assert.False(objectBase.IsObjectAlive);
                Assert.True(objectBase.ObjectBirthTime != DateTime.MinValue);
                Assert.True(objectBase.ObjectDeathTime != DateTime.MinValue);
                Assert.True(objectBase.ObjectBirthTime <= objectBase.ObjectDeathTime);
                Assert.True(objectBase.ObjectLiveTime == (objectBase.ObjectDeathTime - objectBase.ObjectBirthTime));
            }
        }

        [Fact]
        public async Task ObjectLifeTimeTestAsync()
        {
            await ObjectDeathTestAsync();
        }
    }
}
