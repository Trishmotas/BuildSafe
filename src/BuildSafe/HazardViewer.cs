using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafeDesignLite
{
    public static class HazardViewer
    {
        public static void Show(UIDocument uidoc, List<Hazard> hazards)
        {
            int total = hazards.Count;
            int high = hazards.Count(h => h.Severity == "High");
            int medium = hazards.Count(h => h.Severity == "Medium");
            int low = hazards.Count(h => h.Severity == "Low");

            int assumed = hazards.Count(h => h.IsAssumedRisk);
            int confirmed = total - assumed;

            StringBuilder report = new StringBuilder();

            report.AppendLine("=== BUILDSAFE SAFETY ANALYSIS REPORT ===\n");

            report.AppendLine($"Total Hazards: {total}");
            report.AppendLine($"High Severity: {high} | Medium Severity: {medium} | Low Severity: {low}");
            report.AppendLine($"Confirmed Hazards: {confirmed} | Assumption-Based Hazards: {assumed}\n");

            report.AppendLine("----- HAZARD DETAILS -----\n");

            int i = 1;
            foreach (var h in hazards)
            {
                report.AppendLine($"{i}. {h.Category} [{h.Severity}]");
                report.AppendLine($"   Confidence: {h.Confidence} (based on model completeness)");
                report.AppendLine($"   Status: {h.Status}");
                report.AppendLine($"   Risk Type: {(h.IsAssumedRisk ? "Assumption-based risk" : "Confirmed risk")}");

                if (h.IsAssumedRisk)
                    report.AppendLine("   ⚠ No protection modeled - verification required");

                report.AppendLine($"   Description: {h.Description}");
                report.AppendLine($"   Recommendation: {h.Recommendation}\n");

                i++;
            }

            TaskDialog dialog = new TaskDialog("BuildSafe - Hazard Dashboard");

            dialog.MainInstruction = "BuildSafe Safety Summary";

            dialog.MainContent =
                $"Total Hazards: {total}\n" +
                $"High Severity: {high} | Medium Severity: {medium} | Low Severity: {low}\n" +
                $"Confirmed: {confirmed} | Assumption-Based: {assumed}";

            dialog.ExpandedContent = report.ToString();

            dialog.CommonButtons = TaskDialogCommonButtons.Close;
            dialog.Show();
        }
    }
}