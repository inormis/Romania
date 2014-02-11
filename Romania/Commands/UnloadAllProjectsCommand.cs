using System;
using EnvDTE;
using EnvDTE80;
using Romania.Extensions;

namespace Romania.Commands
{
    public class UnloadAllProjectsCommand : ICommand
    {
        public UnloadAllProjectsCommand(DTE2 dte, AddIn addIn)
            : base(dte)
        {
        }

        public override string Text
        {
            get { throw new NotImplementedException(); }
        }

        public override string GetSpecificName()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            var solution = Dte.GetSolutionClass();

            var solutionExplorer = Dte.GetSolutionExplorer();
            solutionExplorer.Activate();

            foreach (var project in solution.GetAllProjects())
            {
                Dte.SelectProject(project);
                Dte.ExecuteCommand(CommandNames.UnloadProjectCommandName);
            }
        }
    }
}