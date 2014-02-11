using EnvDTE;

namespace Romania.Commands
{
    public static class UIHierarchyItemExtensions
    {
        public static bool HasChildren(this UIHierarchyItem item)
        {
            return item.UIHierarchyItems.Count > 0;
        }
    }
}