using Microsoft.Extensions.Logging;
using NumToWordsCurrency.Interfaces;
using NumToWordsCurrency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumToWordsCurrency.Services
{
    public class NumToWordsConverter : INumToWords
    {
        private static readonly string[] Ones = {"", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN",
                                             "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"};
        private static readonly string[] Tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        private static readonly Dictionary<long, string> DPLessThanThousand = new Dictionary<long, string>();

        private static readonly string[] Groups = { "", "THOUSAND", "MILLION" };
        private readonly ILogger<NumToWordsConverter> _logger;

        public NumToWordsConverter(ILogger<NumToWordsConverter> logger)
        {
            _logger = logger;
        }
        private void IsValidInput (string input)
        {
            const decimal dollarLimit = 999999999.99m;
            const int centsLimit = 99; 

            if (decimal.TryParse(input, out decimal amount))
            {
                if (amount< 0)
                {
                        throw new FormatException("Input cannot be negative");
                }
                else if (amount > dollarLimit)
                {
                    throw new FormatException(string.Format("Input exceed maximum allowed value {0}", dollarLimit));
                }
                var dollarCents = input.Split('.'); 
                try
                {
                    var cents = dollarCents[1];
                    if (int.Parse(cents) > centsLimit)
                    {
                        throw new FormatException("Input is not in the correct format");
                    }
                } catch (IndexOutOfRangeException) // . is not specified (assume 0)
                {
                    return;
                }
            } 
            else
            {
                throw new FormatException("Input is not a valid number");
            }
        }

        private string ConvertLessThanThousand(long input)
        {
            if (DPLessThanThousand.ContainsKey(input))
            {
                return DPLessThanThousand[input]; //store results of recursion
            }
            string result;
            if (input == 0)
            {
                result = "";
            }
            else if (input <20)
            {
                result = Ones[input];
            } 
            else if (input <100)
            {
                result = Tens[input / 10] + "-" + Ones[input % 10];
            }
            else
            {
                if (input % 100 > 0) //has remainder
                {
                    result = Ones[input / 100] + " HUNDRED AND " + ConvertLessThanThousand(input % 100);
                }
                else
                {
                    result = Ones[input / 100] + " HUNDRED";
                }
            }
            DPLessThanThousand[input] = result;
            return result;
        }
        private string ConvertToString(long inputDec)
        {
            try
            {
                if (inputDec == 0)
                {
                    return "ZERO";
                }
                string result = "";
                int groupIndex = 0;
                while (inputDec > 0 && groupIndex < Groups.Length) //group index from ones to millions
                {
                    long currentGroup = (long)(inputDec % 1000);
                    if (currentGroup != 0)
                    {
                        string currentGroupWords = ConvertLessThanThousand(currentGroup);
                        result = currentGroupWords + " " + Groups[groupIndex] + " " + result;
                    }

                    inputDec /= 1000;
                    groupIndex++;
                }
                return result.Trim();
            }
            catch(Exception e)
            {
                _logger.LogError(e, string.Format("An error occurred: {0}", e.Message));
                throw new Exception("An unknown error occured");
            }
        }
        public Form ConvertNumToWords(Form form)
        {
            string input = form.Input;
            IsValidInput(input);

            string[] dollarsCents = input.Split('.');
            int cents;
            if (dollarsCents.Length > 1)
            {
                cents = int.Parse(dollarsCents[1]);
            }
            else
            {
                cents = 0;
            }
            var dollars = long.Parse(dollarsCents[0]);
            var dollarsWord = ConvertToString(dollars);
            var centsWord = cents != 0 ? ConvertLessThanThousand(cents) : "ZERO";
            string result = $"{dollarsWord} DOLLARS AND {centsWord} CENTS";
            form.Output = result;
            return form;
        }
    }
}
