using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session
{
  [Table("OptionsOfQuestionsInSessions")]
  public class OptionOfQuestionInSession
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [Required]
    public bool Selected { get; set; } = false;

    [Required]
    public int BaseOptionId { get; set; }
    [ForeignKey("BaseOptionId")]
    public OptionOfQuestion BaseOption { get; set; }

    [Required]
    public string QuestionOfSessionId { get; set; }
    [ForeignKey("QuestionOfSessionId")]
    public QuestionOfSession Question { get; set; }

    public OptionOfQuestionInSession() { }
    public OptionOfQuestionInSession(OptionOfQuestion ofQuestion)
    {
      BaseOption = ofQuestion;
    }
  }
}
