using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session
{
  [Table("QuestionsOfSessions")]
  public class QuestionOfSession
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public int BaseQuestionId { get; set; }
    [ForeignKey("BaseQuestionId")]
    public QuestionOfTest BaseQuestion { get; set; }

    [Required]
    public string SessionOfTestId { get; set; }
    [ForeignKey("SessionOfTestId")]
    public SessionOfTest Session { get; set; }

    public ICollection<OptionOfQuestionInSession> Options { get; set; }
    public QuestionOfSession()
    {
      Options = new List<OptionOfQuestionInSession>();
    }

    public QuestionOfSession(QuestionOfTest ofTest): this()
    {
      BaseQuestion = ofTest;

      var rnd = new Random();
      Options = BaseQuestion.Options.Where(item => !item.Correct).OrderBy(item => rnd.Next()).Take(BaseQuestion.maxOptionsCount - BaseQuestion.maxSelectedOptionsCount).Concat(
                        BaseQuestion.Options.Where(item => item.Correct).OrderBy(item => rnd.Next()).Take(BaseQuestion.maxSelectedOptionsCount)
                      ).OrderBy(item => rnd.Next()).Select(o => new OptionOfQuestionInSession(o)).ToList();
    }
  }
}
