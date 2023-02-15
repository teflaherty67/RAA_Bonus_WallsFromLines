#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace RAA_Bonus_WallsFromLines
{
    [Transaction(TransactionMode.Manual)]
    public class cmdWallsFromLines : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<string> lineStyles = GetAllLineStyleNames(doc);
            List<string> wallTypes = GetAllWallTypeNames(doc);

            frmWallsFromLines curForm = new frmWallsFromLines(lineStyles, wallTypes);

            curForm.Height = 450;
            curForm.Width = 750;
            curForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            if(curForm.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                string selectedLineStyle = curForm.GetSelectedLineStyle();
                string selectedWallType = curForm.GetSelectedWallType();
                double wallHeight = curForm.GetWallHeight();
                bool isStructural = curForm.AreWallsStructural();

                List<CurveElement> curveList = Utils.GetLinesByStyle(doc, selectedLineStyle);

                WallType curWT = Utils.GetWallTypeByName(doc, selectedWallType);

                Level curLevel = Utils.GetLevelFromView(doc);

                using(Transaction t = new Transaction(doc))
                {
                    t.Start("Create Walls");

                    foreach(CurveElement curve in curveList)
                    {
                        Curve curCurve = curve.GeometryCurve;

                        Wall newWall = Wall.Create(doc, curCurve, curWT.Id,
                            curLevel.Id, wallHeight, 0, false, isStructural);
                    }

                    t.Commit();
                }
            }

            return Result.Succeeded;
        }

        private List<string> GetAllLineStyleNames(Document doc)
        {
            List<string> results = new List<string>();

            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            collector.OfClass(typeof(CurveElement));

            foreach(CurveElement element in collector)
            {
                GraphicsStyle curGS = element.LineStyle as GraphicsStyle;

                if(results.Contains(curGS.Name) == false)
                {
                    results.Add(curGS.Name);
                }
            }

            return results;
        }

        private List<string> GetAllWallTypeNames(Document doc)
        {
            List<string> results = new List<string>();

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(WallType));

            foreach(WallType wallType in collector)
            {
                results.Add(wallType.Name);
            }

            return results;
        }
    }
}
