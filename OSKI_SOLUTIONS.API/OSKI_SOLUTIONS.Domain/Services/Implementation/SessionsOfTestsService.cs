using Microsoft.EntityFrameworkCore;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session;
using OSKI_SOLUTIONS.DataAccess.Interfaces;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSKI_SOLUTIONS.Domain.Services.Implementation
{
  public class SessionsOfTestsService
  {
    private readonly IGenericRepository<Test> _testsGR;
    private readonly IGenericRepository<SessionOfTest> _sessionsGR;
    private readonly IGenericRepository<QuestionOfSession> _questionsOfSessionsGR;
    private readonly IGenericRepository<OptionOfQuestionInSession> _optionsOfQuestionsInSessionsGR;

    public SessionsOfTestsService(IGenericRepository<SessionOfTest> sessionsGR,
                                  IGenericRepository<QuestionOfSession> questionsOfSessionsGR,
                                  IGenericRepository<OptionOfQuestionInSession> optionsOfQuestionsInSessionsGR,
                                  IGenericRepository<Test> testsGR,
                                  TestsService testsService,
                                  ClientErrorManager clientErrorManager)
    {
      _sessionsGR = sessionsGR;
      _questionsOfSessionsGR = questionsOfSessionsGR;
      _optionsOfQuestionsInSessionsGR = optionsOfQuestionsInSessionsGR;
      _testsGR = testsGR;

      if (!clientErrorManager.IsIssetErrors("SessionsOfTests"))
        clientErrorManager.AddErrors(new ClientErrors("SessionsOfTests",
          new Dictionary<string, ClientError>
          {
              {"inc-ses-id", new ClientError("Incorrect session id!")},
              {"inc-opt", new ClientError("Incorrect option!")},
              {"ses-is-already-ended", new ClientError("Session is already ended!")}
          })); 
    }

    public async Task<SessionOfTest> StartNewSession(User user, int testId)
    {
      var test = _testsGR.GetDbSet()
                  .Include(t => t.Questions)
                  .ThenInclude(q => q.Options)
                  .FirstOrDefault(x => x.Id == testId);
      if (test == null)
        throw new ClientException("inc-test-id");

      SessionOfTest session = await _sessionsGR.GetDbSet()
        .Include(s => s.Test)
        .Include(s => s.Questions)
        .ThenInclude(q => q.BaseQuestion)
        .Include(s => s.Questions)
        .ThenInclude(q => q.Options)
        .ThenInclude(o => o.BaseOption)
        .FirstOrDefaultAsync(session => session.UserId == user.Id && session.TestId == testId && session.StartDateTime.AddMinutes(session.Test.TestLengthInMinuts) > DateTime.Now);
      if (session != null)
        return session;
      else
        return await _sessionsGR.CreateAsync(new SessionOfTest(user, test));
    }

    public async Task<SessionOfTest> getById(User user, string id)
    {
      SessionOfTest session = await _sessionsGR.GetDbSet()
        .Include(s => s.Test)
        .Include(s => s.Questions)
        .ThenInclude(q => q.BaseQuestion)
        .Include(s => s.Questions)
        .ThenInclude(q => q.Options)
        .ThenInclude(o => o.BaseOption)
        .FirstOrDefaultAsync(session => session.UserId == user.Id && session.Id == id);

      if (session == null)
        throw new ClientException("inc-ses-id");

      return session;
    }

    public async Task<OptionOfQuestionInSession> SetQuestionOption(User user, string oid, bool selection)
    {
      var option = await _optionsOfQuestionsInSessionsGR.GetDbSet()
                    .Include(o => o.Question)
                    .ThenInclude(q => q.Session)
                    .ThenInclude(s => s.Test)
                    .Include(o => o.BaseOption)
                    .FirstOrDefaultAsync(o => o.Id == oid && o.Question.Session.UserId == user.Id);

      if (option == null)
        throw new ClientException("inc-opt");

      option.Selected = selection;
      await _optionsOfQuestionsInSessionsGR.UpdateAsync(option);
      return option;
    }

    public async Task<SessionOfTest> EndSessionById(User user, string sid)
    {
      SessionOfTest session = await _sessionsGR.GetDbSet()
        .Include(s => s.Test)
        .Include(s => s.Questions)
        .ThenInclude(q => q.BaseQuestion)
        .Include(s => s.Questions)
        .ThenInclude(q => q.Options)
        .ThenInclude(o => o.BaseOption)
        .FirstOrDefaultAsync(session => session.UserId == user.Id && session.Id == sid);

      if (session == null)
        throw new ClientException("inc-ses-id");

      if (session.EndDateTime.HasValue)
        throw new ClientException("ses-is-already-ended");

      session.EndDateTime = DateTime.Now;
      await _sessionsGR.UpdateAsync(session);
      return session;
    }
  }
}
