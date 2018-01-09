using AppscoreAncestry.Entities;

namespace AppscoreAncestry.Services.Tests.TestData
{
    public static class TestPlaces
    {
        public static Place[] GetPlaces()
        {
            return new []
            {
                new Place
                {
                    id = 100,
                    name = "Sydney"
                },
                new Place
                {
                    id = 101,
                    name = "Melbourne"
                },
                new Place
                {
                    id = 102,
                    name = "Perth"
                },
                new Place
                {
                    id = 103,
                    name = "Adelaide"
                },
                new Place
                {
                    id = 104,
                    name = "Brisbane"
                },
                new Place
                {
                    id = 105,
                    name = "Darwin"
                },
                new Place
                {
                    id = 106,
                    name = "Hobart"
                },
                new Place
                {
                    id = 107,
                    name = "Canberra"
                },
                new Place
                {
                    id = 108,
                    name = "Cairns"
                },
                new Place
                {
                    id = 109,
                    name = "Alice Springs"
                }
            };
        }
    }
}
