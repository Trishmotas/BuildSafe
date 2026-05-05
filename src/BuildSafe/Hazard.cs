using Autodesk.Revit.DB;

namespace SafeDesignLite
{
    public class Hazard
    {
        public string HazardId { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }

        // NEW
        public string Confidence { get; set; }
        public bool IsAssumedRisk { get; set; }
        public string Status { get; set; }

        public ElementId ElementId { get; set; }
        public XYZ Location { get; set; }

        public string Description { get; set; }
        public string Recommendation { get; set; }
    }
}