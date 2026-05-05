using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace SafeDesignLite
{
    public static class HazardWriter
    {
        public static void Write(Document doc, List<Hazard> hazards)
        {
            using (Transaction t = new Transaction(doc, "Write Hazard Data"))
            {
                t.Start();

                foreach (var h in hazards)
                {
                    Element e = doc.GetElement(h.ElementId);
                    if (e == null) continue;

                    Set(e, "Hazard_ID", h.HazardId);
                    Set(e, "Hazard_Severity", h.Severity);
                }

                t.Commit();
            }
        }

        private static void Set(Element e, string name, string value)
        {
            Parameter p = e.LookupParameter(name);

            if (p != null && !p.IsReadOnly && p.StorageType == StorageType.String)
            {
                p.Set(value ?? "");
            }
        }
    }
}