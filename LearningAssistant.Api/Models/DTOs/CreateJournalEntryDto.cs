namespace LearningAssistant.Api.Models.DTOs.Journal;

public class CreateJournalEntryDto
{
    public int? TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int StudyDurationMinutes { get; set; }
    public DateTime StudiedAt { get; set; }
}
