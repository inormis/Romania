using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using EnvDTE80;
using VSLangProj;
using System.Linq;

namespace Romania.Extensions
{
    public static class ProjectExtensions
    {
        private const string ProjectExtension = ".csproj";

        public static bool IsProjectOrFolder(this Project project)
        {
            return project.Name != "Miscellaneous Files";
        }

        public static bool IsProjectFile(this Project project)
        {
            return
                string.Compare(Path.GetExtension(project.FileName), ProjectExtension,
                               StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static bool IsFolder(this Project project)
        {
            return project.Kind == ProjectKinds.vsProjectKindSolutionFolder;
        }

        public static prjOutputType? GetOutputType(this Project project)
        {
            try
            {
                return (prjOutputType) project.Properties.Item("OutputType").Value;
            }
            catch
            {
                return null;
            }
        }

        public static List<ProjectItem> GetAllItems(this Project project, Predicate<ProjectItem> filter=null)
        {
            var result = new List<ProjectItem>();
     
                foreach (ProjectItem projectItem in project.ProjectItems)
                {
                    var nestedItem = GetNestedProjectItem(projectItem, filter);
                    if (nestedItem != null)
                        result.AddRange(nestedItem);
                }
            return result;
        }

        private static IEnumerable<ProjectItem> GetNestedProjectItem(ProjectItem projectItem,
                                                                     Predicate<ProjectItem> filter = null)
        {
            var result = new List<ProjectItem>();
            if (projectItem.IsPhysicalFile() && (filter == null || filter(projectItem)))
                result.Add(projectItem);

            if (projectItem.ProjectItems.Count > 0)
            {
                foreach (ProjectItem item in projectItem.ProjectItems)
                {
                    var found = GetNestedProjectItem(item, filter);
                    if (found.Any())
                        result.AddRange(found);
                }
            }
            else if (filter == null || filter(projectItem))
                result.Add(projectItem);
            return result;
        }
    }
}