using Bogus;
using SuperErp.Core.FunctionalTests;
using SuperErp.Management.Model;

namespace SuperErp.Management.FunctionalTests
{
    public class UserMother
    {
        public static RegisterUser Register(string password = null)
        {
            var faker = new Faker<RegisterUser>()
               .StrictMode(true)
               .RuleFor(x => x.Email, f => f.Person.Email)
               .RuleFor(x => x.DelegationId, f => f.Random.Number(1, 10))
               .RuleFor(x => x.StartDate, f => f.Date.Past().Date)
               .RuleFor(x => x.Password, f => password.IsEmpty() ? PasswordMother.Valid() : password);

            return faker.Generate();
        }
    }
}
