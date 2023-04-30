using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperErp.Management.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int? DelegationId { get; set; }
        public DateTime? StartDate { get; set; }

    }
}
