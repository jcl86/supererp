using SuperErp.Core;

namespace SuperErp.Management.Domain
{
    public record EmailAddress
    {
        private readonly string email;

        public EmailAddress(string email)
        {
            if (email.IsEmpty())
            {
                throw new DomainException("Email of username can not be empty");
            }

            if (!IsValidEmail(email))
            {
                throw new DomainException($"{email} is not a valid email address");
            }

            this.email = email.Trim();
        }

        public override string ToString() => email;

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }

}
