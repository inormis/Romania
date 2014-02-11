using System;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Romania.Extensions;
using Romania.UI;

namespace Romania.Commands
{
    public class GoToClassTestCommand : ICommand
    {
        private readonly Events _events;
        private readonly WindowEvents _winEvents;
        private Window _activeWindow;

        public GoToClassTestCommand(DTE2 dte) : base(dte)
        {
            _events = Dte.Events;
            _winEvents = _events.get_WindowEvents();
            _winEvents.WindowActivated += WindowActivated;
        }

        public override string Text
        {
            get { return "Go to test class"; }
        }

        private void WindowActivated(Window gotfocus, Window lostfocus)
        {
            _activeWindow = gotfocus;
        }

        public override string GetSpecificName()
        {
            return "GoToClassTestCommand";
        }

        public override void Execute()
        {
            if (_activeWindow == null || _activeWindow.Document == null)
                return;

            var classNameWithoutExtension = Path.GetFileNameWithoutExtension(_activeWindow.Document.Name);

            var classNames = GetTestNames(classNameWithoutExtension);
            var item = Dte.Solution.FindByName(classNames);
            if (item.Count == 1)
                item.First().OpenIt();
            else if (item.Count > 1)
            {

                var dialog = new ChooseProjectItemDialog(item, projectItem => projectItem.OpenIt(), Dte.Solution);
                dialog.Show();
            }
        }

        private static string[] GetTestNames(string classNameWithoutExtension)
        {
            return new[]
                {
                    classNameWithoutExtension + "Tests.cs",
                    classNameWithoutExtension + "UnitTests.cs",
                    classNameWithoutExtension + "UnitTest.cs",
                    classNameWithoutExtension + "Test.cs",
                };
        }
    }
}