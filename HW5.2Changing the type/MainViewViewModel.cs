using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitAPITrainingLibrary;

namespace HW5._2Changing_the_type
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<Element> PickedObjects { get; } = new List<Element>();
        public List<WallType> WallTypes { get; } = new List<WallType>();

        public WallType SelectedType { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);
            WallTypes =WallTools.GetWallTypes(commandData);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedType == null)
                return;

            using (var ts = new Transaction(doc, "Set wall type"))
            {
                ts.Start();

                foreach (var pickedObject in PickedObjects)
                {
                    if (pickedObject is Wall)
                    {
                        var oWal = pickedObject as Wall;
                        //oWal.SetSystemType(SelectedType.Id);
                        oWal.WallType= SelectedType;
                    }
                }

                ts.Commit();
            }

            RaiseCloseRequest();
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
        //public static List<WallType> GetWallTypes(ExternalCommandData commandData)
        //{
        //    UIApplication uiapp = commandData.Application;
        //    UIDocument uidoc = uiapp.ActiveUIDocument;
        //    Document doc = uidoc.Document;

        //    List<WallType> wallTypes = new FilteredElementCollector(doc)
        //                                                .OfClass(typeof(WallType))
        //                                                .Cast<WallType>()
        //                                                .ToList();

        //    return wallTypes;
        //}
    }
}
