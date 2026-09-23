namespace GymHelper.Application.DTOs;

public class CreateWorkoutDto
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}