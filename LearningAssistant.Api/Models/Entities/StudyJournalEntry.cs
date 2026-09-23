namespace LearningAssistant.Api.Models.Entities;

public class StudyJournalEntry
{
    public int Id { get; set; }
    public int? TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int StudyDurationMinutes { get; set; }
    public DateTime StudiedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property - EF Core ilişkiyi buradan anlar
    public LearningTopic? Topic { get; set; }
}
