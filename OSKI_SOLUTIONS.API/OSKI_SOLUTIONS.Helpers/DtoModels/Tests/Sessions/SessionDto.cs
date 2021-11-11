using System;
using System.Collections.Generic;
using System.Text;

namespace OSKI_SOLUTIONS.Helpers.DtoModels.Tests.Sessions
{
  public class SessionDto
  {
    public string Id { get; set; }
    public TestDto Test { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; } = null;
    public ICollection<QuestionOfSessionDto> Questions { get; set; }
  }
}
