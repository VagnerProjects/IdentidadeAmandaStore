﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace IdentidadeAmandaStore.Security.Hash
{
    public class SHA256Algorithm
    {
        public static string Algorithm(string data)
        {
            using (var sha256Hash = SHA256.Create())
            {           
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
             
                var  builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
