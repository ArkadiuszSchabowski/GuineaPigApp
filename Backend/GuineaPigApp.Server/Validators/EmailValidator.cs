﻿using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using System.Text.RegularExpressions;

namespace GuineaPigApp.Server.Validators
{
    public class EmailValidator : IEmailValidator
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public void ValidateEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BadRequestException("Adres e-mail nie może być pusty.");
            }

            if (!EmailRegex.IsMatch(email))
            {
                throw new BadRequestException("Podany adres e-mail jest niepoprawny.");
            }
        }
    }
}
