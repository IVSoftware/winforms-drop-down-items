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
                tsmi.DropDownClosed += async (sender, e) =>
                {
                    // This delay ensures that the handler will still have
                    // access to the OwnerItem. We'll probably want to know
                    // whether the call was made by InsertAbove or InsertBelow
                    await Task.Delay(TimeSpan.FromSeconds(0.5));
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
                    comboBoxItem.SelectedIndexChanged += async (sender, e) =>
                    { 
                        await Task.Delay(TimeSpan.FromSeconds(0.5)); ContextMenuStrip?.Close();
                    }; 
                    comboBoxItem.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
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

        private void SimpleClick_Clicked(object? sender, EventArgs e)
        {
            if (sender is ToolStripItem tsi)
            {
                MessageBox.Show(
                    text: tsi.Text, caption: $"Called by {tsi.OwnerItem?.Text}");
            }
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is ToolStripItem tsi)
            {
                MessageBox.Show(
                    text: tsi.Text, caption: $"Called by {tsi.OwnerItem?.Text}");
            }
        }
    }
}
