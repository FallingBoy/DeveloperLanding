using DeveloperLandingApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Domain.ValueObjects
{
    // Value Object для Email. Он инкапсулирует логику валидации и создания email-адреса, обеспечивая, что объект Email всегда будет содержать корректный email.
    public sealed class Email
    {

        public string Value { get; }
        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty");

            if (!email.Contains("@"))
                throw new DomainException("Invalid email");

            return new Email(email);
        }

        public override string ToString() => Value;
    }
}
