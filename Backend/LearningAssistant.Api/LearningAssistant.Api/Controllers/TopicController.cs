using LearningAssistant.Api.Models.DTOs.Topics;
using LearningAssistant.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningAssistant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _topicService;

    public TopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TopicDto>>> GetAll()
    {
        var topics = await _topicService.GetAllAsync();
        return Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicDto>> GetById(int id)
    {
        var topic = await _topicService.GetByIdAsync(id);
        return topic == null ? NotFound() : Ok(topic);
    }

    [HttpPost]
    public async Task<ActionResult<TopicDto>> Create(CreateTopicDto dto)
    {
        var created = await _topicService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TopicDto>> Update(int id, UpdateTopicDto dto)
    {
        var updated = await _topicService.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _topicService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<TopicDto>> Complete(int id)
    {
        var topic = await _topicService.CompleteAsync(id);
        return topic == null ? NotFound() : Ok(topic);
    }
}
