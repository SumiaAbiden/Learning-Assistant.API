using LearningAssistant.Api.Models.DTOs.Topics;

namespace LearningAssistant.Api.Services.Interfaces;

public interface ITopicService
{
    Task<List<TopicDto>> GetAllAsync();
    Task<TopicDto?> GetByIdAsync(int id);
    Task<TopicDto> CreateAsync(CreateTopicDto dto);
    Task<TopicDto?> UpdateAsync(int id, UpdateTopicDto dto);
    Task<bool> DeleteAsync(int id);
    Task<TopicDto?> CompleteAsync(int id);
}
