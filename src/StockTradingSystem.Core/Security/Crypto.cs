using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace StockTradingSystem.Core.Security
{
    public static class Crypto
    {
        private const int Iterations = 8192;
        private const int SaltSize = 32;
        private const int SubkeyLength = 128;

        /// <summary>Returns an RFC 2898 hash value for the specified password.</summary>
        /// <returns>The hash value for <paramref name="password" /> as a base-64-encoded string.</returns>
        /// <param name="password">The password to generate a hash value for.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="password" /> is null.</exception>
        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            byte[] salt;
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(SubkeyLength);
            }
            var inArray = new byte[SaltSize+SubkeyLength+1];
            Buffer.BlockCopy(salt, 0, inArray, 1, SaltSize);
            Buffer.BlockCopy(bytes, 0, inArray, SaltSize+1, SubkeyLength);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>Determines whether the specified RFC 2898 hash and password are a cryptographic match.</summary>
        /// <returns>true if the hash value is a cryptographic match for the password; otherwise, false.</returns>
        /// <param name="hashedPassword">The previously-computed RFC 2898 hash value as a base-64-encoded string.</param>
        /// <param name="password">The plaintext password to cryptographically compare with <paramref name="hashedPassword" />.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="hashedPassword" /> or <paramref name="password" /> is null.</exception>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException(nameof(hashedPassword));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            var numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length != SaltSize+SubkeyLength+1 || numArray[0] != 0)
                return false;
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(numArray, 1, salt, 0, SaltSize);
            var a = new byte[SubkeyLength];
            Buffer.BlockCopy(numArray, SaltSize+1, a, 0, SubkeyLength);
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
                bytes = rfc2898DeriveBytes.GetBytes(SubkeyLength);
            return ByteArraysEqual(a, bytes);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null || b == null || a.Count != b.Count)
                return false;
            var flag = true;
            for (var index = 0; index < a.Count; ++index)
                flag &= a[index] == b[index];
            return flag;
        }
    }
}