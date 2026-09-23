using LearningAssistant.Api.Models.Entities;

namespace LearningAssistant.Api.Models.DTOs.Topics;

public class TopicDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public TopicStatus Status { get; set; }
    public TopicPriority Priority { get; set; }
    public DateTime? TargetDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
