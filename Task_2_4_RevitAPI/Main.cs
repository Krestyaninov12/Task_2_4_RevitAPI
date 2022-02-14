using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_4_RevitAPI
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {//
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            var ductsOnlevel1 = new List<string>();
            var ductsOnlevel2 = new List<string>();

            foreach (var duct in ducts)
            {
                Parameter levelReference = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM);
                var level = levelReference.AsValueString();
                if (level == "Level 1")
                {
                    ductsOnlevel1.Add(level);
                }
                if (level == "Level 2")
                {
                    ductsOnlevel2.Add(level);
                }
            }

            TaskDialog.Show("Ducts on Level", $"Количество воздузоводов на 1 этаже: {ductsOnlevel1.Count}");
            TaskDialog.Show("Ducts on Level", $"Количество воздузоводов на 2 этаже: {ductsOnlevel2.Count}");

            return Result.Succeeded;
        }
    }
}
