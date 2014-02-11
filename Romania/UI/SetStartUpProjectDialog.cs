using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;

namespace Romania.UI
{
    public partial class SetStartUpProjectDialog : BaseListViewDialog<Project>
    {

        public SetStartUpProjectDialog(ICollection<Project> projects, Action<Project> setStartUpProject)
            : base(setStartUpProject)
        {
            InitializeComponent();

            foreach (var project in projects)
            {
                listView.Items.Add(project.Name).Tag = project;
            }
            if (projects.Any())
            {
                listView.Items[0].Selected = true;
                listView.Select();
            }
        }

        protected override ListView ListView
        {
            get { return listView; }
        }
    }
}