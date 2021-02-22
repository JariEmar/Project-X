using Api.Cache;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Unit.Tests.Cache
{
    [TestFixture]
    public class CachedAttributeTest
    {
        private CachedAttribute cachedAttribute;

        [SetUp]
        public void SetUp()
        {
            cachedAttribute = new CachedAttribute(1);
        }

        [Test]
        public void FirstTest()
        {

        }
    }
}
