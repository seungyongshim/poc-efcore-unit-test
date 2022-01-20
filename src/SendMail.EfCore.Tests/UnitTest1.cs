using System;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SendMail.EfCore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var i = new ServicePermissionData(new[]
            {
                RoleType.Admin
            });

            var s = JsonSerializer.Serialize(i);
        }
    }
}
