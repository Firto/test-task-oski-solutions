using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests;
using OSKI_SOLUTIONS.DataAccess.Interfaces;

namespace OSKI_SOLUTIONS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;
        IGenericRepository<Test> _testsGR;
        IGenericRepository<User> _userGR;

        public ValuesController(IWebHostEnvironment webHostEnvironment, 
                                IGenericRepository<Test> testsGR,
                                IGenericRepository<User> userGR) {
            _webHostEnvironment = webHostEnvironment;
            _testsGR = testsGR;
            _userGR = userGR;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return new string[] { _webHostEnvironment.ContentRootPath, _webHostEnvironment.WebRootPath };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            /*List<Test> tests = new List<Test>();
            for (int i = 0; i < 10; i++)
            {
                var test = new Test() { TestLengthInMinuts = 30, MaxCountOfQuestions = Faker.RandomNumber.Next(10, 15), Name = Faker.Lorem.Sentence(10), Description = Faker.Lorem.Paragraph(10) };
                for (int j = 0; j < 20; j++)
                {
                    var question = new QuestionOfTest { Question = Faker.Lorem.Sentence(10), maxOptionsCount = Faker.RandomNumber.Next(4, 6), maxSelectedOptionsCount = Faker.RandomNumber.Next(1, 4) };

                    for (int z = 0; z < 14; z++)
                    {
                        var option = new OptionOfQuestion { Answer = Faker.Lorem.Sentence(10), Correct = false };
                        question.Options.Add(option);
                    }

                    for (int z = 0; z < 6; z++)
                    {
                      var option = new OptionOfQuestion { Answer = "Correct " + z, Correct = true };
                      question.Options.Add(option);
                    }

                    test.Questions.Add(question);
                }
                tests.Add(test);
            }
            _testsGR.GetDbSet().AddRange(tests);
            _testsGR.GetDbContext().SaveChanges();*/
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
