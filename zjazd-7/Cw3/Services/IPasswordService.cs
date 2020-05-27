﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IPasswordService
    {
        public string Create(string value, string salt);

        public bool Validate(string value, string salt, string hash);

        public string CreateSalt();
    }
    public class PBKDFPasswordService : IPasswordService
    {
        public string Create(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,
                salt: Encoding.UTF8.GetBytes(salt),
                iterationCount: 10000,
                prf: KeyDerivationPrf.HMACSHA512,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public bool Validate(string value, string salt, string hash)
        {
            return Create(value, salt) == hash;
        }
    }
}
