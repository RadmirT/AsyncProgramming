List<Employee> employees = Employee.GetEmployees();

ParallelOptions parallelOptions = new ParallelOptions();
parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

Func<int> localInit = () => 0;

Func<Employee, ParallelLoopState, int, int> loopBody = (employee, state, localValue) =>
{
    if (state.ShouldExitCurrentIteration == true)
        return localValue;

    Console.WriteLine($"[Поток #{Thread.CurrentThread.ManagedThreadId}] Сотрудник {employee.Name} получил зп - {employee.Salary:N}");
    return localValue + (int)employee.Salary;
};

int companyPayments = 0;
Action<int> localFinally = (localValue) =>
{
    Interlocked.Add(ref companyPayments, localValue);
};

Parallel.ForEach(employees, parallelOptions, localInit, loopBody, localFinally);
Console.WriteLine($"Компания тратит на зарплаты сотрудникам - {companyPayments:N}");

Console.ReadKey();

class Employee
{
    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public string Name { get; set; }
    public decimal Salary { get; set; }

    public static List<Employee> GetEmployees()
    {
        return new List<Employee>
            {
                new Employee("Иванов Рома", 10000),
                new Employee("Петров Рома", 10000),
                new Employee("Сидоров Рома", 10000),
                new Employee("Семенов Рома", 10000),
                new Employee("Михайлов Рома", 10000),

                new Employee("Иванов Олег", 20000),
                new Employee("Петров Олег", 20000),
                new Employee("Сидоров Олег", 20000),
                new Employee("Семенов Олег", 20000),
                new Employee("Михайлов Олег", 20000),

                new Employee("Иванов Петр", 20000),
                new Employee("Петров Петр", 20000),
                new Employee("Сидоров Петр", 20000),
                new Employee("Семенов Петр", 20000),
                new Employee("Михайлов Петр", 20000),

                new Employee("Иванов Женя", 50000),
                new Employee("Петров Женя", 50000),
                new Employee("Сидоров Женя", 50000),
                new Employee("Семенов Женя", 50000),
                new Employee("Михайлов Женя", 50000),

                new Employee("Иванов Дима", 100000),
                new Employee("Петров Дима", 100000),
                new Employee("Сидоров Дима", 100000),
                new Employee("Семенов Дима", 100000),
                new Employee("Михайлов Дима", 100000),
            };
    }
}