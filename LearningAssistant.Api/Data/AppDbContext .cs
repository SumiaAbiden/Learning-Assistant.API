using LearningAssistant.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningAssistant.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<LearningTopic> LearningTopics { get; set; }

    public DbSet<StudyJournalEntry> StudyJournalEntries { get; set; }

}
