using Autodesk.Revit.UI;
using System.Reflection;

namespace SafeDesignLite
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            string tabName = "BuildSafe";

            try { app.CreateRibbonTab(tabName); } catch { }

            RibbonPanel panel = app.CreateRibbonPanel(tabName, "Safety Analysis");

            string path = Assembly.GetExecutingAssembly().Location;

            PushButtonData runAnalysisButton = new PushButtonData(
                "RunSafetyAnalysis",
                "Run Safety\nAnalysis",
                path,
                "SafeDesignLite.SafetyAnalysisCommand"
            );

            runAnalysisButton.ToolTip =
                "Runs BuildSafe safety analysis: detects hazards, highlights them, zooms to hazards, exports a report, and displays the safety dashboard.";

            panel.AddItem(runAnalysisButton);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }
    }
}