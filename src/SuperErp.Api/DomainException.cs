﻿namespace SuperErp.Core
{
    public class DomainException : Exception
    {
        public DomainException(params string[] errors) : base(string.Join(", ", errors)) { }
    }
}
