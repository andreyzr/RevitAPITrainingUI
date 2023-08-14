using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
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
            WallInfo = new DelegateCommand(WallVolume);
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

            TaskDialog.Show("Информация", $"Число труб: {lPipe.Count} шт.");

            RaiseShowRequest();
        }

        private void WallVolume()
        {
            RaiseHideRequest();
            double info = 0;
            List<Wall> lWall = Info.GetWalls(_commandData);
            foreach (Wall wall in lWall)
            {
                Parameter parameter = wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
                info +=UnitUtils.ConvertFromInternalUnits(parameter.AsDouble(),UnitTypeId.Meters)  ;
            }

            TaskDialog.Show("Информация", $"Объем стен: {info:f2} м3");

            RaiseShowRequest();
        }

        private void DoorCount()
        {
            RaiseHideRequest();

            List<FamilyInstance> lDoors = Info.GetDoors(_commandData);

            TaskDialog.Show("Информация", $"Число дверей: {lDoors.Count} шт.");

            RaiseShowRequest();
        }
    }
}
