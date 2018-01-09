using AppscoreAncestry.Common.Extensions;
using AppscoreAncestry.Entities;
using AppscoreAncestry.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppscoreAncestry.Services
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly IDataStore<Data> dataStore;

        public PersonSearchService(IDataStore<Data> dataStore)
        {
            this.dataStore = dataStore;
        }

        public PersonView[] Search(string name, Gender gender, int pageNum, int pageSize = 10)
        {
            Data data = dataStore.Get();

            var query = (from person in data.people
                    join place in data.places
                    on person.place_id equals place.id
                    where person.name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase) && checkPersonGender(person.gender, gender)
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

        public PersonView[] AncestrySearch(string name, Gender gender, Ancestry anchestry)
        {
            Data data = dataStore.Get();

            Person current = data.people.FirstOrDefault(p => p.name.Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));
            var ancestryList = current != null ? FindAncestry(data.people, current, gender, anchestry) : new List<Person>();
            var query = (from person in ancestryList.ToArray()
                    join place in data.places
                    on person.place_id equals place.id
                    orderby person.level, person.id
                    select new PersonView
                    {
                        Id = person.id,
                        Name = person.name,
                        Gender = person.gender.Equals('M') ? Gender.Male : Gender.Female,
                        BirthPlace = place.name
                    })
                .Take(10);
            return query.ToArray();
        }

        private List<Person> FindAncestry(Person[] people, Person current, Gender gender, Ancestry ancestry)
        {
            List<Person> ancestryList = new List<Person>();

            switch (ancestry)
            {
                case Ancestry.Ancestors:
                    if (current.father_id.HasValue)
                    {
                        var father = people.FirstOrDefault(p => p.id == current.father_id);
                        if (father != null)
                        {
                            if (checkPersonGender(father.gender, gender))
                                ancestryList.Add(father);
                            ancestryList.AddRange(FindAncestry(people, father, gender, ancestry));
                        }
                    }
                    if (current.mother_id.HasValue)
                    {
                        var mother = people.FirstOrDefault(p => p.id == current.mother_id);
                        if (mother != null)
                        {
                            if (checkPersonGender(mother.gender, gender))
                                ancestryList.Add(mother);
                            ancestryList.AddRange(FindAncestry(people, mother, gender, ancestry));
                        }
                    }
                    break;
                case Ancestry.Descendants:
                    var children = people.Where(p => p.father_id == current.id || p.mother_id == current.id);
                    foreach (var child in children)
                    {
                        if (checkPersonGender(child.gender, gender))
                            ancestryList.Add(child);
                        ancestryList.AddRange(FindAncestry(people, child, gender, ancestry));
                    }
                    break;
            }

            return ancestryList;
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
