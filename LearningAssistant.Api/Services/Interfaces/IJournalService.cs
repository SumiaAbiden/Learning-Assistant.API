using LearningAssistant.Api.Models.DTOs.Journal;

namespace LearningAssistant.Api.Services.Interfaces;

public interface IJournalService
{
    Task<List<JournalEntryDto>> GetAllAsync();
    Task<List<JournalEntryDto>> GetByTopicIdAsync(int topicId);
    Task<JournalEntryDto?> GetByIdAsync(int id);
    Task<JournalEntryDto> CreateAsync(CreateJournalEntryDto dto);
    Task<bool> DeleteAsync(int id);
}
