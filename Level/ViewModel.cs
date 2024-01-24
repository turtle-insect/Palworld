using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Level
{
	class ViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand OpenFileCommand { get; private set; }
		public ICommand SaveFileCommand { get; private set; }
		public ICommand ImportFileCommand { get; private set; }
		public ICommand ExportFileCommand { get; private set; }

		public Player? Player { get; private set; }
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Pal> Pals { get; private set; } = new ObservableCollection<Pal>();

		public ViewModel()
		{
			OpenFileCommand = new CommandAction(OpenFile);
			SaveFileCommand = new CommandAction(SaveFile);
			ImportFileCommand = new CommandAction(ImportFile);
			ExportFileCommand = new CommandAction(ExportFile);
		}

		private void Initialize()
		{
			var address_list = SaveData.Instance().FindAddress("PalIndividualCharacterSaveParameter", 0);
			if (address_list.Count > 0)
			{
				Player = new Player(address_list[0]);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player)));
			}

			Pals.Clear();
			for(int index = 1; index < address_list.Count; index++)
			{
				Pals.Add(new Pal(address_list[index]));
			}

			Items.Clear();
			address_list = SaveData.Instance().FindAddress("PalItemSlotSaveData", 0);
			foreach (var address in address_list)
			{
				//Items.Add(new Item(address));
			}
		}

		private void OpenFile(Object? parameter)
		{
			var dlg = new OpenFileDialog();

			dlg.Filter = "Level|Level.sav";
			var path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			path = System.IO.Path.Combine(path, @"Pal\Saved\SaveGames");
			if(System.IO.Directory.Exists(path))
			{
				dlg.InitialDirectory = path;
			}
			if (dlg.ShowDialog() == false) return;
			if (SaveData.Instance().Open(dlg.FileName) == false) return;
			MessageBox.Show("OK");
			Initialize();
		}

		private void SaveFile(Object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void ImportFile(Object? parameter)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Import(dlg.FileName);
		}

		private void ExportFile(Object? parameter)
		{
			var dlg = new SaveFileDialog();
			dlg.Filter = "GVAS|*.gvas";
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}
	}
}
