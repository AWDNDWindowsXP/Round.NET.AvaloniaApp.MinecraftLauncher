using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RMCL.Base.Entry.Assets.Center;

namespace RMCL.Controls.Item.AssetsItem;

public partial class AssetsCenterItem : UserControl
{
    private AssetsIndexItemEntry _assetsIndexItem;
    public Action<AssetsIndexItemEntry> DownloadButtonClicked { get; set; }
    public AssetsCenterItem()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        DownloadButtonClicked.Invoke(_assetsIndexItem);
    }

    public void LoadShow(AssetsIndexItemEntry item)
    {
        _assetsIndexItem = item;
        AssetsName.Text = item.Name;
        AssetsProfile.Text = item.Description;
    }
}