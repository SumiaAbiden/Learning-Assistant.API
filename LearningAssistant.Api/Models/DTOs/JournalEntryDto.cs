namespace LearningAssistant.Api.Models.DTOs.Journal;

public class JournalEntryDto
{
    public int Id { get; set; }
    public int? TopicId { get; set; }
    public string? TopicTitle { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int StudyDurationMinutes { get; set; }
    public DateTime StudiedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
