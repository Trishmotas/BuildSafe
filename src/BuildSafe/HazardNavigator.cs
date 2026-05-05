using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace SafeDesignLite
{
    public static class HazardNavigator
    {
        public static void ZoomTo(UIDocument uidoc, ElementId id)
        {
            if (uidoc == null || id == null) return;

            uidoc.ShowElements(id);
            uidoc.RefreshActiveView();
        }
    }
}