using System.Threading.Tasks;
using NUnit.Framework;

namespace EGS.Application.UnitTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
