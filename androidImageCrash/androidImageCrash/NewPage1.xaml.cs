using Android.Graphics;
using AndroidX.ConstraintLayout.Core.Widgets.Analyzer;
using Java.Nio.FileNio;
using static Android.Provider.SyncStateContract;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace androidImageCrash;

public partial class NewPage1 : ContentPage
{
    bool firstAppeard = false;

    public NewPage1()
    {
        InitializeComponent();
        this.Appearing += NewPage1_Appearing;
    }

    private async void NewPage1_Appearing(object sender, EventArgs e)
    {
        if (firstAppeard) return;
        firstAppeard = true;
        List<Item> source = new List<Item>();
        var folder = Directory.CreateDirectory(System.IO.Path.Combine(FileSystem.AppDataDirectory, "imagezs"));
        var files = folder.GetFiles();
        var count = files.Count();
        for (int i = count; i < 1000; i++)
        {
            var file = System.IO.Path.Combine(folder.FullName, Guid.NewGuid().ToString("N") + ".png");
            var fileData = GetStream(GetBitmapForResourceId(Resource.Drawable.dotnet_bot));

            await File.WriteAllBytesAsync(file, fileData.ToArray());

        }

        foreach(var f in folder.GetFiles())
            source.Add(new Item(ImageSource.FromFile(f.FullName)));


        ListView.ItemsSource = source;
    }

    public static MemoryStream GetStream(Bitmap bitmap)
    {
        var stream = new MemoryStream();
        bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public static Bitmap GetBitmapForResourceId(int resourceId)
    {
        var bitmap = BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, resourceId);
        return bitmap;
    }


    public class Item
    {
        public ImageSource ImageSource { get; private set; }

        public Item(ImageSource imageSource)
        {
            ImageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));
        }
    }
}