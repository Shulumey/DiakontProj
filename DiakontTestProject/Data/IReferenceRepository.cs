using System.Collections.Generic;
using DiakontTestProject.Model;

namespace DiakontTestProject.Data
{
    /// <summary>
    /// Репозиторий для справочников
    /// </summary>
    public interface IReferenceRepository
    {
        IEnumerable<ReferenceItem> GetPositions();
        IEnumerable<ReferenceItem> GetDepartments();
    }
}
