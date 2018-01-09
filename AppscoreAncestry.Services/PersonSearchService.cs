using System;
using AppscoreAncestry.Common.Extensions;
using AppscoreAncestry.Entities;
using AppscoreAncestry.Infrastructure;
using System.Linq;

namespace AppscoreAncestry.Services
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly IDataStore<Place[]> placesDataStore;
        private readonly IDataStore<Person[]> peopleDataStore;

        public PersonSearchService(IDataStore<Place[]> placesDataStore, IDataStore<Person[]> peopleDataStore)
        {
            this.placesDataStore = placesDataStore;
            this.peopleDataStore = peopleDataStore;
        }

        public PersonView[] Search(string name, Gender gender, int pageNum, int pageSize = 10)
        {
            return SearchFromDataStore(name, gender, pageNum, pageSize);
        }

        private PersonView[] SearchFromDataStore(string name, Gender gender, int pageNum, int pageSize)
        {
            Place[] places = placesDataStore.Get();
            Person[] people = peopleDataStore.Get();

            var query = (from person in people
                    join place in places
                    on person.place_id equals place.id
                    where person.name.Contains(name, StringComparison.OrdinalIgnoreCase) && checkPersonGender(person.gender, gender)
                         select new PersonView
                    {
                        Id = person.id,
                        Name = person.name,
                        Gender = person.gender.Equals('M') ? Gender.Male : Gender.Female,
                        BirthPlace = place.name
                    })
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize);

            return query.ToArray();
        }

        private bool checkPersonGender(char gender, Gender requestGender)
        {
            if ((requestGender & Gender.Male) == Gender.Male && gender == 'M')
                return true;
            if ((requestGender & Gender.Female) == Gender.Female && gender == 'F')
                return true;

            return false;
        }
    }
}
