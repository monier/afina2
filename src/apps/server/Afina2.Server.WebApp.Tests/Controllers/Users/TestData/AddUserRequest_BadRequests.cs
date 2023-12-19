using System.Collections;
using Afina2.Server.WebApp.Controllers.Users;

namespace Afina2.Server.WebApp.Tests.Controllers.Users.TestData
{
    internal class AddUserRequest_BadRequests : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new AddUserRequest() { Username = "" } };
            yield return new object[] { new AddUserRequest() { Username = " " } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
