using DeveloperLandingApi.Domain.Common;
using DeveloperLandingApi.Domain.Exceptions;
using DeveloperLandingApi.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Domain.Entities
{
    public class ContactMessage : Entity
    {

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public Email Email { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private ContactMessage(
            Guid id,
            string name,
            string phone,
            Email email,
            string comment)
            : base(id)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }

        public static ContactMessage Create(
            string name,
            string phone,
            string email,
            string comment)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name required");


            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Phone required");


            if (string.IsNullOrWhiteSpace(comment))
                throw new DomainException("Comment required");


            if (comment.Length > 2000)
                throw new DomainException("Comment too long");

            return new ContactMessage(
                Guid.NewGuid(),
                name,
                phone,
                Email.Create(email),
                comment);
        }
    }
}
