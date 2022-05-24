﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.encrypt.crc32
{
    /// <summary>
    /// Implements a 32-bit CRC hash algorithm compatible with Zip etc.
    /// </summary>
    /// <remarks>
    /// Crc32 should only be used for backward compatibility with older file formats
    /// and algorithms. It is not secure enough for new applications.
    /// If you need to call multiple times for the same data either use the HashAlgorithm
    /// interface or remember that the result of one Compute call needs to be ~ (XOR) before
    /// being passed in as the seed for the next Compute call.
    /// </remarks>
    public sealed class Crc32 : HashAlgorithm
    {
        public const uint DefaultPolynomial = 0xedb88320u;
        public const uint DefaultSeed = 0xffffffffu;

        static uint[] defaultTable;

        readonly uint seed;
        readonly uint[] table;
        uint hash;

        public Crc32()
            : this(DefaultPolynomial, DefaultSeed)
        {
        }

        public Crc32(uint polynomial, uint seed)
        {
            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        public override void Initialize()
        {
            hash = seed;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hash = CalculateHash(table, hash, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt32ToBigEndianBytes(~hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize { get { return 32; } }

        public static uint Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        public static uint Compute(uint seed, byte[] buffer)
        {
            return Compute(DefaultPolynomial, seed, buffer);
        }

        public static uint Compute(uint polynomial, uint seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        static uint[] InitializeTable(uint polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            var createTable = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (uint)i;
                for (var j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = entry >> 1 ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
        {
            var crc = seed;
            for (var i = start; i < size - start; i++)
                crc = crc >> 8 ^ table[buffer[i] ^ crc & 0xff];
            return crc;
        }

        static byte[] UInt32ToBigEndianBytes(uint uint32)
        {
            var result = BitConverter.GetBytes(uint32);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }
    }

    public class Encrypt
    {
        //protected readonly string encKeySource = "*aB//8K[KuoYB,q\\O!vBvwYeB0IL\\c!";
        protected readonly string encKeySource = "*skill.bank01@gmail.com$";
        private string MethodFull(string textPlain)
        {
            var result = "";
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] baseHash = sha.ComputeHash(Encoding.ASCII.GetBytes(textPlain));
            byte[] array = new byte[baseHash.Length];
            for (int i = 0; i < baseHash.Length; i++)
            {
                array[i] = (byte)((uint)baseHash[i] ^ 170);
            }
            uint num = Crc32.Compute(Encoding.ASCII.GetBytes(textPlain));
            byte[] bytes = Encoding.ASCII.GetBytes(encKeySource);
            byte[] numArray = new byte[4]
            {
                (byte) (num &  byte.MaxValue),
                (byte) (num >> 8 &  byte.MaxValue),
                (byte) (num >> 16 &  byte.MaxValue),
                (byte) (num >> 24 &  byte.MaxValue)
            };
            int length = numArray.Length;
            for (int index = 0; index < bytes.Length; ++index)
                bytes[index] = (byte)(bytes[index] ^ (uint)numArray[index % length]);
            return result;
        }
        public static string BitConvert(string textPlain)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] baseHash = sha.ComputeHash(Encoding.ASCII.GetBytes(textPlain));
            byte[] array = new byte[baseHash.Length];
            for (int i = 0; i < baseHash.Length; i++)
            {
                array[i] = (byte)((uint)baseHash[i] ^ 170);
            }
            var result = BitConverter.ToString(array).Replace("-", "");
            return result;
        }
        public static string CRC32(string textPlain)
        {
            uint num = Crc32.Compute(Encoding.ASCII.GetBytes(textPlain));
            _ = new byte[4]
            {
                (byte) (num &  byte.MaxValue),
                (byte) (num >> 8 &  byte.MaxValue),
                (byte) (num >> 16 &  byte.MaxValue),
                (byte) (num >> 24 &  byte.MaxValue)
            };
            var result = num.ToString("x");
            return result;
        }
        public string Key(string textPlain)
        {
            uint num = Crc32.Compute(Encoding.ASCII.GetBytes(textPlain));
            byte[] bytes = Encoding.ASCII.GetBytes(encKeySource);
            byte[] numArray = new byte[4]
            {
                (byte) (num &  byte.MaxValue),
                (byte) (num >> 8 &  byte.MaxValue),
                (byte) (num >> 16 &  byte.MaxValue),
                (byte) (num >> 24 &  byte.MaxValue)
            };
            int length = numArray.Length;
            for (int index = 0; index < bytes.Length; ++index)
                bytes[index] = (byte)(bytes[index] ^ (uint)numArray[index % length]);
            var result = BitConverter.ToString(bytes).Replace("-", "");
            return result;
        }
    }
}
