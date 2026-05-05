using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SafeDesignLite
{
    public static class HazardExporter
    {
        public static string Export(List<Hazard> hazards, string modelName)
        {
            string folderPath = @"C:\Temp\BuildSafe";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string safeModelName = string.IsNullOrWhiteSpace(modelName)
                ? "RevitModel"
                : MakeSafeFileName(modelName);

            string filePath = Path.Combine(
                folderPath,
                $"BuildSafe_Risk_Register_{safeModelName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            );

            StringBuilder csv = new StringBuilder();

            csv.AppendLine(
                "Risk ID,Model Name,Element ID,Hazard Category,Description,Severity,Confidence Level,Hazard Classification,Review Status,Recommendation,Suggested Owner,Required Action,Date Generated"
            );

            int i = 1;
            string dateGenerated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (Hazard h in hazards)
            {
                string riskId = $"BS-RR-{i:D3}";
                string classification = h.IsAssumedRisk ? "Assumed Risk" : "Confirmed Risk";
                string reviewStatus = "Pending Review";
                string suggestedOwner = GetSuggestedOwner(h);
                string requiredAction = GetRequiredAction(h);

                csv.AppendLine(
                    $"{Escape(riskId)}," +
                    $"{Escape(modelName)}," +
                    $"{h.ElementId.Value}," +
                    $"{Escape(h.Category)}," +
                    $"{Escape(h.Description)}," +
                    $"{Escape(h.Severity)}," +
                    $"{Escape(h.Confidence)}," +
                    $"{Escape(classification)}," +
                    $"{Escape(reviewStatus)}," +
                    $"{Escape(h.Recommendation)}," +
                    $"{Escape(suggestedOwner)}," +
                    $"{Escape(requiredAction)}," +
                    $"{Escape(dateGenerated)}"
                );

                i++;
            }

            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);

            return filePath;
        }

        private static string GetSuggestedOwner(Hazard h)
        {
            if (h.Category != null && h.Category.ToLower().Contains("fall"))
            {
                return "Design Manager / Health and Safety Lead";
            }

            if (h.Category != null && h.Category.ToLower().Contains("temporary"))
            {
                return "Temporary Works Coordinator";
            }

            return "Design Manager";
        }

        private static string GetRequiredAction(Hazard h)
        {
            if (h.IsAssumedRisk)
            {
                return "Review BIM model and confirm whether protection or mitigation is required.";
            }

            return "Review hazard and confirm mitigation action before construction stage.";
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private static string MakeSafeFileName(string value)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(c, '_');
            }

            return value;
        }
    }
}