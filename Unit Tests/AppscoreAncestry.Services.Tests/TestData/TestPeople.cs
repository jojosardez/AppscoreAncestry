using AppscoreAncestry.Entities;

namespace AppscoreAncestry.Services.Tests.TestData
{
    public static class TestPeople
    {
        public static Person[] GetPeople()
        {
            return new []
            {
                new Person
                {
                    id = 100,
                    name = "Darron Jobs",
                    gender = 'M',
                    father_id = null,
                    mother_id = null,
                    place_id = 100,
                    level = 0
                },
                new Person
                {
                    id = 101,
                    name = "Nellie Price",
                    gender = 'F',
                    father_id = null,
                    mother_id = null,
                    place_id = 101,
                    level = 0
                },
                new Person
                {
                    id = 200,
                    name = "Steve Jobs",
                    gender = 'M',
                    father_id = 100,
                    mother_id = 101,
                    place_id = 102,
                    level = 1
                },
                new Person
                {
                    id = 201,
                    name = "Laurene Powell",
                    gender = 'F',
                    father_id = null,
                    mother_id = null,
                    place_id = 103,
                    level = 0
                },
                new Person
                {
                    id = 210,
                    name = "Olivia Jobs",
                    gender = 'F',
                    father_id = 100,
                    mother_id = 101,
                    place_id = 104,
                    level = 1
                },
                new Person
                {
                    id = 211,
                    name = "James Tahir",
                    gender = 'M',
                    father_id = null,
                    mother_id = null,
                    place_id = 105,
                    level = 0
                },
                new Person
                {
                    id = 300,
                    name = "Reed Jobs",
                    gender = 'M',
                    father_id = 200,
                    mother_id = 201,
                    place_id = 106,
                    level = 2
                },
                new Person
                {
                    id = 301,
                    name = "Erin Jobs",
                    gender = 'F',
                    father_id = 200,
                    mother_id = 201,
                    place_id = 107,
                    level = 2
                },
                new Person
                {
                    id = 302,
                    name = "Eve Jobs",
                    gender = 'F',
                    father_id = 200,
                    mother_id = 201,
                    place_id = 108,
                    level = 2
                },
                new Person
                {
                    id = 303,
                    name = "Lisa Jobs",
                    gender = 'F',
                    father_id = 200,
                    mother_id = null,
                    place_id = 109,
                    level = 2
                },
                new Person
                {
                    id = 310,
                    name = "Blanca Tahir",
                    gender = 'F',
                    father_id = 211,
                    mother_id = 210,
                    place_id = 100,
                    level = 2
                },
                new Person
                {
                    id = 311,
                    name = "Jenny Tahir",
                    gender = 'F',
                    father_id = 211,
                    mother_id = 210,
                    place_id = 101,
                    level = 2
                },
                new Person
                {
                    id = 312,
                    name = "Amy Tahir",
                    gender = 'F',
                    father_id = 211,
                    mother_id = 210,
                    place_id = 102,
                    level = 2
                },
                new Person
                {
                    id = 313,
                    name = "Jerry Tyson",
                    gender = 'M',
                    father_id = null,
                    mother_id = null,
                    place_id = 103,
                    level = 2
                },
                new Person
                {
                    id = 410,
                    name = "Isabel Tyson",
                    gender = 'F',
                    father_id = 313,
                    mother_id = 312,
                    place_id = 104,
                    level = 3
                }
            };
        }
    }
}
