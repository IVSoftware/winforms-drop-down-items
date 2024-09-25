This might be trickier than it sounds. The easy part is making the singleton collection of `ToolStripItem`:

```
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
            comboBoxItem.SelectedIndexChanged += async (sender, e) =>{ await Task.Delay(10); ContextMenuStrip?.Close(); };
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
```
___

But as you've observed:
> Win form seems not to let me have the same exact items in different right click window items.

Since each of these items can only have _one `Parent` at a time_, you can't just `AddRange` to both `InsertAbove` _and_ `InsertBelow`. You have to game the system a little bit by loading them on-demand when  `InsertAbove` _or_ `InsertBelow` dynamically raise the `DropDownOpening` event.

```
public MainForm()
{
    InitializeComponent();
    ContextMenuStrip = RightClickWindow;

    var InsertAbove = new ToolStripMenuItem { Text = "InsertAbove", AutoSize = true };
    var InsertBelow = new ToolStripMenuItem { Text = "InsertBelow", AutoSize = true };
    foreach (var tsmi in new[] { InsertAbove, InsertBelow })
    {
        tsmi.DropDownItems.Add(new ToolStripMenuItem());    // Placeholder ensures ► visible
        tsmi.DropDownOpening += (sender, e) =>
        {
            tsmi.DropDownItems.Clear();
            tsmi.DropDownItems.AddRange(CommonItems);
        };
        tsmi.DropDownClosed += (sender, e) =>
        {
            tsmi.DropDownItems.Clear();
            tsmi.DropDownItems.Add(new ToolStripMenuItem()); // Again, Placeholder ensures ► visible
        };
        RightClickWindow.Items.Add(tsmi);
    }
}
```

[![showing identical item collections][1]][1]


  [1]: https://i.sstatic.net/zYNGKr5n.png