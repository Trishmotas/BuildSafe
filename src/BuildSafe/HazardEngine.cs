using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SafeDesignLite
{
    public static class HazardEngine
    {
        public static List<Hazard> Run(Document doc)
        {
            var hazards = new List<Hazard>();

            hazards.AddRange(HazardRule_FallEdges.Check(doc));
            hazards.AddRange(HazardRule_LongSpans.Check(doc));

            return hazards;
        }
    }
}