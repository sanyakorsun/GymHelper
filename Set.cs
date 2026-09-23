namespace GymHelper.Models;

public class Set
{
    public Guid Id { get; set; }
    public Guid WorkoutExerciseId { get; set; }
    public double Weight { get; set; }
    public int Repetitions { get; set; }

    public WorkoutExercise WorkoutExercise { get; set; }
}