using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public string CreateSalt()
        {
            throw new NotImplementedException();
        }

        public bool Validate(string value, string salt, string hash)
        {
            throw new NotImplementedException();
        }
    }
}
