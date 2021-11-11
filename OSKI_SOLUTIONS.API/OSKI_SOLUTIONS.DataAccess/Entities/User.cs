using OSKI_SOLUTIONS.DataAccess.Entities.Authorization;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSKI_SOLUTIONS.DataAccess.Entities
{
    public class User : AuthUser
    {
        [Required, MaxLength(120)]
        public string Login { get; set; }
        [Required, MaxLength(120)]
        public string PasswordHash { get; set; }

        public ICollection<SessionOfTest> Sessions { get; set; }
  }
}
