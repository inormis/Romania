using System.Linq;
using EnvDTE;
using EnvDTE80;
using Romania.Extensions;
using Romania.UI;

namespace Romania.Commands
{
    public class SetStartUpProjectCommand : ICommand
    {
        public SetStartUpProjectCommand(DTE2 dte) : base(dte)
        {
        }

        public override string Text
        {
            get { return "Set start up project"; }
        }

        public override string Tooltip
        {
            get { return "Shows dialog with list of executable projects"; }
        }

        public override string GetSpecificName()
        {
            return "SetStartUpProject";
        }

        public override void Execute()
        {
            var allProjects = Dte.Solution.GetExecutableProjects();
            if (allProjects.Count == 1)
                SetStartUpProject(allProjects.First());
            else if (allProjects.Count > 1)
            {
                var window = new SetStartUpProjectDialog(allProjects, SetStartUpProject);
                window.Show();
            }
        }

        private void SetStartUpProject(Project project)
        {
            Dte.Solution.SolutionBuild.StartupProjects = project.FullName;
        }
    }
}