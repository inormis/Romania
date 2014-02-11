using System.Collections.Generic;
using System.IO;
using EnvDTE;

namespace Romania.Extensions
{
    public static class ProjectItemExtensions
    {
        public static void OpenIt(this ProjectItem projectItem)
        {
            var fileNames = projectItem.FileNames[0];
            projectItem.DTE.ItemOperations.OpenFile(fileNames, Constants.vsViewKindCode);
        }

        public static string GetPath(this ProjectItem projectItem, Solution solution)
        {
            var solutionPath = Directory.GetParent(solution.FileName).FullName;

            var fileName = projectItem.FileNames[0];
            return fileName.StartsWith(solutionPath) ? fileName.Remove(0, solutionPath.Length) : fileName;
        }

        private const string PhysicalFileKind = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}";

        public static bool IsPhysicalFile(this ProjectItem projectItem)
        {
            return File.Exists(projectItem.FileNames[0]);
        }
    }
}