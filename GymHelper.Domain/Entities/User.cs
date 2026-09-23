namespace GymHelper.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }

    public List<Workout> Workouts { get; set; } = new();
}