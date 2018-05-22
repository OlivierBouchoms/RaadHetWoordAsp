namespace Logic
{
    public class ValidationLogic
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

            return !string.Equals(teamOne.ToLower().Trim(), teamTwo.ToLower().Trim());
        }
        
        /// <summary>
        /// Check for empty/null values
        /// </summary>
        private bool ValuesAreNull(string team)
        {
            return string.IsNullOrWhiteSpace(team);
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
