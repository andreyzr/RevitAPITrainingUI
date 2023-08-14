using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAPITrainingRiddonHW
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Revit API training HW";
            application.CreateRibbonTab(tabName);
            string utilsFolderPath = @"C:\RevitAPITraining\";

            var panel1 = application.CreateRibbonPanel(tabName, "Работа с проектом");
            var panel2 = application.CreateRibbonPanel(tabName, "Работа с типами семейств");

            var button1 = new PushButtonData("Проект", "Сведения о проекте",
                Path.Combine(utilsFolderPath, "HW5.1Info.dll"),
                "HW5._1Info.Main");

            var button2 = new PushButtonData("Стены", "Смена типа стен",
                Path.Combine(utilsFolderPath, "HW5.2Changing_the_type.dll"),
                "HW5._2Changing_the_type.Main");

            Uri uriImage1 = new Uri(@"C:\RevitAPITraining\Images\Pro.png", UriKind.Absolute);
            BitmapImage largeImage1 = new BitmapImage(uriImage1);
            button1.LargeImage = largeImage1;

            Uri uriImage2 = new Uri(@"C:\RevitAPITraining\Images\Wall.png", UriKind.Absolute);
            BitmapImage largeImage2 = new BitmapImage(uriImage2);
            button2.LargeImage = largeImage2;

            panel1.AddItem(button1);
            panel2.AddItem(button2);

            return Result.Succeeded;
        }
    }
}
