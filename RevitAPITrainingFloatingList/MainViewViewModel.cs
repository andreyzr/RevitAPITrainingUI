using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingFloatingList
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<Element> PickedObjects { get; } = new List<Element>();
        public List<PipingSystemType> PipeSystems { get; } = new List<PipingSystemType>();

        public PipingSystemType SelectedPipeSystem { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);
            PipeSystems = PipesUtils.GetPipingSystems(commandData);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedPipeSystem == null)
                return;

            using (var ts = new Transaction(doc, "Set system type"))
            {
                ts.Start();

                foreach (var pickedObject in PickedObjects)
                {
                    if (pickedObject is Pipe)
                    {
                        var oPipe = pickedObject as Pipe;
                        oPipe.SetSystemType(SelectedPipeSystem.Id);
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
    }
}
