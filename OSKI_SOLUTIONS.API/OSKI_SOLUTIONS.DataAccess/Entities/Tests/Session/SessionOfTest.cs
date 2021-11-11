using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session
{
  [Table("SessionsOfTests")]
  public class SessionOfTest
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    public int TestId { get; set; }
    [ForeignKey("TestId")]
    public Test Test { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; } = null;

    public ICollection<QuestionOfSession> Questions { get; set; }
    public SessionOfTest()
    {
      Questions = new List<QuestionOfSession>();
    }

    public SessionOfTest(User user, Test test): this()
    {
      UserId = user.Id;
      TestId = test.Id;
      Test = test;

      var rnd = new Random();
      Questions = Test.Questions.OrderBy(item => rnd.Next()).Take(Test.MaxCountOfQuestions).Select(q => new QuestionOfSession(q)).ToList();

      StartDateTime = DateTime.Now;
    }
  }
}
