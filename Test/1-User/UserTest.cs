using Resources;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    [TestCaseOrderer("User", "User_Tests")]
    public class UserTest: IClassFixture<UserData>
    {
        private string _url;

        private readonly UserData _data;

        public UserTest(UserData data)
        {
            _data = data;
            _url = Parameters.BaseUrl + "User";
        }

        [Fact, TestPriority(1)]
        public async Task Crear()
        {
            await _data.Crear();
            var response = await GenericUtils.PostAsync(_url, _data.User, "");
            Assert.Equal(response.StatusCode.ToString(), "OK");
        }

        //[Fact, TestPriority(2)]
        //public async Task Listar()
        //{
        //    var response = await GenericUtils.GetAsync(_url+"/List?page=1&pageSize=10", "");
        //    Assert.NotEmpty(response);
        //}
    }
}
