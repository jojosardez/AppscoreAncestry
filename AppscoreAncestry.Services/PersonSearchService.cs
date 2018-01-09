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
                    where person.name.Contains(name?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                          CheckPersonGender(person.gender, gender)
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

            Person current =
                data.people.FirstOrDefault(
                    p => p.name.Equals(name?.Trim(), StringComparison.InvariantCultureIgnoreCase));
            var ancestryList = current != null
                ? FindAncestry(GetPeopleByIdDictionary(data.people), GetPeopleByParentIdDictionary(data.people),
                    current, gender, anchestry, 0)
                : new List<Person>();
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

        private List<Person> FindAncestry(Dictionary<int, Person> people, Dictionary<int, Person[]> descendants,
            Person current, Gender gender,
            Ancestry ancestry, int count)
        {
            List<Person> ancestryList = new List<Person>();

            switch (ancestry)
            {
                case Ancestry.Ancestors:
                    if (current.father_id.HasValue)
                    {
                        if (people.TryGetValue(current.father_id.Value, out var father))
                        {
                            if (CheckPersonGender(father.gender, gender))
                            {
                                ancestryList.Add(father);
                                count++;
                            }
                            if (count < 10)
                                ancestryList.AddRange(
                                    FindAncestry(people, descendants, father, gender, ancestry, count));
                        }
                    }
                    if (current.mother_id.HasValue)
                    {
                        if (people.TryGetValue(current.mother_id.Value, out var mother))
                        {
                            if (CheckPersonGender(mother.gender, gender))
                            {
                                ancestryList.Add(mother);
                                count++;
                            }
                            if (count < 10)
                                ancestryList.AddRange(
                                    FindAncestry(people, descendants, mother, gender, ancestry, count));
                        }
                    }
                    break;
                case Ancestry.Descendants:
                    if (descendants.TryGetValue(current.id, out var children))
                    {
                        foreach (var child in children)
                        {
                            if (CheckPersonGender(child.gender, gender))
                            {
                                ancestryList.Add(child);
                                count++;
                            }
                            if (count < 10)
                                ancestryList.AddRange(FindAncestry(people, descendants, child, gender, ancestry,
                                    count));
                        }
                    }
                    break;
            }

            return ancestryList;
        }

        private bool CheckPersonGender(char gender, Gender requestGender)
        {
            if ((requestGender & Gender.Male) == Gender.Male && gender == 'M')
                return true;
            if ((requestGender & Gender.Female) == Gender.Female && gender == 'F')
                return true;

            return false;
        }

        private Dictionary<int, Person> GetPeopleByIdDictionary(Person[] people)
        {
            return people.Select(p => p).ToDictionary(per => per.id, per => per);
        }

        private Dictionary<int, Person[]> GetPeopleByParentIdDictionary(Person[] people)
        {
            var fathers = people.Where(p => p.father_id.HasValue).GroupBy(p => p.father_id.GetValueOrDefault()).ToDictionary(per => per.Key, per => per.ToArray());
            var mothers = people.Where(p => p.mother_id.HasValue).GroupBy(p => p.mother_id.GetValueOrDefault()).ToDictionary(per => per.Key, per => per.ToArray());
            return fathers.Concat(mothers).ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
