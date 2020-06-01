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
}
