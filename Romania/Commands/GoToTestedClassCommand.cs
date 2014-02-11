using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Romania.Extensions;
using Romania.Extensions.System;
using Romania.UI;

namespace Romania.Commands
{
    public class GoToTestedClassCommand : ICommand
    {
        private readonly Events _events;
        private readonly WindowEvents _winEvents;
        private Window _activeWindow;

        public GoToTestedClassCommand(DTE2 dte)
            : base(dte)
        {
            _events = Dte.Events;
            _winEvents = _events.get_WindowEvents();
            _winEvents.WindowActivated += WindowActivated;
        }

        public override string Text
        {
            get { return "Go to tested class"; }
        }

        private void WindowActivated(Window gotfocus, Window lostfocus)
        {
            _activeWindow = gotfocus;
        }

        public override string GetSpecificName()
        {
            return "GoToTestedClassCommand";
        }

        public override void Execute()
        {
            if (_activeWindow == null || _activeWindow.Document == null)
                return;

            var classNameWithoutExtension = GetTestedClassName(_activeWindow.Document.Name);

            var classNames = GetTestedClassName(classNameWithoutExtension);
            var item = Dte.Solution.FindByName(classNames);
            if (item.Count == 1)
            {
                item.First().OpenIt();
            }
            else if (item.Count > 1)
            {
                var dialog = new ChooseProjectItemDialog(item, projectItem => projectItem.OpenIt(), Dte.Solution);
                dialog.Show();
            }
        }

        private static string GetTestedClassName(string testClassName)
        {
            var classNameWithoutExtension = Path.GetFileNameWithoutExtension(testClassName);

            var patterns = new List<string>
                {
                    "Tests",
                    "UnitTests",
                    "UnitTest",
                    "TestUnit",
                    "Test",
                };
            classNameWithoutExtension = patterns.Aggregate(classNameWithoutExtension,
                                                           (current, pattern) => current.TrimEnd(pattern));

            return classNameWithoutExtension + ".cs";
        }
    }
}