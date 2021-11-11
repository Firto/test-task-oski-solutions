using System;
using System.Collections.Generic;
using System.Text;

namespace OSKI_SOLUTIONS.Helpers.DtoModels.Tests
{
  public class TestDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int MaxCountOfQuestions { get; set; }
    public int TestLengthInMinuts { get; set; }
    public string SessionAvaible { get; set; } = null;
  }
}
