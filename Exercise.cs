namespace GymHelper.Models;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public string Description { get; set; }

    public List<WorkoutExercise> WorkoutExercises { get; set; } = new();
}