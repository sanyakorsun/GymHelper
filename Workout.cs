namespace GymHelper.Models;

public class Workout
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }

    public User User { get; set; }
    public List<WorkoutExercise> WorkoutExercises { get; set; } = new();
}