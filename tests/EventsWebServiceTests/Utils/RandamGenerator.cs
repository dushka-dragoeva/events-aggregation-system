namespace EventsWebServiceTests.Utils
{
    internal static class RandamGenerator
    {
        private static Random _random = new Random();

        internal static int GenerateInt(int minValue = 100, int maxValue = 9999)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
