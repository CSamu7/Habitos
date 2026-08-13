using System.Collections;

namespace Testing.IntegrationTesting.UserTests.Data
{
    public class PatchUserRequestTestsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 3 };
        }

        IEnumerator IEnumerable.GetEnumerator()  => GetEnumerator();
    }
}
