using System;
using EnvDTE80;

namespace Romania.Commands
{
    public class LoadAllProjectsCommand : ICommand
    {
        public LoadAllProjectsCommand(DTE2 dte)
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
            throw new NotImplementedException();
        }
    }
}