using AutoFixture;
using Entities;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Test
{
    public class UserData
    {
        public User User { get; set; }

        private Guid CountryId;

        private Guid StateId;

        private Guid CityId;

        private string _url;

        private readonly Fixture _fixture;

        public UserData()
        {
            _fixture = new Fixture();
            _url = Parameters.BaseUrl + "Catalog";
        }

        public async Task Crear()
        {
            await ConsultarPais();

            await ConsultarDepartamento();

            await ConsultarCiudad();

            User = new User()
            {
                Name = _fixture.Create<string>().Substring(0, 10),
                Address = _fixture.Create<string>().Substring(0, 15),
                BirthDate = _fixture.Create<DateTime>(),
                DocumentType = _fixture.Create<DocumentType>(),
                DocumentNumber = _fixture.Create<string>().Substring(0, 8),
                CountryId = CountryId,
                StateId = StateId,
                CityId = CityId
            };
        }

        public void Modificar()
        {
            User.Name = _fixture.Create<string>().Substring(0, 10);
            User.Address = _fixture.Create<string>().Substring(0, 15);
            User.BirthDate = _fixture.Create<DateTime>();
            User.DocumentNumber = _fixture.Create<string>().Substring(0, 8);
        }

        private async Task ConsultarPais()
        {
            var response = (await GenericUtils.GetAsync(_url + "/ListByType?page=1&pageSize=99&type=0", ""))["resultado"].First;
            CountryId = Guid.Parse(response["id"].ToString());
        }

        private async Task ConsultarDepartamento()
        {
            var response = (await GenericUtils.GetAsync(_url + "/ListByParent?page=1&pageSize=99&parent=" + CountryId.ToString(), ""))["resultado"].First;
            StateId = Guid.Parse(response["id"].ToString());
        }

        private async Task ConsultarCiudad()
        {
            var response = (await GenericUtils.GetAsync(_url + "/ListByParent?page=1&pageSize=99&parent=" + StateId.ToString(), ""))["resultado"].First;
            CityId = Guid.Parse(response["id"].ToString());
        }

    }
}
