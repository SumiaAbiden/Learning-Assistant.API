namespace LearningAssistant.Api.Models.Entities;

public class LearningTopic
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Category { get; set; } = string.Empty;

    public TopicStatus Status { get; set; } = TopicStatus.NotStarted;

    public TopicPriority Priority { get; set; } = TopicPriority.Medium;

    public DateTime? TargetDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public enum TopicStatus
{
    NotStarted,
    InProgress,
    Completed,
    OnHold
}

public enum TopicPriority
{
    Low,
    Medium,
    High
}