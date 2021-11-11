using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests
{
  [Table("OptionsOfQuestions")]
  public class OptionOfQuestion
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Answer { get; set; }
    [Required]
    public bool Correct { get; set; } = false;

    [Required]
    public int QuestionId { get; set; }
    [ForeignKey("QuestionId")]
    public QuestionOfTest Question { get; set; }
  }
}
