using LearningAssistant.Api.Data;
using LearningAssistant.Api.Models.DTOs.Journal;
using LearningAssistant.Api.Models.Entities;
using LearningAssistant.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearningAssistant.Api.Services;

public class JournalService : IJournalService
{
    private readonly AppDbContext _context;

    public JournalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<JournalEntryDto>> GetAllAsync()
    {
        var entries = await _context.StudyJournalEntries
            .Include(e => e.Topic)
            .OrderByDescending(e => e.StudiedAt)
            .ToListAsync();

        return entries.Select(MapToDto).ToList();
    }

    public async Task<List<JournalEntryDto>> GetByTopicIdAsync(int topicId)
    {
        var entries = await _context.StudyJournalEntries
            .Include(e => e.Topic)
            .Where(e => e.TopicId == topicId)
            .OrderByDescending(e => e.StudiedAt)
            .ToListAsync();

        return entries.Select(MapToDto).ToList();
    }

    public async Task<JournalEntryDto?> GetByIdAsync(int id)
    {
        var entry = await _context.StudyJournalEntries
            .Include(e => e.Topic)
            .FirstOrDefaultAsync(e => e.Id == id);

        return entry == null ? null : MapToDto(entry);
    }

    public async Task<JournalEntryDto> CreateAsync(CreateJournalEntryDto dto)
    {
        var entry = new StudyJournalEntry
        {
            TopicId = dto.TopicId,
            Title = dto.Title,
            Content = dto.Content,
            StudyDurationMinutes = dto.StudyDurationMinutes,
            StudiedAt = dto.StudiedAt,
            CreatedAt = DateTime.UtcNow
        };

        _context.StudyJournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        // Topic adını da dönmek için tekrar çek
        return await GetByIdAsync(entry.Id) ?? MapToDto(entry);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entry = await _context.StudyJournalEntries.FindAsync(id);
        if (entry == null) return false;

        _context.StudyJournalEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
    }

    private static JournalEntryDto MapToDto(StudyJournalEntry entry)
    {
        return new JournalEntryDto
        {
            Id = entry.Id,
            TopicId = entry.TopicId,
            TopicTitle = entry.Topic?.Title,
            Title = entry.Title,
            Content = entry.Content,
            StudyDurationMinutes = entry.StudyDurationMinutes,
            StudiedAt = entry.StudiedAt,
            CreatedAt = entry.CreatedAt
        };
    }
}
