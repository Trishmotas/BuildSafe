using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SafeDesignLite
{
    public static class HazardRule_LongSpans
    {
        public static List<Hazard> Check(Document doc)
        {
            var results = new List<Hazard>();

            var beams = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming)
                .WhereElementIsNotElementType();

            foreach (Element beam in beams)
            {
                LocationCurve lc = beam.Location as LocationCurve;
                if (lc == null) continue;

                if (lc.Curve.Length > 30)
                {
                    results.Add(new Hazard
                    {
                        HazardId = System.Guid.NewGuid().ToString(),
                        Category = "Temporary Works",
                        Severity = "Medium",

                        // 👇 NEW (keep consistent with your upgrade)
                        Confidence = "Medium",
                        IsAssumedRisk = false,
                        Status = "Unreviewed",

                        ElementId = beam.Id,
                        Location = (lc.Curve.GetEndPoint(0) + lc.Curve.GetEndPoint(1)) / 2,

                        Description = "Long-span beam",
                        Recommendation = "Assess need for temporary supports during construction"
                    });
                }
            }

            return results;
        }
    }
}