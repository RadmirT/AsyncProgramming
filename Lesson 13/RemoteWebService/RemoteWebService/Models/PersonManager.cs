using Contracts;

namespace RemoteWebService.Models
{
    public class PersonManager
    {
        public List<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person(1, "Николай", "Иванов", 35, 10000),
                new Person(2, "Александр", "Сидоров", 24, 85000),
                new Person(3, "Владислав", "Петров", 29, 19000),
                new Person(4, "Дмитрий", "Иванов", 31, 20000),
                new Person(5, "Сергей", "Петров", 44, 11642),
            };
        }
    }
}