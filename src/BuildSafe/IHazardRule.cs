using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SafeDesignLite
{
    public interface IHazardRule
    {
        List<Hazard> Check(Document doc);
    }
}