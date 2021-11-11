using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests
{
  [Table("QuestionsOfTests")]
  public class QuestionOfTest
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int TestId { get; set; }
    [ForeignKey("TestId")]
    public Test Test { get; set; }

    [Required]
    public string Question { get; set; }
    [Required]
    public int maxSelectedOptionsCount { get; set; } = 1;
    [Required]
    public int maxOptionsCount { get; set; } = 4;

    public ICollection<OptionOfQuestion> Options { get; set; }

    public QuestionOfTest()
    {
      Options = new List<OptionOfQuestion>();
    }
  }
}
