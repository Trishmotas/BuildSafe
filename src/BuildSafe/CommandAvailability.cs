using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace SafeDesignLite
{
    public class CommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication app, CategorySet selectedCategories)
        {
            return true;
        }
    }
}