using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using e3;

namespace KSPE3Lib
{
    internal class ProjectDispatcher
    {
        private int undefinedSelectionIndex = -1;
        private int selectedIndex;
        private CT.Dispatcher e3Dispatcher;

        internal ProjectDispatcher()
        {
            e3Dispatcher = new CT.Dispatcher();
            selectedIndex = undefinedSelectionIndex;
        }

        internal int GetApplicationProcessId(ref Status status)
        {
            List<Process> e3Processes = Process.GetProcessesByName("E3.series").ToList<Process>();
            e3Processes.RemoveAll(process => !IsAppropriateProcess(process));
            switch (e3Processes.Count)
            {
                case 0:
                    status = Status.None;
                    return undefinedSelectionIndex;
                case 1:
                    status = Status.Selected;
                    return e3Processes[0].Id;
                default:
                    List<string> projectNames = new List<string>();
                    foreach (Process process in e3Processes)
                    {
                        e3Application e3App = e3Dispatcher.GetE3ByProcessId(process.Id) as e3Application;
                        projectNames.Add(e3App.CreateJobObject().GetName());
                    }
                    ProjectSelectingWindow window = new ProjectSelectingWindow(projectNames, new Action<int>(index=>selectedIndex = index));
                    window.ShowDialog();
                    if (selectedIndex != undefinedSelectionIndex)
                    {
                        status = Status.Selected;
                        return e3Processes[selectedIndex].Id;
                    }
                    else
                    {
                        status = Status.NoSelected;
                        return undefinedSelectionIndex;
                    }
            }
        }

        internal e3Application GetE3ApplicationByProcessId(int id)
        {
            return e3Dispatcher.GetE3ByProcessId(id) as e3Application;
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
