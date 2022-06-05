using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Interface
{
    public interface ISequenceService
    {
        Task<int> GetKnowledgeBaseNewId();
        Task<int> GetCategoryNewId();
    }
}
