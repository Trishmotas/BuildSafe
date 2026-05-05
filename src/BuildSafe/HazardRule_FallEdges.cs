using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace SafeDesignLite
{
    public static class HazardRule_FallEdges
    {
        public static List<Hazard> Check(Document doc)
        {
            var results = new List<Hazard>();

            var floors = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Floors)
                .WhereElementIsNotElementType()
                .Cast<Floor>();

            foreach (var floor in floors)
            {
                BoundingBoxXYZ bb = floor.get_BoundingBox(null);
                if (bb == null) continue;

                // Elevated slab → potential fall risk
                if (bb.Min.Z > 10)
                {
                    results.Add(new Hazard
                    {
                        HazardId = System.Guid.NewGuid().ToString(),
                        Category = "Fall Risk",
                        Severity = "High",

                        // 🔥 NEW (THIS SOLVES YOUR REVIEWER ISSUE)
                        Confidence = "Low",
                        IsAssumedRisk = true,
                        Status = "Unreviewed",

                        ElementId = floor.Id,
                        Location = (bb.Min + bb.Max) / 2,

                        Description = "Elevated slab edge (no edge protection modeled)",
                        Recommendation = "Verify if guardrails or temporary edge protection will be installed"
                    });
                }
            }

            return results;
        }
    }
}