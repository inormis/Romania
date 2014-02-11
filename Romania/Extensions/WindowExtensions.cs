using EnvDTE;

namespace Romania.Extensions
{
    public static class WindowExtensions
    {
        public static UIHierarchy GetUIHierarchy(this Window window)
        {
            return (UIHierarchy) window.Object;
        }
    }
}