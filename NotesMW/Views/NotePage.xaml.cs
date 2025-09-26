namespace NotesMW.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	string _filename = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
	public string ItemId
	{
		set => LoadNote(value)
	}
	public NotePage()
	{
		InitializeComponent();

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
		LoadNote(Path.Combine(appDataPath, randomFileName));
	}
	private void LoadNote(string fileName)
	{
		Models.Note noteModel = new();
		noteModel.Filename = fileName;
		if(File.Exists(fileName))
		{
			noteModel.Text = File.ReadAllText(fileName);
			noteModel.Date = File.GetCreationTime(fileName);
		}
		BindingContext = noteModel;
	}
	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
			File.WriteAllText(note.Filename, TextEditor.Text);
		await Shell.Current.GoToAsync("..");
		TextEditor.Text = string.Empty;
	}
}