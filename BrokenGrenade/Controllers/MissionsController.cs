using BrokenGrenade.Data;
using BrokenGrenade.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrokenGrenade.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private readonly MissionService _missionService;

    public MissionsController(MissionService missionService)
    {
        _missionService = missionService;
    }
    
    [HttpGet]
    public async Task<List<MissionApiModel>> GetAll([FromQuery]DateTime? start, [FromQuery]DateTime? end)
    {
        var missions = await _missionService.GetAllAsync(start, end);
        var result = missions.Select(MissionApiModel.FromEntity).ToList();
        return result;
    }
    
    [HttpGet("upcoming")]
    public async Task<List<MissionApiModel>> GetUpcoming([FromQuery]DateTime? end)
    {
        var missions = await _missionService.GetUpcomingAsync(end);
        return missions.Select(MissionApiModel.FromEntity).ToList();
    }
}