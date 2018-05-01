
using System;

namespace Logic
{
    public class ChecksLogic
    {
        /// <summary>
        /// Trim the strings and check for duplicates/empty values
        /// </summary>
        public bool ValuesAreValid(string teamOne, string teamTwo)
        {
            if (ValuesAreNull(teamOne) || ValuesAreNull(teamTwo))
            {
                return false;
            }

            if (String.Equals(teamOne.ToLower().Trim(), teamTwo.ToLower().Trim()))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Check for empty/null values
        /// </summary>
        private bool ValuesAreNull(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the MaxScore int to prevent a too low or null value.
        /// </summary>
        public int MaxScore(int input)
        {
            if (input < 2)
            {
                return 2;
            }

            return input;
        }
    }
}
