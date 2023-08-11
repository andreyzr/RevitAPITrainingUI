using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5._1Info
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand PipeInfo { get; }
        public DelegateCommand WallInfo { get; }
        public DelegateCommand DoorInfo { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            PipeInfo = new DelegateCommand(PipeCount);
            WallInfo = new DelegateCommand(WallCount);
            DoorInfo = new DelegateCommand(DoorCount);
        }
        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        private void PipeCount()
        {
            RaiseHideRequest();

            List<Pipe> lPipe = Info.GetPipes(_commandData);

            TaskDialog.Show("Информация", $"Число труб:{lPipe.Count}");

            RaiseShowRequest();
        }

        private void WallCount()
        {
            RaiseHideRequest();

            List<Wall> lWall = Info.GetWalls(_commandData);

            TaskDialog.Show("Информация", $"Число стен:{lWall.Count}");

            RaiseShowRequest();
        }

        private void DoorCount()
        {
            RaiseHideRequest();

            List<FamilyInstance> lDoors = Info.GetDoors(_commandData);

            TaskDialog.Show("Информация", $"Число дверей:{lDoors.Count}");

            RaiseShowRequest();
        }
    }
}
