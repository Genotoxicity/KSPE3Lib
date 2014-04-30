using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using e3;

namespace KSPE3Lib
{
    internal class ApplicationDispatcher
    {
        private const int undefinedSelectionIndex = -1;
        private CT.Dispatcher e3Dispatcher;

        internal SelectionStatus SelectedStatus { get; private set; }

        internal int SelectedProcessId { get; private set; }

        internal string SelectedProjectTitle { get; private set; }

        internal ApplicationDispatcher()
        {
            e3Dispatcher = new CT.Dispatcher();
        }

        internal void Select()
        {
            List<Process> e3Processes = Process.GetProcessesByName("E3.series").ToList<Process>();
            e3Processes.RemoveAll(process => !IsAppropriateProcess(process));
            SelectedProcessId = 0;
            SelectedProjectTitle = String.Empty;
            switch (e3Processes.Count)
            {
                case 0:
                    SelectedStatus = SelectionStatus.None;
                    break;
                case 1:
                    SelectedStatus = SelectionStatus.Selected;
                    SelectedProcessId = e3Processes[0].Id;
                    SelectedProjectTitle = ((e3Application)e3Dispatcher.GetE3ByProcessId(e3Processes[0].Id)).CreateJobObject().GetName();
                    break;
                default:
                    int selectedIndex = undefinedSelectionIndex;
                    List<string> projectNames = new List<string>(e3Processes.Count);
                    foreach (Process process in e3Processes)
                        projectNames.Add(((e3Application)e3Dispatcher.GetE3ByProcessId(process.Id)).CreateJobObject().GetName());
                    ApplicationSelectingWindow window = new ApplicationSelectingWindow(projectNames, new Action<int>(index=>selectedIndex = index));
                    window.ShowDialog();
                    if (selectedIndex != undefinedSelectionIndex)
                    {
                        SelectedStatus = SelectionStatus.Selected;
                        SelectedProcessId = e3Processes[selectedIndex].Id;
                        SelectedProjectTitle = projectNames[selectedIndex];
                    }
                    else
                        SelectedStatus = SelectionStatus.NoSelected;
                    break;
            }
        }

        private bool IsAppropriateProcess(Process process)
        {
            e3Application e3App = e3Dispatcher.GetE3ByProcessId(process.Id) as e3Application;
            if (e3App == null)  // на случай открытой БД
                return false;
            int jobCount = e3App.GetJobCount();
            if (jobCount == 0)  // на случай приложения без открытого проекта
                return false;
            return true;
        }


    }
}
