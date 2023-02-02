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

            List<string> lineStyle = GetAllLineStyleNames(doc);
            List<string> wallTypes = GetAllWallTypeNames(doc);

            return Result.Succeeded;
        }

        private List<string> GetAllLineStyleNames(Document doc)
        {
            throw new NotImplementedException();
        }

        private List<string> GetAllWallTypeNames(Document doc)
        {
            throw new NotImplementedException();
        }
    }
}
