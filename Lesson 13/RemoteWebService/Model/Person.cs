namespace Contracts;

public class Person
{
    public Person()
    {

    }

    public Person(int id, string name, string surname, int age, decimal salary)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Age = age;
        Salary = salary;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
}
