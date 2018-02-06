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

        public static readonly int[] CONST_EAN_WEIGHTS = { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1 };

        #endregion

        /// <summary>
        /// 
        /// Calculates the checksum for a provided EAN (or the main segment of an EAN)
        /// 
        /// </summary>
        public static int CalculateEANChecksum(this string TargetEAN, StringBuilder EANWithChecksum)
        {
            int    nCheckSum;
            int    nTemp;
            string sMainFragmentEAN;

            // Initialization
            nTemp            = 0;
            nCheckSum        = -1;
            sMainFragmentEAN = TargetEAN;

            EANWithChecksum.Clear();

            if (TargetEAN.Length == CONST_EAN_LEN)
            {
                if (!IsValidEAN(TargetEAN))
                    return nCheckSum;

                sMainFragmentEAN = TargetEAN.Substring(0, CONST_EAN_LEN - 1);
            }
            else if (TargetEAN.Length == (CONST_EAN_LEN - 1))
                sMainFragmentEAN = TargetEAN;
            else
                return -1;

            nCheckSum = 0;
            for (uint i = 0; i < sMainFragmentEAN.Length; i++)
            {
                char cTempVal = sMainFragmentEAN.ToCharArray()[i];

                nTemp = Convert.ToInt32(new String(new char[] { cTempVal }));

                if (nTemp > 0)
                {
                    if ((i % 2) == 0)
                        nCheckSum += nTemp;
                    else
                        nCheckSum += (3 * nTemp);
                }
            }

            nCheckSum = (CONST_ISBN_LEN - (nCheckSum % 10)) % 10;

            EANWithChecksum.Clear().Append(sMainFragmentEAN).Append(nCheckSum);

            return nCheckSum;
        }

        public static string ConvertEANToISBN(this string TargetEAN)
        {
            int     nCheckSum;
            int     nSumOfProduct;
            int     nTemp;
            string  sEAN;
            string  sISBN;

            // Initialization
            nTemp         = 0;
            nCheckSum     = 0;
            nSumOfProduct = 0;
            sEAN          = TargetEAN;

            if (TargetEAN.Length < CONST_EAN_LEN)
                sEAN = TargetEAN.PadLeft(CONST_EAN_LEN, '0');

            if (!IsValidEAN(sEAN))
                return "";

            for (int i = 3, j = 10; i < (CONST_EAN_LEN - 1); i++, j--)
            {
                char cTempVal = TargetEAN.ToCharArray()[i];

                nTemp = Convert.ToInt32(new String(new char[] { cTempVal }));

                nSumOfProduct += (j * nTemp);
            }

            sISBN = sEAN.Substring(3, 9);

            nCheckSum = 11 - (nSumOfProduct % 11);

            if (nCheckSum == 10)
                sISBN += "X";
            else if (nCheckSum == 11)
                sISBN += "0";
            else
                sISBN += Convert.ToString(nCheckSum);

            return sISBN;
        }

        public static string ConvertISBNToEAN(this string TargetISBN, string EANPrefix = "978")
        {
            int    nCheckSum;
            int    nTemp;
            string EAN;
            string ISBN;

            // Initialization
            nTemp     = 0;
            nCheckSum = 0;
            EAN       = "";

            if (TargetISBN.Length < CONST_ISBN_LEN)
                ISBN = TargetISBN.PadLeft(CONST_ISBN_LEN, '0');
            else
                ISBN = TargetISBN;

            if (!ISBN.IsISBNValid())
                return "";

            if (EANPrefix.Length != 3)
                return "";

            EAN = EANPrefix + ISBN.Substring(0, (CONST_ISBN_LEN - 1));

            nCheckSum = 0;
            for (uint i = 0; i < EAN.Length; i++)
            {
                char cTempVal = EAN.ToCharArray()[i];

                // Do not convert the number directly from a char, since we do not want the ASCII value
                // nTemp = Convert.ToInt32(cTempVal);

                nTemp = Convert.ToInt32(new String(new char[] { cTempVal }));

                if (nTemp > 0)
                {
                    if ((i % 2) == 0)
                        nCheckSum += nTemp;
                    else
                        nCheckSum += (3 * nTemp);
                }
            }

            nCheckSum = (CONST_ISBN_LEN - (nCheckSum % 10)) % 10;

            EAN += Convert.ToString(nCheckSum);

            return EAN;
        }

        public static bool HasValidEAN(this OnixLegacyProduct TargetProduct)
        {
            bool   IsValid = false;
            string EAN     = TargetProduct.EAN;

            if (!String.IsNullOrEmpty(EAN))
                IsValid = EAN.IsValidEAN();

            return IsValid;
        }

        public static bool IsValidEAN(this string TargetEAN)
        {
            bool IsValid = false;

            int  sum, diff;
            int  ZeroCharVal = (int)'0';
            long value;

            String TempEAN = TargetEAN;

            // Initialization
            sum   = 0;
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

                sum += (diff) * CONST_EAN_WEIGHTS[i];

                value = value * 10 + (diff);
            }

            IsValid = (sum % 10 == 0);

            return IsValid;
        }

        public static bool HasValidISBN(this OnixLegacyProduct TargetProduct)
        {
            bool IsValid = false;

            string ISBN = TargetProduct.ISBN;
            if (!String.IsNullOrEmpty(ISBN))
                IsValid = ISBN.IsISBNValid();

            return IsValid;
        }

        public static bool IsISBNValid(this string TargetISBN)
        {
            bool    IsValid;
            int     TempVal;
            int     Counter;
            int     CheckSum;
            char    cCurrentCheckDigit;

            // Initialization
            IsValid             = true;
            cCurrentCheckDigit  = 'x';
            CheckSum            = Counter = 0;
            TempVal             = 0;

            if (TargetISBN.Length != CONST_ISBN_LEN)
                return false;

            string ISBNPrefix = TargetISBN.Substring(0, (CONST_ISBN_LEN - 1));

            try
            {
                long nISBNPrefix = Convert.ToInt64(ISBNPrefix);
            }
            catch (Exception ex) { return false; }

            try
            {
                cCurrentCheckDigit = TargetISBN.ToCharArray()[CONST_ISBN_LEN - 1];

                if( cCurrentCheckDigit == 'x' )
                    cCurrentCheckDigit = 'X';

                CheckSum = Counter = 0;
                foreach (char TempDigit in ISBNPrefix)
                {
                    int TempDigitVal = Convert.ToInt32(new String(new char[] { TempDigit }));

                    if (TempDigitVal > 0)
                        CheckSum += (CONST_ISBN_LEN - Counter) * TempDigitVal;

                    Counter++;
                }

                CheckSum = 11 - (CheckSum % 11);

                // sprintf(acTemp, "%c", cCurrentCheckDigit);

                if (cCurrentCheckDigit == 'X')
                    TempVal = 0;
                else
                    TempVal = Convert.ToInt32(new String(new char[] { cCurrentCheckDigit } ));

                if( ((CheckSum == 10) && (cCurrentCheckDigit == 'X')) ||
                    ((CheckSum == 11) && (cCurrentCheckDigit == '0')) ||
                    (CheckSum == TempVal))
                {
                    IsValid = true;
                }
                else
                    IsValid = false;
            }
            catch(Exception ex)
            {
                IsValid = false;
            }

            return IsValid;
        }
    }
}
