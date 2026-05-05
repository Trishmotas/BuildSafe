using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace SafeDesignLite
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
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
                    TaskDialog.Show("BuildSafe - Safety Analysis", "No hazards found.");
                    return Result.Succeeded;
                }

                HazardWriter.Write(doc, hazards);
                HazardOverlay.Apply(doc, hazards, doc.ActiveView);

                ZoomToHazard(uidoc, hazards[0].ElementId);

                HazardViewer.Show(uidoc, hazards);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("BuildSafe - Error", ex.Message);
                return Result.Failed;
            }
        }

        private void ZoomToHazard(UIDocument uidoc, ElementId elementId)
        {
            try
            {
                uidoc.Selection.SetElementIds(new List<ElementId> { elementId });
                uidoc.ShowElements(elementId);
            }
            catch
            {
                // Prevent crash if navigation fails
            }
        }
    }
}