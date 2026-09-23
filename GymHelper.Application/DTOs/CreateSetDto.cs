namespace GymHelper.Application.DTOs;

public class CreateSetDto
{
    public Guid WorkoutExerciseId { get; set; }
    public double Weight { get; set; }
    public int Reps { get; set; }
}