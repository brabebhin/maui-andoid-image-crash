namespace androidImageCrash;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
        var folder = Directory.CreateDirectory(System.IO.Path.Combine(FileSystem.AppDataDirectory, "imagezs"));
		var files = folder.EnumerateFiles();
		if (files.Any())
		{
            image.Source = ImageSource.FromFile(files.First().FullName);
            Image2.Source = ImageSource.FromFile(files.First().FullName);
        }
		else
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
			image.Source = ImageSource.FromFile("dotnet_bot.png");
			Image2.Source = ImageSource.FromFile("dotnet_bot.png");
		}
    }
}

