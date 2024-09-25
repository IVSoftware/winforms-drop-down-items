using System.Runtime.ExceptionServices;

namespace WinformsDropDownItems
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ContextMenuStrip = RightClickWindow;

            var InsertAbove = new ToolStripMenuItem { Text = "InsertAbove", AutoSize = true };
            var InsertBelow = new ToolStripMenuItem { Text = "InsertBelow", AutoSize = true };
            foreach (var tsmi in new[] { InsertAbove, InsertBelow })
            {
                tsmi.DropDownItems.Add(new ToolStripMenuItem());    // Placeholder
                tsmi.DropDownOpening += (sender, e) =>
                {
                    tsmi.DropDownItems.Clear();
                    tsmi.DropDownItems.AddRange(CommonItems);
                };
                tsmi.DropDownClosed += (sender, e) =>
                {
                    tsmi.DropDownItems.Clear();
                    tsmi.DropDownItems.Add(new ToolStripMenuItem()); // Placeholder
                };
                RightClickWindow.Items.Add(tsmi);
            }
        }
        ToolStripItem[] CommonItems
        {
            get
            {
                if (_commonItemsSingleton is null)
                {
                    var clickableItem = new ToolStripMenuItem
                    {
                        Text = "Simple click"
                    };
                    clickableItem.Click += SimpleClick_Clicked;

                    var comboBoxItem = new ToolStripComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                    };

                    comboBoxItem.Items.AddRange(new[]
                    {
                        "Select",
                        "Item A",
                        "Item B",
                        "Item C",
                    });

                    comboBoxItem.SelectedIndex = 0;
                    comboBoxItem.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                    comboBoxItem.SelectedIndexChanged += async (sender, e) =>{ await Task.Delay(10); ContextMenuStrip?.Close(); }; 
                    _commonItemsSingleton = new ToolStripItem[]
                    {
                        clickableItem,
                        new ToolStripSeparator(),
                        comboBoxItem,
                    };
                }
                return _commonItemsSingleton;
            }
        }
        ToolStripItem[]? _commonItemsSingleton = default;

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e){ }
            // MessageBox.Show($"{(sender as ToolStripItem)?.Text}");

        private void SimpleClick_Clicked(object? sender, EventArgs e) =>
            MessageBox.Show($"{(sender as ToolStripItem)?.Text}");
    }
}
