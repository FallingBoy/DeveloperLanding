using DeveloperLandingApi.Application.DTOs;
using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Application.Options;
using DeveloperLandingApi.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Services
{
    public class ContactService : IContactService
    {

        private readonly IEmailSender _emailSender;
        private readonly IAiAnalyzer _aiAnalyzer;
        private readonly IContactRepository _repository;
        private readonly ContactSettings _settings;

        public ContactService(
            IEmailSender emailSender,
            IAiAnalyzer aiAnalyzer,
            IContactRepository repository,
            IOptions<ContactSettings> settings)
        {
            _emailSender = emailSender; 
            _aiAnalyzer = aiAnalyzer;
            _repository = repository;
            _settings = settings.Value;
        }

        public async Task<ContactResponseDto> CreateAsync(ContactRequestDto request, CancellationToken cancellationToken = default)
        {
            // 1. Создаем Domain Entity

            var contact =
                ContactMessage.Create(
                    request.Name,
                    request.Phone,
                    request.Email,
                    request.Comment);



            // 2. AI анализ

            var aiResult = await _aiAnalyzer.AnalyzeAsync(request.Comment, cancellationToken);
            // 3. Сохраняем

            await _repository.SaveAsync(contact);
            // 4. Отправляем владельцу

            await _emailSender.SendAsync(
                _settings.OwnerEmail,
                "Новое обращение",
                $"""
            Имя:
            {contact.Name}

            Email:
            {contact.Email}

            Комментарий:
            {contact.Comment}

            AI:
            {aiResult}
            """
                );
            // 5. Ответ пользователю

            await _emailSender.SendAsync(
                request.Email,
                "Спасибо за обращение",
                "Мы получили ваше сообщение."
                );

            return new ContactResponseDto
            {
                Id = contact.Id,
                Message = "Обращение успешно отправлено"
            };

        }
    }
}
