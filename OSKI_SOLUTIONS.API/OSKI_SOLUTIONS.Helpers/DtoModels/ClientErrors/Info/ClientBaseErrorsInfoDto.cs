using System.Collections.Generic;

namespace OSKI_SOLUTIONS.Helpers.DtoModels.ClientErrors.Info
{
    public class ClientBaseErrorsInfoDto 
    {
        public IDictionary<string, ClientErrorInfoDto> Errors { get; set; }
        public IEnumerable<ClientBaseErrorsInfoDto> Dictionaries { get; set; }
    }
}
