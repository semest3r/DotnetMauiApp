namespace DotnetMauiApp.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage()
	{
		InitializeComponent();
		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
		LoadNote(Path.Combine(appDataPath, randomFileName));
	}

	private void LoadNote(string filename)
	{
		Models.Product product = new Models.Product();
		product.FileName = filename;
		if (File.Exists(filename))
		{
			product.Date = File.GetCreationTime(filename);
			product.Name = File.ReadAllText(filename);
		}
		BindingContext = product;
	}

	private void SaveButton_Clicked(object sender, EventArgs e)
	{

	}

	private void DeleteButton_Clicked(object sender, EventArgs e)
	{

	}
}