using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using Romania.Extensions;

namespace Romania.UI
{
    public partial class ChooseProjectItemDialog : BaseListViewDialog<ProjectItem>
    {
        public ChooseProjectItemDialog(ICollection<ProjectItem> items, Action<ProjectItem> onItemSelected, Solution solution)
            : base(onItemSelected)

        {
            InitializeComponent();

            foreach (var project in items)
            {
                var fileName = project.GetPath(solution);
                listView.Items.Add(new ListViewItem(new[] {project.Name, fileName})).Tag = project;
            }
            if (items.Any())
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