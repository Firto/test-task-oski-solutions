using AutoMapper;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session;
using OSKI_SOLUTIONS.Helpers.DtoModels.Tests;
using OSKI_SOLUTIONS.Helpers.DtoModels.Tests.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSKI_SOLUTIONS.Domain.Profiles
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Test, TestDto>().AfterMap((from, to, opt) => {
        if (opt.Items.ContainsKey("avaibleSessions"))
        {
          var session = ((List<KeyValuePair<int, string>?>)opt.Items["avaibleSessions"]).FirstOrDefault(ses => ses.Value.Key == to.Id);
          if (session != null)
            to.SessionAvaible = session.Value.Value;
        }
      }).ReverseMap();
      CreateMap<OptionOfQuestionInSession, OptionOfQuestionInSessionDto>().AfterMap((from, to) => {
        to.Answer = from.BaseOption.Answer;
        if (from.Question != null && from.BaseOption != null && from.Question.Session != null &&
            (
              from.Question.Session.EndDateTime.HasValue ||
              (
                from.Question.Session.Test != null &&
                from.Question.Session.StartDateTime.AddMinutes(from.Question.Session.Test.TestLengthInMinuts) <= DateTime.Now
              )
            )
           )
          to.Correct = from.BaseOption.Correct;
      }).ReverseMap();
      CreateMap<QuestionOfSession, QuestionOfSessionDto>().AfterMap((from, to) => {
        to.Question = from.BaseQuestion.Question;
        to.maxOptionsCount = from.BaseQuestion.maxOptionsCount;
        to.maxSelectedOptionsCount = from.BaseQuestion.maxSelectedOptionsCount;
      }).ReverseMap();
      CreateMap<SessionOfTest, SessionDto>().AfterMap((from, to) => {
        if (!to.EndDateTime.HasValue)
        {
          if (!from.EndDateTime.HasValue && from.Test != null && from.StartDateTime.AddMinutes(from.Test.TestLengthInMinuts) <= DateTime.Now)
            to.EndDateTime = from.StartDateTime.AddMinutes(from.Test.TestLengthInMinuts);
          else
            to.EndDateTime = null;
        }
      }).ReverseMap();
    }
  }
}
