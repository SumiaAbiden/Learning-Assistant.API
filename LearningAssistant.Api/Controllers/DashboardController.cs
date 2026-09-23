using LearningAssistant.Api.Data;
using LearningAssistant.Api.Models.DTOs;
using LearningAssistant.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningAssistant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetStats()
    {
        var stats = new DashboardStatsDto
        {
            TotalTopics = await _context.LearningTopics.CountAsync(),

            CompletedTopics = await _context.LearningTopics
                .CountAsync(t => t.Status == TopicStatus.Completed),

            InProgressTopics = await _context.LearningTopics
                .CountAsync(t => t.Status == TopicStatus.InProgress),

            NotStartedTopics = await _context.LearningTopics
                .CountAsync(t => t.Status == TopicStatus.NotStarted),

            TotalStudyMinutes = await _context.StudyJournalEntries
                .SumAsync(e => e.StudyDurationMinutes),

            TotalJournalEntries = await _context.StudyJournalEntries.CountAsync()
        };

        return Ok(stats);
    }
}
