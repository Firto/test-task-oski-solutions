using System;
using System.Collections.Generic;
using System.Text;

namespace OSKI_SOLUTIONS.Helpers.DtoModels.Tests.Sessions
{
  public class QuestionOfSessionDto
  {
    public string Id { get; set; }
    public string Question { get; set; }
    public int maxSelectedOptionsCount { get; set; }
    public int maxOptionsCount { get; set; }

    public IEnumerable<OptionOfQuestionInSessionDto> Options { get; set; }
  }
}
