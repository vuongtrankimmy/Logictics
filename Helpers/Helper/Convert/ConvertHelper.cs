using Entities.OwnerModels.Functions.Staffs.Staff;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers.Helper.Convert
{
    public static class ConvertHelper
    {
        public static string Now()=> DateTime.Now.ToString("o");
        public static string Serialize(object obj)=>JsonConvert.SerializeObject(obj);        
        public static T Deserialize<T>(string str)=> JsonConvert.DeserializeObject<T>(str);        
        public static string ToLower(this string str)=> string.Join("", str.Split(' ')).Trim().ToLower();
        public static string ToCapitalize(this string str)=> CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str.ToLower());
        public static string DecimalToCurrency(this decimal money, string format = "#,###")=> money.ToString(format) ?? "0";
        public static string IntToCurrency(this int value, string format = "#,###")=> value.ToString(format) ?? "0";
        public static string ToCode(this string value, int numberFormat, string prefix = "", string hyphen = "")
        {
            if (value != null && value != "")
            {
                var maxCharacter = value.Length > numberFormat ? numberFormat + (value.Length - numberFormat) : numberFormat;
                var valueFormat=value.PadLeft(maxCharacter, '0');
                return string.Concat(prefix, hyphen, valueFormat);
            }
            return default;
        }
        public static string ToAcronymn(this string str)
        {
            var name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str.ToLower());
            return string.Join(string.Empty, Regex.Match(name, "(?:([A-Z]+)(?:[^A-Z]*))*").Groups[1].Captures.Cast<Capture>().Select(p => p.Value));
        }
        public static PasswordResult PasswordEncrypt(string password)
        {
            //https://github.com/trichards57/zxcvbn-cs
            var generate = Zxcvbn.Core.EvaluatePassword(password);
            double calc = generate.CrackTime.OfflineFastHashing1e10PerSecond / double.MaxValue * 100.000d;
            var result = new PasswordResult
            {
                Password = calc.ToString(CultureInfo.InvariantCulture),
                Calc = calc,
                Score = generate.Score,
            };
            return result;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a time to the time in a particular time zone.
        /// </summary>
        /// <param name="dateTimeOffset">The date and time to convert.</param>
        /// <param name="destinationTimeZone">The time zone to convert  to.</param>
        /// <returns>The date and time in the destination time zone.</returns>
        public static DateTimeOffset ConvertTime(this DateTimeOffset dateTimeOffset)
        {
            //2022.01.22
            //https://csharp-extension.com/en/method/1002275/datetimeoffset-converttime
            //https://dotnetfiddle.net/zcc9CF
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTime(dateTimeOffset, tst);
        }       
    }
}
