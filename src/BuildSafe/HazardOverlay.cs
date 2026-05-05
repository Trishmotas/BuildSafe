using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SafeDesignLite
{
    public static class HazardOverlay
    {
        public static void Apply(Document doc, List<Hazard> hazards, View view)
        {
            if (view == null) return;

            using (Transaction t = new Transaction(doc, "Highlight Hazards"))
            {
                t.Start();

                foreach (var h in hazards)
                {
                    OverrideGraphicSettings ogs = new OverrideGraphicSettings();

                    if (h.Severity == "High")
                    {
                        ogs.SetProjectionLineColor(new Color(255, 0, 0));
                        ogs.SetSurfaceTransparency(20);
                    }
                    else if (h.Severity == "Medium")
                    {
                        ogs.SetProjectionLineColor(new Color(255, 165, 0));
                        ogs.SetSurfaceTransparency(40);
                    }
                    else
                    {
                        ogs.SetProjectionLineColor(new Color(0, 255, 0));
                        ogs.SetSurfaceTransparency(60);
                    }

                    view.SetElementOverrides(h.ElementId, ogs);
                }

                t.Commit();
            }
        }
    }
}