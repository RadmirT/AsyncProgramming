namespace Contracts;

public class Movie
{
    public Movie() { }

    public Movie(string description, decimal price)
    {
        Description = description;
        Price = price;
    }

    public string Description { get; set; }
    public decimal Price { get; set; }
}
