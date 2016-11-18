﻿using System;
using System.Linq;

namespace obj_tc.Extensions
{
    public static class StringExtensions
    {
        private static readonly Random random = new Random((int) DateTime.Now.Ticks);

        public static string GenerateMaxNumericString(int max)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, max).Select(s => s[random.Next(chars.Length)]).ToArray());
        }

        public static string GenerateMaxAlphanumericString(int max)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, max).Select(s => s[random.Next(chars.Length)]).ToArray());
        }
    }
}