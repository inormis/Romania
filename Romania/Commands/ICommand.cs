using EnvDTE;
using EnvDTE80;

namespace Romania.Commands
{
    public abstract class ICommand
    {
        protected ICommand(DTE2 dte)
        {
            Dte = dte;
        }

        protected DTE2 Dte { get; private set; }


        public abstract string Text { get; }

        public virtual string Tooltip
        {
            get { return Text; }
        }

        public string GetAddInCommandName(AddIn addIn)
        {
            return addIn.ProgID + "." + GetSpecificName();
        }

        public abstract string GetSpecificName();

        public abstract void Execute();
    }
}