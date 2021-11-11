using OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests
{
  [Table("Tests")]
  public class Test
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int MaxCountOfQuestions { get; set; }
    [Required]
    public int TestLengthInMinuts { get; set; }
    public ICollection<QuestionOfTest> Questions { get; set; }
    public ICollection<SessionOfTest> Sessions { get; set; }

    public Test()
    {
      Questions = new List<QuestionOfTest>();
      Sessions = new List<SessionOfTest>();
    }
  }
}
