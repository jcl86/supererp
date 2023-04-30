using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SuperErp.Management.Domain
{
    public class ClaimConverterShould
    {
        private readonly User user;

        private static IdentityUserClaim<string> Claim(Claim claim)
        {
            return new IdentityUserClaim<string>()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }

        public ClaimConverterShould()
        {
            user = new User()
            {
                Claims = new List<IdentityUserClaim<string>>()
                {
                    Claim("France".ToClaim("Country")),
                    Claim(new DateTime(2022, 10, 30).ToClaim("StartDate")),
                    Claim(14.ToClaim("DelegationId")),
                    Claim(true.ToClaim("IsVip")),
                    Claim(false.ToClaim("IsConfidential")),
                    Claim(32500.60m.ToClaim("Salary")),
                    Claim(Status.Deleted.ToClaim("CustomStatus")),
                }
            };
        }


        [Fact]
        public void Convert_string()
        {
            user.GetClaim("Country").Should().Be("France");
        }

        [Fact]
        public void Convert_datetime()
        {
            user.GetClaim<DateTime>("StartDate").Should().Be(new DateTime(2022, 10, 30));
        }

        [Fact]
        public void Convert_int()
        {
            user.GetClaim<int>("DelegationId").Should().Be(14);
        }

        [Fact]
        public void Convert_true()
        {
            user.GetClaim<bool>("IsVip").Should().BeTrue();
        }

        [Fact]
        public void Convert_false()
        {
            user.GetClaim<bool>("IsConfidential").Should().BeFalse();
        }

        [Fact]
        public void Convert_decimal()
        {
            user.GetClaim<decimal>("Salary").Should().Be(32500.60m);
        }

        [Fact]
        public void Convert_enum()
        {
            user.GetClaim<Status>("Salary").Should().Be(Status.Deleted);
        }

        private enum Status
        {
            Active = 1,
            Inactive,
            Deleted
        }
    }

   
}