using System.Security.Cryptography;
using GuessWho.Application.Core.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GuessWho.Infrastructure;

public class StringHasher : IDisposable, IStringHasher
{
     private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
     private const int IterationCount = 10000;
     private const int NumberOfBytesRequested = 256 / 8;
     private const int SaltSize = 128 / 8;
     private readonly RandomNumberGenerator _rng;

     public StringHasher() => _rng = new RNGCryptoServiceProvider();

     public string Hash(string password)
     {
         if (password is null)
         {
             throw new ArgumentNullException(nameof(password));
         }

         return Convert.ToBase64String(HashPasswordInternal(password));
     }

     public bool HashesMatch(string passwordHash, string providedPassword)
     {
         if (passwordHash is null)
         {
             throw new ArgumentNullException(nameof(passwordHash));
         }

         if (providedPassword is null)
         {
             throw new ArgumentNullException(nameof(providedPassword));
         }

         var decodedHashedPassword = Convert.FromBase64String(passwordHash);

         if (decodedHashedPassword.Length == 0)
         {
             return false;
         }

         var verified = VerifyPasswordHashInternal(decodedHashedPassword, providedPassword);

         return verified;
     }   

     private byte[] HashPasswordInternal(string password)
     {
         var salt = GetRandomSalt();

         var subKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, NumberOfBytesRequested);

         var outputBytes = new byte[salt.Length + subKey.Length];

         Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);

         Buffer.BlockCopy(subKey, 0, outputBytes, salt.Length, subKey.Length);

         return outputBytes;
     }   

     private byte[] GetRandomSalt()
     {
         var salt = new byte[SaltSize];

         _rng.GetBytes(salt);

         return salt;
     }   

     private static bool VerifyPasswordHashInternal(byte[] hashedPassword, string password)
     {
         try
         {
             var salt = new byte[SaltSize];

             Buffer.BlockCopy(hashedPassword, 0, salt, 0, salt.Length);

             var subKeyLength = hashedPassword.Length - salt.Length;

             if (subKeyLength < SaltSize)
             {
                 return false;
             }

             var expectedSubKey = new byte[subKeyLength];

             Buffer.BlockCopy(hashedPassword, salt.Length, expectedSubKey, 0, expectedSubKey.Length);

             var actualSubKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, subKeyLength);

             return ByteArraysEqual(actualSubKey, expectedSubKey);
         }
         catch
         {
             return false;
         }
     }   

     private static bool ByteArraysEqual(byte[] a, byte[] b)
     {
         if (a == null && b == null)
         {
             return true;
         }

         if (a == null || b == null || a.Length != b.Length)
         {
             return false;
         }

         var areSame = true;

         for (var i = 0; i < a.Length; i++)
         {
             areSame &= a[i] == b[i];
         }

         return areSame;
     }

     public void Dispose()
     {
         _rng.Dispose();
     }   
}