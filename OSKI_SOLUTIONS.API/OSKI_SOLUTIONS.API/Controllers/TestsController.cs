using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OSKI_SOLUTIONS.API.Controllers.Attributes;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Interfaces;
using OSKI_SOLUTIONS.Domain.Services.Implementation;
using OSKI_SOLUTIONS.Helpers.DtoModels;
using OSKI_SOLUTIONS.Helpers.DtoModels.Tests;
using OSKI_SOLUTIONS.Helpers.DtoModels.Tests.Sessions;

namespace OSKI_SOLUTIONS.API.Controllers
{
  [Route("api/[controller]/[action]")]
  public class TestsController : ControllerBase
  {
    private readonly TestsService _testsService;
    private readonly SessionsOfTestsService _sessionService;
    private readonly IGenericRepository<User> _usersGR;
    private readonly IMapper _mapper;

    public TestsController(TestsService testsService,
                          SessionsOfTestsService sessionService,
                          IGenericRepository<User> usersGR,
                          IMapper mapper)
    {
      _testsService = testsService;
      _sessionService = sessionService;
      _usersGR = usersGR;
      _mapper = mapper;
    }

    private List<KeyValuePair<int, string>?> AvaibleSessions(User user)
    {
      return _usersGR.GetDbSet()
              .Where(u => u.Id == user.Id)
              .Include(u => u.Sessions)
              .ThenInclude(s => s.Test)
              .FirstOrDefault()
                .Sessions
                .Where(s => !s.EndDateTime.HasValue && s.StartDateTime.AddMinutes(s.Test.TestLengthInMinuts) >= DateTime.Now)
                .Select(s => new KeyValuePair<int, string>(s.TestId, s.Id))
                .Cast<KeyValuePair<int, string>?>().ToList();
    }

    [HttpGet]
    [MyAutorize]
    public async Task<ResultDto> GetAll([BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<IEnumerable<TestDto>>(
              await _testsService.GetAllTestsAsync(),
              opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
             )
            );
    }

    [HttpGet]
    [MyAutorize]
    public async Task<ResultDto> GetById([FromQuery]int id, [BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<TestDto>(
          await _testsService.GetByIdAsync(id),
          opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
      ));
    }

    [HttpGet]
    [MyAutorize]
    public async Task<ResultDto> Start([FromQuery] int id, [BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<SessionDto>(
        await _sessionService.StartNewSession(user, id),
        opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
       )
      );
    }

    [HttpGet]
    [MyAutorize]
    public async Task<ResultDto> getSessionById([FromQuery] string id, [BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<SessionDto>(
        await _sessionService.getById(user, id),
        opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
       )
      );
    }

    [HttpPost]
    [MyAutorize]
    public async Task<ResultDto> SetQuestionOption([FromBody]SetQuestionOptionDto dto, [BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<OptionOfQuestionInSessionDto>(
        await _sessionService.SetQuestionOption(user, dto.OptionId, dto.Selection),
        opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
       )
      );
    }

    [HttpGet]
    [MyAutorize]
    public async Task<ResultDto> EndSessionById([FromQuery]string id, [BindNever] User user)
    {
      return ResultDto.Create(_mapper.Map<SessionDto>(
        await _sessionService.EndSessionById(user, id),
        opt => opt.Items["avaibleSessions"] = AvaibleSessions(user)
       )
      );
    }
  }
}
