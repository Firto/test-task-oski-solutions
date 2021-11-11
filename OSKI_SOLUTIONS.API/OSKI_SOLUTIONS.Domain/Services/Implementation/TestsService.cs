using OSKI_SOLUTIONS.DataAccess.Entities.Tests;
using OSKI_SOLUTIONS.DataAccess.Interfaces;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OSKI_SOLUTIONS.Domain.Services.Implementation
{
  public class TestsService
  {
    private readonly IGenericRepository<Test> _testsGR;

    public TestsService(IGenericRepository<Test> testsGR,
                        ClientErrorManager clientErrorManager)
    {
      _testsGR = testsGR;
      if (!clientErrorManager.IsIssetErrors("Tests"))
        clientErrorManager.AddErrors(new ClientErrors("Tests",
          new Dictionary<string, ClientError>
          {
              {"inc-test-id", new ClientError("Incorrect test ID!")},
          }));
    }

    public async Task<IEnumerable<Test>> GetAllTestsAsync()
    {
      var result = await _testsGR.GetAllAsync();
      return result;
    }

    public async Task<Test> GetByIdAsync(int id)
    {
      var result = await _testsGR.FindAsync(id);
      if (result == null)
        throw new ClientException("inc-test-id");
      return result;
    }
  }
}
