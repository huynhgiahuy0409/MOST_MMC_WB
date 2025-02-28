using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tsb.Fontos.Core.Validator.Type
{
	public class StringChecker
	{
		/// <summary>
		/// Gets a value indicating whether container number validation is succeeded.
		/// </summary>
		/// <returns>true if container number validation is succeeded; otherwise, false.</returns>
		public static bool IsValidCntrNo(string cntrNo)
		{
			bool returnValue = false;
			try
			{
				int result;
				int chkDigit;
				int wgtDigit = 0;
				int[] chkNum = new int[10];

				if (cntrNo == null || cntrNo == string.Empty)
				{
					return true;
				}

				if (cntrNo == null || cntrNo.Length != 11)
				{
					return false;
				}

				if (Int32.TryParse(cntrNo.Substring(cntrNo.Length - 1, 1), out result) == false)
				{
					return false;
				}
				else
				{
					chkDigit = Convert.ToInt32(cntrNo.Substring(cntrNo.Length - 1, 1));
				}

				for (int i = 0; i < 10; i++)
				{
					int ascii = Convert.ToInt32(cntrNo[i]);

					if (i < 4)
					{
						if (ascii == 65)
						{
							chkNum[i] = 10;
						}
						else if (ascii > 65 && ascii < 76)
						{
							chkNum[i] = ascii - 54;
						}
						else if (ascii > 75 && ascii < 86)
						{
							chkNum[i] = ascii - 53;
						}
						else if (ascii > 85 && ascii < 91)
						{
							chkNum[i] = ascii - 52;
						}
						else
						{
							return false;
						}
					}
					else
					{
						if (ascii > 47 && ascii < 58)
						{
							int number;
							if (int.TryParse(cntrNo[i].ToString(), out number))
							{
								chkNum[i] = number;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					}

					wgtDigit = wgtDigit + Convert.ToInt32(chkNum[i] * Math.Pow(2, i));
				}

				wgtDigit = wgtDigit % 11;

				if (wgtDigit == 10)
				{
					wgtDigit = 0;
				}

				if (chkDigit == wgtDigit)
				{
					returnValue = true;
				}
				else
				{
					returnValue = false;
				}
			}
			catch (Exception)
			{
				returnValue = false;
			}
			return returnValue;
		}

        /// <summary>
        /// Gets a value indicating whether wagon number validation is succeeded.
        /// </summary>
        /// <returns>true if wagon number validation is succeeded; otherwise, false.</returns>
        public static bool IsValidWagonNo(string wagonNo)
        {
            bool returnValue = false;
            string regexWagonNo;
            double checkSum = 0;
            int checkResult = 0;
            int checkDigit = 12;

            try
            {
                if (string.IsNullOrEmpty(wagonNo) == true) return true;

                regexWagonNo = Regex.Replace(wagonNo, @"[^0-9]", "", RegexOptions.Singleline);

                if (string.IsNullOrEmpty(regexWagonNo) == true) return false;
                if (regexWagonNo.Length != checkDigit) return false;

                for (int i = 0; i < checkDigit - 1; i++)
                {
                    checkResult = Convert.ToInt32(regexWagonNo[i] - 48) * (i % 2 == 0 ? 2 : 1);

                    if (checkResult >= 10)
                        checkSum += ((checkResult / 10) + (checkResult % 10));
                    else
                        checkSum += checkResult;
                }

                if ((regexWagonNo[checkDigit - 1] - 48) == (int)Math.Ceiling(checkSum / 10) * 10 - checkSum)
                    returnValue = true;
                else
                    returnValue = false;
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }
	}
}
