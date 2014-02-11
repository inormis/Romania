using EnvDTE;
using EnvDTE80;
using Romania.Extensions;

namespace Romania.Commands
{
    public class CollapseAllAdvancedCommand : ICommand
    {
        public CollapseAllAdvancedCommand(DTE2 dte)
            : base(dte)
        {
        }

        public override string Text
        {
            get { return "Collapse all"; }
        }

        public override string Tooltip
        {
            get { return "Collapse all items in solution explorer recursively"; }
        }

        public override string GetSpecificName()
        {
            return "CollapseAllAdvancedCommand";
        }

        public override void Execute()
        {
            var solutionExplorer = Dte.GetSolutionExplorer();
            solutionExplorer.Activate();


            var solutionHierarchy = Dte.GetSolutionExplorer().GetUIHierarchy();
            if (solutionHierarchy.UIHierarchyItems.Count == 0)
                return;
            var rootNode = solutionHierarchy.GetRootNode();
            Dte.SuppressUI = true;
            CollapseWithChildren(rootNode);
            Dte.SuppressUI = false;
        }

        private void CollapseWithChildren(UIHierarchyItem rootNode)
        {
            foreach (UIHierarchyItem childItem in rootNode.UIHierarchyItems)
            {
                if (childItem.HasChildren())
                    CollapseWithChildren(childItem);

                if (childItem.UIHierarchyItems.Expanded)
                {
                    childItem.UIHierarchyItems.Expanded = false;
                }
            }
        }
    }
}