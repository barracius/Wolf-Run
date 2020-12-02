using UnityEngine;

namespace Helpers
{
    public static class Methods
    {
        public static int CalculateTotalStars()
        {
            var counter = 0;
            for (int i = 1; i <= (int) Helpers.GobalVariables.NumberOfStages; i++)
            {
                var stars = PlayerPrefs.GetInt("level" + i + "Stars", 0);
                counter += stars;
            }

            return counter;
        }
    }
}