using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Bonus_WallsFromLines
{
    internal static class Utils
    {
        internal static Level GetLevelFromView(Document doc)
        {
            View curView = doc.ActiveView;

            SketchPlane curSP = curView.SketchPlane;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Level));
            collector.WhereElementIsNotElementType();

            foreach(Level curLevel in collector)
            {
                if(curLevel.Name == curSP.Name)
                    return curLevel;
            }

            return collector.FirstElement() as Level;
        }

        internal static List<CurveElement> GetLinesByStyle(Document doc, string selectedLineStyle)
        {
            {
                List<CurveElement> results = new List<CurveElement>();

                FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
                collector.OfClass(typeof(CurveElement));

                foreach (CurveElement element in collector)
                {
                    GraphicsStyle curGS = element.LineStyle as GraphicsStyle;

                    if (curGS.Name == selectedLineStyle)
                    {
                        results.Add(element);
                    }
                }

                return results;
            }
        }

        internal static WallType GetWallTypeByName(Document doc, string selectedWallType)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(WallType));

            foreach (WallType wallType in collector)
            {
                if(wallType.Name == selectedWallType)
                    return wallType;
            }

            return null;
        }

        internal static void method1()
        {
            // add method code here
        }
    }
}
