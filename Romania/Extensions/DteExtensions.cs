using EnvDTE;
using EnvDTE80;

namespace Romania.Extensions
{
    public static class DteExtensions
    {
        public static Window GetSolutionExplorer(this DTE2 dte)
        {
            return dte.Windows.Item(Constants.vsWindowKindSolutionExplorer);
        }

        public static SolutionClass GetSolutionClass(this DTE2 dte)
        {
            return ((SolutionClass) (dte.Solution));
        }

        public static UIHierarchyItem SelectProject(this DTE2 dte, Project project)
        {
            var solutionName = dte.GetSolutionClass().GetName();

            var solutionHierarchy = dte.GetSolutionExplorer().GetUIHierarchy();

            var projPath = solutionName + "\\" + project.Name;
            var uiHierarchyItem = solutionHierarchy.GetItem(projPath);

            uiHierarchyItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
            return uiHierarchyItem;
        }

        public static UIHierarchyItem SelectProjectItem(this DTE2 dte, ProjectItem projectItem)
        {
            var solutionName = dte.GetSolutionClass().GetName();

            var solutionHierarchy = dte.GetSolutionExplorer().GetUIHierarchy();

            var projPath = solutionName + "\\" + projectItem.Name;
            var uiHierarchyItem = solutionHierarchy.GetItem(projPath);

            uiHierarchyItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
            return uiHierarchyItem;
        }
    }
}