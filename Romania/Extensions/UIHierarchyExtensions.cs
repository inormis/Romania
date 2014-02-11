using EnvDTE;

namespace Romania.Extensions
{
    public static class UIHierarchyExtensions
    {
        public static void SelectProject(this UIHierarchy hierarchy, Project project)
        {
            var solutionName = project.DTE.Solution.Item("Name");
            var projPath = solutionName + "\\" + project.Name;
            var obj = hierarchy.GetItem(projPath);
            obj.Select(vsUISelectionType.vsUISelectionTypeSelect);
        }


        public static UIHierarchyItem GetRootNode(this UIHierarchy window)
        {
            return window.UIHierarchyItems.Item(1);
        }
    }
}