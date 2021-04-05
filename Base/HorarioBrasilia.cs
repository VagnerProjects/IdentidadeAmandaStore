using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Base
{
    public class HorarioBrasilia
    {
        public static DateTime Get()
        {
            var cetZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
        }

        public static DateTime Set(DateTime data)
        {
            var cetZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(data.ToUniversalTime(), cetZone);
        }
    }
}
