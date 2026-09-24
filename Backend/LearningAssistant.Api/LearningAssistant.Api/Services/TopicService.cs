using LearningAssistant.Api.Data;
using LearningAssistant.Api.Models.DTOs.Topics;
using LearningAssistant.Api.Models.Entities;
using LearningAssistant.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearningAssistant.Api.Services;

public class TopicService : ITopicService
{
    private readonly AppDbContext _context;

    public TopicService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TopicDto>> GetAllAsync()
    {
        var topics = await _context.LearningTopics
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return topics.Select(MapToDto).ToList();
    }

    public async Task<TopicDto?> GetByIdAsync(int id)
    {
        var topic = await _context.LearningTopics.FindAsync(id);
        return topic == null ? null : MapToDto(topic);
    }

    public async Task<TopicDto> CreateAsync(CreateTopicDto dto)
    {
        var topic = new LearningTopic
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Priority = dto.Priority,
            TargetDate = dto.TargetDate,
            Status = TopicStatus.NotStarted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.LearningTopics.Add(topic);
        await _context.SaveChangesAsync();

        return MapToDto(topic);
    }

    public async Task<TopicDto?> UpdateAsync(int id, UpdateTopicDto dto)
    {
        var topic = await _context.LearningTopics.FindAsync(id);
        if (topic == null) return null;

        topic.Title = dto.Title;
        topic.Description = dto.Description;
        topic.Category = dto.Category;
        topic.Status = dto.Status;
        topic.Priority = dto.Priority;
        topic.TargetDate = dto.TargetDate;
        topic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(topic);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var topic = await _context.LearningTopics.FindAsync(id);
        if (topic == null) return false;

        _context.LearningTopics.Remove(topic);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TopicDto?> CompleteAsync(int id)
    {
        var topic = await _context.LearningTopics.FindAsync(id);
        if (topic == null) return null;

        topic.Status = TopicStatus.Completed;
        topic.CompletedAt = DateTime.UtcNow;
        topic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(topic);
    }

    private static TopicDto MapToDto(LearningTopic topic)
    {
        return new TopicDto
        {
            Id = topic.Id,
            Title = topic.Title,
            Description = topic.Description,
            Category = topic.Category,
            Status = topic.Status,
            Priority = topic.Priority,
            TargetDate = topic.TargetDate,
            CompletedAt = topic.CompletedAt,
            CreatedAt = topic.CreatedAt,
            UpdatedAt = topic.UpdatedAt
        };
    }
}
