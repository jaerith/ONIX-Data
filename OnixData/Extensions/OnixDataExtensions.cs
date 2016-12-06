using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnixData.Legacy;
using OnixData.Version3;

namespace OnixData.Extensions
{
    public static class OnixDataExtensions
    {
        #region CONSTANTS

        public const int CONST_ISBN_LEN = 10;
        public const int CONST_EAN_LEN  = 13;

        #endregion

        public static bool HasValidEAN(this OnixLegacyProduct TargetProduct)
        {
            bool IsValid = false;
            string EAN = TargetProduct.EAN;

            if (!String.IsNullOrEmpty(EAN))
                IsValid = EAN.IsValidEAN();

            return IsValid;
        }

        public static bool IsValidEAN(this string TargetEAN)
        {
            bool IsValid = false;

            int[] EanWeights = { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1 };

            int sum, diff;
            int ZeroCharVal = (int)'0';
            long value;

            String TempEAN = TargetEAN;

            // Initialization
            sum = 0;
            value = 0;

            if (String.IsNullOrEmpty(TargetEAN))
                return false;

            if (TargetEAN.Length > CONST_EAN_LEN)
                return false;

            try
            {
                long nEAN = Convert.ToInt64(TargetEAN);
            }
            catch (Exception ex)
            { return false; }

            if (TargetEAN.Length < CONST_EAN_LEN)
                TempEAN = TargetEAN.PadLeft(CONST_EAN_LEN, '0');

            char[] EanCharArray = TempEAN.ToCharArray();
            for (int i = 0; i < EanCharArray.Length; ++i)
            {
                int TempCharVal = (int)EanCharArray[i];

                diff = (TempCharVal - ZeroCharVal);

                sum += (diff) * EanWeights[i];

                value = value * 10 + (diff);
            }

            IsValid = (sum % 10 == 0);

            return IsValid;
        }
    }
}
