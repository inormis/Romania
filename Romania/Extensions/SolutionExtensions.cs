using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using VSLangProj;

namespace Romania.Extensions
{
    public static class SolutionExtensions
    {
        private const string NameItem = "Name";
        private static readonly List<Project> EmptyProjectList = new List<Project>();

        public static string GetName(this SolutionClass solution)
        {
            return solution.Properties.Item(NameItem).Value.ToString();
        }

        public static List<Project> GetAllProjects(this Solution solution)
        {
            return solution.Projects.OfType<Project>().SelectMany(project => project.GetSelfOrSubProjects()).ToList();
        }

        public static List<Project> GetSelfOrSubProjects(this Project project)
        {
            if (project.IsProjectFile())
                return new List<Project> {project};
            if (project.IsFolder())
            {
                return GetSolutionFolderProjects(project);
            }
            return EmptyProjectList;
        }

        private static List<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            var list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                    continue;

                if (subProject.IsFolder())
                    list.AddRange(GetSolutionFolderProjects(subProject));
                else
                    list.Add(subProject);
            }

            return list;
        }

        public static List<Project> GetExecutableProjects(this Solution solution)
        {
            return GetAllProjects(solution)
                .Where(project => project.GetOutputType() != prjOutputType.prjOutputTypeLibrary)
                .ToList();
        }

        public static List<ProjectItem> FindByName(this Solution solution, params string[] names)
        {
            var result = new List<ProjectItem>();
            if (names.Length == 0)
                return result;

            var allProjects = GetAllProjects(solution);
            foreach (var project in allProjects)
            {
                foreach (ProjectItem projectItem in project.ProjectItems)
                {
                    var nestedItem = GetNestedProjectItem(projectItem, names);
                    if (nestedItem != null)
                        result.AddRange(nestedItem);
                }
            }
            return result;
        }

        private static IEnumerable<ProjectItem> GetNestedProjectItem(ProjectItem projectItem, params string[] names)
        {
            var result = new List<ProjectItem>();

            if (names.Contains(projectItem.Name))
                result.Add(projectItem);

            if (projectItem.ProjectItems.Count > 0)
            {
                foreach (ProjectItem item in projectItem.ProjectItems)
                {
                    var found = GetNestedProjectItem(item, names);
                    if (found != null)
                        result.AddRange(found);
                }
            }
            return result;
        }
    }
}