using System;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Romania.Extensions;
using System.Linq;

namespace Romania.Commands
{
    public class FindStyleCommand : ICommand
    {
        public FindStyleCommand(DTE2 dte) : base(dte)
        {
        }

        public override string Text
        {
            get { return "Find resource with the style"; }
        }

        public override string GetSpecificName()
        {
            return "FindStyleCommand";
        }

        public override void Execute()
        {
            var selection = ((TextSelection)Dte.ActiveDocument.Selection).Text;
            if (string.IsNullOrWhiteSpace(selection))
                return;

            var formattedSelection = string.Format("x:Key=\"{0}\"", selection);
            Predicate<string> filter = s => File.ReadAllText(s).Contains(formattedSelection);

            var projectItems = Dte.Solution.GetAllProjects().SelectMany(project => project.GetAllItems(item =>
                {
                    var path = item.FileNames[0];
                    return path.EndsWith(".xaml", StringComparison.InvariantCultureIgnoreCase) && filter(path);
                })).ToList();

            foreach (var projectItem in projectItems)
            {
                projectItem.OpenIt();
                var seel=((TextSelection)Dte.ActiveDocument.Selection);
                
                var lines=File.ReadAllLines(projectItem.FileNames[0]);
                for (int lineIndex = 0; lineIndex < lines.Count(); lineIndex++)
                {
                    if (lines[lineIndex].Contains(formattedSelection))
                    {
                        seel.GotoLine(lineIndex);
                        seel.FindText(formattedSelection, (int) vsFindOptions.vsFindOptionsMatchWholeWord);
                        break;
                    }
                }
            }
        }
    }
}