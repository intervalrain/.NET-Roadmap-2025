namespace CoRSample.Models;

public record LeaveRequest(
    Guid EmployeeId,
    DateTime StartDay,
    DateTime EndDay,
    string? explanation = null
);