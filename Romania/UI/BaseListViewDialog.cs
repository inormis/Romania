using System;
using System.Windows.Forms;

namespace Romania.UI
{
    public abstract class BaseListViewDialog<T> : Form
    {
        private readonly Action<T> _onItemSelected;

        protected BaseListViewDialog(Action<T> onItemSelected)
        {
            _onItemSelected = onItemSelected;
            StartPosition=FormStartPosition.CenterScreen;
        }

        protected abstract ListView ListView { get; }

        private bool ItemIsSelected
        {
            get { return ListView.SelectedItems.Count > 0 && ListView.SelectedItems[0].Tag is T; }
        }

        private T SelectedItem
        {
            get { return (T) ListView.SelectedItems[0].Tag; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ListView.KeyDown += OnListViewKeyDown;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            Close();
        }

        private void OnListViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ItemIsSelected)
                {
                    _onItemSelected(SelectedItem);
                    Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}