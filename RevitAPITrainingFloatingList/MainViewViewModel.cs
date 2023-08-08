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

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand=new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickedObjects(commandData);
        }

        private void OnSaveCommand()
        {
            throw new NotImplementedException();
        }

        public DelegateCommand SaveCommand { get; private set; }
        public object PickedObjects { get; private set; }
    }
}
