namespace EventsWebServiceTests.Utils
{
    internal static class RandamGenerator
    {
        private static Random _random = new Random();

        internal static int GenerateInt(int minValue = 100, int maxValue = 9999)
        {
            return _random.Next(minValue, maxValue);
        }

        public static string GenerateString(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
