namespace BusinessInsightsAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using BusinessInsightsAPI.Exceptions;
    using log4net;

    /// <summary>
    /// FizzBuzz class
    /// </summary>
    public static class FizzBuzz
    {
        #region Private Attributes

        /// <summary>
        /// Last number of the series
        /// </summary>
        private static int limit = 0;

        /// <summary>
        /// Number 3 read from the web.config
        /// </summary>
        private static int fizz = 0;

        /// <summary>
        /// Number 5 read from the web.config
        /// </summary>
        private static int buzz = 0;

        /// <summary>
        /// Logger
        /// </summary>
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
         
        #region Public Methods
        /// <summary>
        /// This method receives a number and do FizzBuzz series to the limit number
        /// If the number is divisible by 3, replace it for "Fizz"
        /// If the number is divisible by 5, replace it for "Buzz"
        /// If the number is divisible by both, replace it for "FizzBuzz"
        /// </summary>
        /// <param name="startNumber">Start number</param>
        /// <returns>List of a strings</returns>
        public static List<string> DoFizzBuzzSeries(int startNumber)
        {
            log.InfoFormat("Method DoFizzBuzzSeries started, startNumber = {0}", startNumber);

            List<string> numberSeries = new List<string>();
            bool isFizz;
            bool isBuzz;

            try
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["limit"], out limit);
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["fizz"], out fizz);
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["buzz"], out buzz);

                ValidateStartNumber(startNumber);

                for (int i = startNumber; i <= limit; i++)
                {
                    string value = i.ToString();
                    isFizz = i % fizz == 0;
                    isBuzz = i % buzz == 0;

                    value = isFizz ? "Fizz" : value;
                    value = isBuzz ? "Buzz" : value;
                    value = isFizz && isBuzz ? "FizzBuzz" : value;

                    numberSeries.Add(value);
                }

                WriteSeriesInFile(numberSeries);
            }
            catch (InvalidNumberException inEx)
            {
              numberSeries.Add(inEx.Message);
              log.ErrorFormat("Method DoFizzBuzzSeries InvalidNumberException = {0}", inEx.ToString());
            }
            catch (Exception ex)
            {
              numberSeries.Add(ex.Message);
              log.ErrorFormat("Method DoFizzBuzzSeries Exception = {0}", ex.ToString());
            }

            log.InfoFormat("Method DoFizzBuzzSeries finished, size of series = {0}", numberSeries.Count);

            return numberSeries;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate if the number is correct
        /// </summary>
        /// <param name="numberToValidate">Start Number</param>
        private static void ValidateStartNumber(int numberToValidate)
        {
            log.InfoFormat("Method ValidateStartNumber started, numberToValidate = {0}", numberToValidate);

            if (numberToValidate < 1 || numberToValidate >= limit)
            {
                throw new InvalidNumberException("The number must be greater than 0 or lower than " + limit);
            }

            log.InfoFormat("Method ValidateStartNumber finished, {0} is valid", numberToValidate);
        }

        /// <summary>
        /// Save the FizzBuzz series into a file
        /// </summary>
        /// <param name="series">FizzBuzz series</param>
        private static void WriteSeriesInFile(List<string> series)
        {
            log.InfoFormat("Method WriteSeriesInFile started");

            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + System.Configuration.ConfigurationManager.AppSettings["fileName"];
            string fileText = Environment.NewLine + "START OF FIZZBUZZ SERIE" + Environment.NewLine + string.Join(",", series) + Environment.NewLine + "END OF FIZZBUZZ SERIE";
            File.AppendAllText(path, fileText);

            log.InfoFormat("Method WriteSeriesInFile finished, text = {0}, path = {1}", fileText, path);
        }
        #endregion
    }
}