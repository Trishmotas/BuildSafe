using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafeDesignLite
{
    [Transaction(TransactionMode.Manual)]
    public class SafetyAnalysisCommand : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                List<Hazard> hazards = HazardEngine.Run(doc);

                if (hazards == null || hazards.Count == 0)
                {
                    TaskDialog.Show(
                        "Safety Analysis",
                        "No hazards found.\n\nNo risk register was exported.");

                    return Result.Succeeded;
                }

                HazardWriter.Write(doc, hazards);
                HazardOverlay.Apply(doc, hazards, doc.ActiveView);

                string exportPath = HazardExporter.Export(hazards, doc.Title);

                ShowDetailedAnalysis(hazards, exportPath);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Safety Analysis Error", ex.Message);
                return Result.Failed;
            }
        }

        private void ShowDetailedAnalysis(List<Hazard> hazards, string exportPath)
        {
            int total = hazards.Count;
            int high = hazards.Count(h => h.Severity == "High");
            int medium = hazards.Count(h => h.Severity == "Medium");
            int low = hazards.Count(h => h.Severity == "Low");
            int assumed = hazards.Count(h => h.IsAssumedRisk);
            int confirmed = total - assumed;

            StringBuilder details = new StringBuilder();

            details.AppendLine("----- HAZARD DETAILS -----");
            details.AppendLine();

            int i = 1;

            foreach (Hazard h in hazards)
            {
                string hazardId = $"H-{i:D3}";

                details.AppendLine($"Hazard {hazardId}");
                details.AppendLine("========================================");

                details.AppendLine($"Category: {h.Category}");
                details.AppendLine($"Severity: {h.Severity}");
                details.AppendLine();

                details.AppendLine($"Element ID: {h.ElementId.Value}");
                details.AppendLine($"Confidence Level: {h.Confidence}");
                details.AppendLine($"Hazard Classification: {(h.IsAssumedRisk ? "Assumption-Based Risk" : "Confirmed Risk")}");
                details.AppendLine("Review Status: Pending Review");
                details.AppendLine();

                details.AppendLine("Description:");
                details.AppendLine(h.Description);
                details.AppendLine();

                details.AppendLine("Recommendation:");
                details.AppendLine(
                    h.IsAssumedRisk
                        ? "Review design to confirm provision of edge protection, temporary works, exclusion zones or other relevant mitigation."
                        : h.Recommendation
                );

                details.AppendLine();
                details.AppendLine("========================================");
                details.AppendLine();

                i++;
            }

            TaskDialog dialog = new TaskDialog("Safety Analysis");

            dialog.MainInstruction = "BuildSafe Analysis Complete";

            dialog.MainContent =
                $"Total Hazards: {total}\n" +
                $"High Severity: {high} | Medium Severity: {medium} | Low Severity: {low}\n" +
                $"Confirmed Hazards: {confirmed} | Assumption-Based Hazards: {assumed}\n\n" +
                $"BuildSafe Risk Register Generated\n\n" +
                $"Location:\n{exportPath}\n\n" +
                $"Risk Register Includes:\n" +
                $"• Hazard classifications\n" +
                $"• Severity ratings\n" +
                $"• Confidence levels\n" +
                $"• Review status\n" +
                $"• Recommendations\n" +
                $"• Suggested owner\n" +
                $"• Required actions";

            dialog.ExpandedContent = details.ToString();
            dialog.CommonButtons = TaskDialogCommonButtons.Close;
            dialog.Show();
        }
    }
}