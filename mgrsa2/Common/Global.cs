using mgrsa2.ViewModels;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace mgrsa2.Common
{
    public sealed class Global
    {

        // cfgConnectionString....
        //
        private Global() { }
         /// <summary>
        //-------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool ValidateDate(string strDate, string format)
        {
            try
            {
                //System.Globalization.CultureInfo nfo = new System.Globalization.CultureInfo("en-US");
                //DateTime date = DateTime.Parse(strDate, nfo);

                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = format;
                DateTime dt = DateTime.ParseExact(strDate, "d", dtfi);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool wValidateDate(object oDate)
        {
            string xdate = oDate.ToString();
            try
            {
                DateTime dt = DateTime.Parse(xdate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public static bool IsNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsNumber(c) && c.ToString() != "." && c.ToString() != "," && c.ToString() != "$")
                {
                    return false;
                }
            }
            return true;
        }

        public static string ExtractNumeric(string str)
        {
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c) || c.ToString() == "." || c.ToString() == "-")
                {
                    sValue += c.ToString();
                }
            }
            sValue = (sValue == "") ? "0" : sValue;
            return sValue;
        }

        public static string ExtractNumericIncNeg(string str)
        {
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c) || c.ToString() == ".")
                {
                    sValue += c.ToString();
                }
            }
            sValue = (sValue == "") ? "0" : sValue;
            return sValue;
        }

        public static string ExtractOnlyNumericWithQuotes(string str)
        {
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c))
                {
                    sValue += "'" + c.ToString() + "',";
                }
            }
            if (sValue.Length > 0)
            {
                sValue = sValue.Substring(0, sValue.Length - 1);
            }

            sValue = (sValue == "") ? "0" : sValue;
            return sValue;
        }
        public static string TimeToMilitary(string sTime)
        {

            int iHour = 0;
            string sMilTime = sTime;

            if (sTime == "")
            {
                sMilTime = "00:00";
            }
            else
            {
                if (sTime.Substring(5, 1).ToUpper().ToString() == "P" && sTime.Substring(0, 2).ToString() != "12")
                {
                    iHour = Convert.ToInt32(sTime.Substring(0, 2).ToString()) + 12;
                    sMilTime = iHour.ToString("0#") + sTime.Substring(2, 4).ToString();
                }
            }
            return sMilTime;
        }


        public static string GetTotalsTime(string sIn, string sOut, string sInB, string sOutB)
        {
            string sTotal = "<-- Check";
            int iHour = 0;
            int iMin = 0;

            if ((sIn == "00:00" && sOut != "00:00") || (sIn != "00:00" && sOut == "00:00"))
            {
                return sTotal;
            }
            if ((sInB == "00:00" && sOutB != "00:00") || (sInB != "00:00" && sOutB == "00:00"))
            {
                return sTotal;
            }

            iHour = Convert.ToInt32(sOut.Substring(0, 2)) - 1 - Convert.ToInt32(sIn.Substring(0, 2));
            iMin = Convert.ToInt32(sOut.Substring(3, 2)) + 60 - Convert.ToInt32(sIn.Substring(3, 2));
            if (iMin >= 60)
            {
                iHour += 1;
                iMin = iMin - 60;
            }

            iHour += Convert.ToInt32(sOutB.Substring(0, 2)) - 1 - Convert.ToInt32(sInB.Substring(0, 2));
            iMin += Convert.ToInt32(sOutB.Substring(3, 2)) + 60 - Convert.ToInt32(sInB.Substring(3, 2));
            if (iMin >= 60)
            {
                iHour += 1;
                iMin = iMin - 60;
            }

            if (iMin >= 60)
            {
                iHour += 1;
                iMin = iMin - 60;
            }

            sTotal = iHour.ToString("0#") + ":" + iMin.ToString("0#");

            return sTotal;
        }

        public static string SumTimes(string sTimeAcc, string sTimeTotal)
        {
            string sSumTimes = "00:00";
            int iHour = 0;
            int iMin = 0;
            int iScTimeAcc = 0;
            int iScTimeTotal = 0;

            if (sTimeTotal.Substring(0, 1) == "<")
            {
                sSumTimes = sTimeAcc;
                return sTimeAcc;
            }
            iScTimeAcc = sTimeAcc.IndexOf(":", 0);
            iScTimeTotal = sTimeTotal.IndexOf(":", 0);
            iHour = Convert.ToInt32(sTimeAcc.Substring(0, iScTimeAcc)) + Convert.ToInt32(sTimeTotal.Substring(0, iScTimeTotal));
            iMin = Convert.ToInt32(sTimeAcc.Substring(iScTimeAcc + 1, 2)) + Convert.ToInt32(sTimeTotal.Substring(iScTimeTotal + 1, 2));
            if (iMin >= 60)
            {
                iHour += 1;
                iMin = iMin - 60;
            }
            sSumTimes = iHour.ToString("0#") + ":" + iMin.ToString("0#");
            return sSumTimes;

        }

        public static double TimeToDec(string sTime)
        {
            double dHour = 0.00;
            int iHour = 0;
            int iSemicolon = 0;

            if (sTime.Substring(0, 1) != "<")
            {
                iSemicolon = sTime.IndexOf(":", 0);
                iHour = Convert.ToInt32(sTime.Substring(0, iSemicolon));
                dHour = iHour + Convert.ToDouble(sTime.Substring(iSemicolon + 1, 2)) / 60;
            }

            return dHour;

        }


        public static string ConvertSpDate(string sFecha)
        {

            int iDia = Convert.ToInt16(sFecha.Substring(2, 2));
            string sDia = iDia.ToString();
            int iMonth = Convert.ToInt16(sFecha.Substring(0, 2));
            string sMonth = "";
            string sYear = sFecha.Substring(4);

            switch (iMonth)
            {
                case 1:
                    sMonth = "Enero";
                    break;
                case 2:
                    sMonth = "Febrero";
                    break;
                case 3:
                    sMonth = "Marzo";
                    break;
                case 4:
                    sMonth = "Abril";
                    break;
                case 5:
                    sMonth = "Mayo";
                    break;
                case 6:
                    sMonth = "Junio";
                    break;
                case 7:
                    sMonth = "Julio";
                    break;
                case 8:
                    sMonth = "Agosto";
                    break;
                case 9:
                    sMonth = "Septiembre";
                    break;
                case 10:
                    sMonth = "Octubre";
                    break;
                case 11:
                    sMonth = "Noviembre";
                    break;
                case 12:
                    sMonth = "Diciembre";
                    break;
            }

            return (sMonth + " " + sDia + ", " + sYear);
        }


        public static bool ValidateCardNumber(string cardNumber)
        {
            try
            {
                // Array to contain individual numbers

                System.Collections.ArrayList CheckNumbers = new ArrayList();
                // So, get length of card

                int CardLength = cardNumber.Length;

                // Double the value of alternate digits, starting with the second digit

                // from the right, i.e. back to front.

                // Loop through starting at the end

                for (int i = CardLength - 2; i >= 0; i = i - 2)
                {
                    // Now read the contents at each index, this

                    // can then be stored as an array of integers


                    // Double the number returned

                    CheckNumbers.Add(Int32.Parse(cardNumber[i].ToString()) * 2);
                }

                int CheckSum = 0;    // Will hold the total sum of all checksum digits


                // Second stage, add separate digits of all products

                for (int iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
                {
                    int _count = 0;    // will hold the sum of the digits


                    // determine if current number has more than one digit

                    if ((int)CheckNumbers[iCount] > 9)
                    {
                        int _numLength = ((int)CheckNumbers[iCount]).ToString().Length;
                        // add count to each digit

                        for (int x = 0; x < _numLength; x++)
                        {
                            _count = _count + Int32.Parse(
                                  ((int)CheckNumbers[iCount]).ToString()[x].ToString());
                        }
                    }
                    else
                    {
                        // single digit, just add it by itself

                        _count = (int)CheckNumbers[iCount];
                    }
                    CheckSum = CheckSum + _count;    // add sum to the total sum

                }
                // Stage 3, add the unaffected digits

                // Add all the digits that we didn't double still starting from the

                // right but this time we'll start from the rightmost number with 

                // alternating digits

                int OriginalSum = 0;
                for (int y = CardLength - 1; y >= 0; y = y - 2)
                {
                    OriginalSum = OriginalSum + Int32.Parse(cardNumber[y].ToString());
                }

                // Perform the final calculation, if the sum Mod 10 results in 0 then

                // it's valid, otherwise its false.

                return (((OriginalSum + CheckSum) % 10) == 0);
            }
            catch
            {
                return false;
            }
        }

        public static string TransformToHtml(string sTxt)
        {
            string sTxtHtml = "";
            sTxtHtml = "<p>" + sTxt.Replace("\r\n", "</p><p>") + "</p>";
            return sTxtHtml;
        }

        public static DateTime GetFirstDayOfMonth(DateTime dtDate)
        {

            // set return value to the first day of the month
            // for any date passed in to the method
            // create a datetime variable set to the passed in date

            DateTime dtFrom = dtDate;

            // remove all of the days in the month
            // except the first day and set the
            // variable to hold that date

            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            // return the first day of the month

            return dtFrom;

        }

        public static DateTime GetFirstDayOfMonth(int iMonth)
        {

            // set return value to the last day of the month
            // for any date passed in to the method
            // create a datetime variable set to the passed in date

            DateTime dtFrom = new DateTime(DateTime.Now.Year, iMonth, 1);

            // remove all of the days in the month
            // except the first day and set the
            // variable to hold that date

            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            // return the first day of the month
            return dtFrom;

        }

        public static DateTime GetLastDayOfMonth(DateTime dtDate)
        {

            // set return value to the last day of the month
            // for any date passed in to the method
            // create a datetime variable set to the passed in date

            DateTime dtTo = dtDate;

            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the
            // previous month

            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month
            return dtTo;

        }

        public static DateTime GetLastDayOfMonth(int iMonth)
        {

            // set return value to the last day of the month
            // for any date passed in to the method
            // create a datetime variable set to the passed in date

            DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);

            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the
            // previous month

            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month

            return dtTo;

        }
        public static DateTime GetFirstDayOfCal(DateTime dtDate)
        {

            //int iDay = dtDate.Day;
            string sDay = dtDate.DayOfWeek.ToString().Substring(0, 2).ToLower();
            int iPos = 0;

            //DateTime dtLastDayMonthBefore = dtDate.AddDays(-1);
            //string sDayBefore = dtLastDayMonthBefore.DayOfWeek.ToString().Substring(0, 2).ToLower();
            //int iDayBefore = dtLastDayMonthBefore.Day;
            //string sDayBefore = dtLastDayMonthBefore.DayOfWeek.ToString().Substring(0, 2).ToLower();

            switch (sDay)
            {
                case "su":
                    iPos = 0;
                    break;
                case "mo":
                    iPos = 1;
                    break;
                case "tu":
                    iPos = 2;
                    break;
                case "we":
                    iPos = 3;
                    break;
                case "th":
                    iPos = 4;
                    break;
                case "fr":
                    iPos = 5;
                    break;
                case "sa":
                    iPos = 6;
                    break;
            }

            if (iPos == 0)
            {
                return dtDate;
            }
            else
            {
                return dtDate.AddDays(-iPos);
            }
        }

        public static int GetDayOfWeekOrdinal(DateTime dtDate)
        {

            //int iDay = dtDate.Day;
            string sDay = dtDate.DayOfWeek.ToString().Substring(0, 2).ToLower();
            int iPos = 0;

            //DateTime dtLastDayMonthBefore = dtDate.AddDays(-1);
            //string sDayBefore = dtLastDayMonthBefore.DayOfWeek.ToString().Substring(0, 2).ToLower();
            //int iDayBefore = dtLastDayMonthBefore.Day;
            //string sDayBefore = dtLastDayMonthBefore.DayOfWeek.ToString().Substring(0, 2).ToLower();

            switch (sDay)
            {
                case "su":
                    iPos = 0;
                    break;
                case "mo":
                    iPos = 1;
                    break;
                case "tu":
                    iPos = 2;
                    break;
                case "we":
                    iPos = 3;
                    break;
                case "th":
                    iPos = 4;
                    break;
                case "fr":
                    iPos = 5;
                    break;
                case "sa":
                    iPos = 6;
                    break;
            }

            return iPos;
        }


        public static string GetMonthSp(DateTime dtTime)
        {
            int iMonth = dtTime.Month;
            string sMonth = "";
            switch (iMonth)
            {
                case 1:
                    sMonth = "Enero";
                    break;
                case 2:
                    sMonth = "Febrero";
                    break;
                case 3:
                    sMonth = "Marzo";
                    break;
                case 4:
                    sMonth = "Abril";
                    break;
                case 5:
                    sMonth = "Mayo";
                    break;
                case 6:
                    sMonth = "Junio";
                    break;
                case 7:
                    sMonth = "Julio";
                    break;
                case 8:
                    sMonth = "Agosto";
                    break;
                case 9:
                    sMonth = "Septiembre";
                    break;
                case 10:
                    sMonth = "Octubre";
                    break;
                case 11:
                    sMonth = "Noviembre";
                    break;
                case 12:
                    sMonth = "Diciembre";
                    break;
            }
            return sMonth;
        }



        public static string GetDiaSp(DateTime dtDate)
        {

            string sDay = dtDate.DayOfWeek.ToString().Substring(0, 2).ToLower();
            string spDay = "";

            switch (sDay)
            {
                case "su":
                    spDay = "Domingo";
                    break;
                case "mo":
                    spDay = "Lunes";
                    break;
                case "tu":
                    spDay = "Martes";
                    break;
                case "we":
                    spDay = "Miercoles";
                    break;
                case "th":
                    spDay = "Jueves";
                    break;
                case "fr":
                    spDay = "Viernes";
                    break;
                case "sa":
                    spDay = "Sabado";
                    break;
            }


            return spDay;

        }

        public static string TransformToStringFromHtml(string sTxt)
        {
            string sTxtHtml = "";
            sTxtHtml = sTxt.Replace("<div>", "");
            sTxtHtml = sTxt.Replace("</div>", "");
            sTxtHtml = sTxt.Replace("<p>", "");
            sTxtHtml = sTxtHtml.Replace("</p>", "<br/>");
            return sTxtHtml;
        }

        public static string HtmlToText(string HTML)
        {
            return Regex.Replace(HTML, "<[^>]*>", "");
        }



        public static int CountWords(string strText, bool stripTags)
        {
            // Declare and initialize the variable holding the number of counted words
            int countedWords = 0;

            // If the stripTags argument was passed as false
            if (stripTags == false)
            {
                // Simply count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
            }
            else
            {
                // If the user wants to strip tags, first define the tag form
                Regex tagMatch = new Regex("<[^>]+>");
                // Replace the tags with an empty string so they are not considered in count
                strText = tagMatch.Replace(strText, "");
                // Count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
            }
            // Return the number of words that were counted
            return countedWords;
        }

        public static string TrunkWords(string strText, bool stripTags, int iNumWords)
        {
            // Declare and initialize the variable holding the number of counted words
            int countedWords = 0;
            string tWords = "";
            string[] split;

            // If the stripTags argument was passed as false
            if (stripTags == false)
            {
                // Simply count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
                split = strText.Split(' ');
            }
            else
            {
                // If the user wants to strip tags, first define the tag form
                Regex tagMatch = new Regex("<[^>]+>");
                // Replace the tags with an empty string so they are not considered in count
                strText = tagMatch.Replace(strText, "");
                // Count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
                split = strText.Split(' ');
            }
            // Return the number of words that were counted

            if (countedWords > iNumWords)
            {
                for (int i = 0; i < iNumWords; i++)
                {
                    tWords += split[i] + " ";
                }
            }
            else
            {
                tWords = strText;
            }

            return tWords.Trim();

        }
        public static string TrunkWordsDif(string strText, bool stripTags, int iNumWords)
        {
            // Declare and initialize the variable holding the number of counted words
            int countedWords = 0;
            string tWords = "";
            string[] split;

            // If the stripTags argument was passed as false
            if (stripTags == false)
            {
                // Simply count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
                split = strText.Split(' ');
            }
            else
            {
                // If the user wants to strip tags, first define the tag form
                Regex tagMatch = new Regex("<[^>]+>");
                // Replace the tags with an empty string so they are not considered in count
                strText = tagMatch.Replace(strText, "");
                // Count the words in the string by splitting them wherever a space is found
                countedWords = strText.Split(' ').Length;
                split = strText.Split(' ');
            }
            // Return the number of words that were counted

            if (countedWords > iNumWords)
            {
                for (int i = 0; i < countedWords; i++)
                {
                    if (i >= iNumWords)
                    {
                        tWords += split[i] + " ";
                    }
                }
            }
            else
            {
                tWords = "";
            }

            return tWords.Trim();

        }


        public static string ExtractOnlyNumeric(string str)
        {
            str += "";
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c))
                {
                    sValue += c.ToString();
                }
            }
            sValue = (sValue == "") ? "0" : sValue;
            return sValue;
        }
        public static int ExtractOnlyNumericInt(string str)
        {
            str += "";
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c))
                {
                    sValue += c.ToString();
                }
            }
            sValue = (sValue == "") ? "0" : sValue;
            return Convert.ToInt32(sValue);
        }
        public static string ExtractNumericEmpty(string str)
        {
            string sValue = "";

            foreach (char c in str)
            {
                if (Char.IsNumber(c))
                {
                    sValue += c.ToString();
                }
            }
            //sValue = (sValue == "") ? "0" : sValue;
            return sValue;
        }


        public static string GetStringDate(string dDate)
        {
            string dValue = "NULL";

            if (!string.IsNullOrEmpty(dDate))
            {
                dValue = "'" + dDate + "'";
            }

            return dValue;
        }

        public static string GetStringBool(bool bValue)
        {
            string sValue = "0";
            if (bValue)
            {
                sValue = "1";
            }
            return sValue;
        }




        //     This code license under the code project open license (CPOL)
        //     http://www.codeproject.com/info/cpol10.aspx
        // </copyright>
        //   public static bool Mod10Check(string creditCardNumber)

        public static bool ValidateCC(string creditCardNumber)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);


            //// If the final sum is divisible by 10, then the credit card number
            //   is valid. If it is not divisible by 10, the number is invalid.            
            return sumOfDigits % 10 == 0;

        }

        public static string CleanUpperCase(string sNombre)
        {
            sNombre = sNombre + "";
            sNombre = sNombre.Replace("Á", "A");
            sNombre = sNombre.Replace("É", "E");
            sNombre = sNombre.Replace("Í", "I");
            sNombre = sNombre.Replace("Ó", "O");
            sNombre = sNombre.Replace("Ú", "U");
            sNombre = sNombre.Replace("Ñ", "N");
            sNombre = sNombre.Replace("'", "");
            return sNombre;
        }

        public static int CountWords(string s)
        {
            //alert('contando');                
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }

        public static double ElapsedHours(string sFechaFrom, string sFechaTo, string sHourTo)
        {
            DateTime dFechaFrom = Convert.ToDateTime(sFechaFrom);
            DateTime dFechaTo = Convert.ToDateTime(sFechaTo);
            sFechaTo = dFechaTo.ToString("d");
            DateTime dHourTo = Convert.ToDateTime(sHourTo);
            sHourTo = dHourTo.ToString("t");
            DateTime dFechaToComp = Convert.ToDateTime(sFechaTo + " " + sHourTo);

            TimeSpan tsp = dFechaFrom - dFechaToComp;

            return tsp.TotalHours;
        }

        public static string SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(to));
            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }

            // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            }       // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;
            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            string sResult = "";
            try
            {

                //send the message 
                SmtpClient smtp = new SmtpClient("smtp.mailanyone.net");
                NetworkCredential Credentials = new NetworkCredential("infoagentes@elaviso.com", "revista1");

                smtp.Credentials = Credentials;
                smtp.Send(mMailMessage);

                sResult = "OK";

                return sResult;
            }
            catch (Exception exc)
            {
                sResult = "Send failure: " + exc.ToString();
                return sResult;
            }
        }
        public static string jFormatDate(string jdate)
        {
            string[] adate = jdate.Split(',');

            string sFormatDate = "";

            try
            {
                sFormatDate = adate[0] + "/" + adate[1] + "/" + adate[2];
            }
            catch (Exception ex)
            {
                sFormatDate = jdate;
            }
            return sFormatDate;

        }

        public static string jFormatTime(string jdate)
        {
            string[] adate = jdate.Split(',');
            string shora = adate[3];
            string smin = adate[4];

            int ihora = Convert.ToInt16(Global.ExtractNumeric(shora));
            string sFormatDate = "";

            if (ihora > 12)
            {
                sFormatDate = (ihora - 12).ToString().PadLeft(2,'0') + ":" + smin.PadLeft(2, '0') + " PM";
            }
            else
            {
                sFormatDate = ihora.ToString().PadLeft(2, '0') + ":" + smin.PadLeft(2, '0') + " AM";
            }
           
             return sFormatDate;

        }

        public static string FormatMomentDate(string sdate, string shr, string smm)
        {
            string sformat = sdate.Substring(5, 2) + "/" +
                sdate.Substring(8, 2) + "/" +
                sdate.Substring(0, 4) + " " + shr + ":" + smm;

            return sformat;

        }

        //public static DateSpanish GetDateSpanish(string dDate)
        //{
        //    DateSpanish dateSp = new DateSpanish();

        //    DateTime dValue = Convert.ToDateTime(dDate);

        //    string sDay = String.Format("{0:MM/dd/yy}", dValue);
        //    string sHora = String.Format("{0:t}", dValue);
        //    string sDaySpanish = "";

        //    if (dValue.DayOfWeek == System.DayOfWeek.Monday)
        //    {
        //        sDaySpanish = "LUNES";
        //    }
        //    else if (dValue.DayOfWeek == System.DayOfWeek.Tuesday)
        //    {
        //        sDaySpanish = "MARTES";
        //    }
        //    else if (dValue.DayOfWeek == System.DayOfWeek.Wednesday)
        //    {
        //        sDaySpanish = "MIERCOLES";
        //    }
        //    else if (dValue.DayOfWeek == System.DayOfWeek.Thursday)
        //    {
        //        sDaySpanish = "JUEVES";
        //    }

        //    else if (dValue.DayOfWeek == System.DayOfWeek.Friday)
        //    {
        //        sDaySpanish = "VIERNES";
        //    }

        //    else if (dValue.DayOfWeek == System.DayOfWeek.Saturday)
        //    {
        //        sDaySpanish = "SABADO";
        //    }
        //    else if (dValue.DayOfWeek == System.DayOfWeek.Sunday)
        //    {
        //        sDaySpanish = "DOMINGO";
        //    }

        //    dateSp.dia = sDaySpanish;
        //    dateSp.fecha = sDay;
        //    dateSp.hora = sHora;

        //    return dateSp;
        //}


        public static string FixString(string value)
        {
            value += "";
            value = value.Replace("'", "''");
            return value;
        }



    }
}