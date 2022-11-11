using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public class UserPasswordHasher
    {
        private const int SaltByteSize = 32;
        private const int HashByteSize = 32;
        private const int HasingIterationsCount = 10101;
        private readonly ILogger<UserPasswordHasher> logger;

        public UserPasswordHasher(ILogger<UserPasswordHasher> logger)
        {
            this.logger = logger;
        }
        public string HashPassword(string password)
        {
            try
            {
                byte[] salt;
                byte[] buffer2;
                if (password == null)
                {
                    logger.LogError("Password is empty.");
                }
                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
                {
                    salt = bytes.Salt;
                    buffer2 = bytes.GetBytes(HashByteSize);
                }
                byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
                Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
                Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
                return Convert.ToBase64String(dst);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "HashPassword error.");
                return ex.Message;
            }
        }
        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            try
            {
                byte[] _passwordHashBytes;
                int _arrayLen = (SaltByteSize + HashByteSize) + 1;

                if (hashedPassword == null)
                {
                    return false;
                }

                if (password == null)
                {
                    logger.LogError("Password is empty.");
                    return false;
                }

                byte[] src = Convert.FromBase64String(hashedPassword);

                if ((src.Length != _arrayLen) || (src[0] != 0))
                {
                    return false;
                }

                byte[] _currentSaltBytes = new byte[SaltByteSize];
                Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

                byte[] _currentHashBytes = new byte[HashByteSize];
                Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCount))
                {
                    _passwordHashBytes = bytes.GetBytes(SaltByteSize);
                }

                return AreHashesEqual(_currentHashBytes, _passwordHashBytes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "VerifyHashedPassword error.");
                return false;
            }

        }
        private bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            try
            {
                int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
                var xor = firstHash.Length ^ secondHash.Length;
                for (int i = 0; i < _minHashLength; i++)
                    xor |= firstHash[i] ^ secondHash[i];
                return 0 == xor;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "VerifyHashedPassword error.");
                return false;
            }
        }
    }
}
