using System;
using System.Collections.Generic;
using System.Text;

namespace OSKI_SOLUTIONS.Helpers.DtoModels.Tests.Sessions
{
  public class OptionOfQuestionInSessionDto
  {
    public string Id { get; set; }
    public string Answer { get; set; }
    public bool Selected { get; set; }

    public bool? Correct { get; set; }
  }
}
