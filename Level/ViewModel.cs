using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Level
{
    class ViewModel
    {
		public ICommand OpenFileCommand { get; private set; }
		public ICommand SaveFileCommand { get; private set; }
		public ICommand ImportFileCommand { get; private set; }
		public ICommand ExportFileCommand { get; private set; }

		public ViewModel()
		{
			OpenFileCommand = new CommandAction(OpenFile);
			SaveFileCommand = new CommandAction(SaveFile);
			ImportFileCommand = new CommandAction(ImportFile);
			ExportFileCommand = new CommandAction(ExportFile);
		}

		private void Initialize()
		{

		}

		private void OpenFile(Object? parameter)
		{
			var dlg = new OpenFileDialog();

			dlg.Filter = "Level|Level.sav";
			var path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			path = System.IO.Path.Combine(path, @"Pal\Saved\SaveGames");
			dlg.InitialDirectory = path;

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
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}
	}
}
