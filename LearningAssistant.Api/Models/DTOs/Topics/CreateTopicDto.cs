using LearningAssistant.Api.Models.Entities;

namespace LearningAssistant.Api.Models.DTOs.Topics;

public class CreateTopicDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public TopicPriority Priority { get; set; } = TopicPriority.Medium;
    public DateTime? TargetDate { get; set; }
}
