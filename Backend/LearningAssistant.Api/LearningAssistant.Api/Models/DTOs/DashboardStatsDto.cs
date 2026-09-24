namespace LearningAssistant.Api.Models.DTOs;

public class DashboardStatsDto
{
    public int TotalTopics { get; set; }
    public int CompletedTopics { get; set; }
    public int InProgressTopics { get; set; }
    public int NotStartedTopics { get; set; }
    public int TotalStudyMinutes { get; set; }
    public int TotalJournalEntries { get; set; }
}
