namespace GymHelper.Models;

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public Guid ExerciseId { get; set; }

    public Workout Workout { get; set; }
    public Exercise Exercise { get; set; }

    public List<Set> Sets { get; set; } = new();
}