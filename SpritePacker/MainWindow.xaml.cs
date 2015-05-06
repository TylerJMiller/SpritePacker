using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;

namespace SpritePacker
{
	public partial class MainWindow : Window
	{
		SpriteSheet CanvasManager;
		public MainWindow()
		{
			try
			{
				InitializeComponent();
				CanvasManager = new SpriteSheet(ViewCanvas);
			}
			catch (Exception)
			{
				
			}
		}
		private void ImportFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opf = new OpenFileDialog();
			opf.InitialDirectory = "C:\\Users\\tyler.miller\\Desktop" + "\\TESTFOLDER";
			opf.Filter = "ALL FILES (*.*)|*.*|BITMAP IMAGES (*.bmp)|*.bmp|COMPUSERVE GIF (*.gif)|*.gif";
			opf.FilterIndex = 1;
			opf.RestoreDirectory = true;
			bool? userClickedOK = opf.ShowDialog();

			if (userClickedOK == true)
			{
				try
				{
					PreviewImage.Source = new BitmapImage(new Uri(opf.FileName));
				}
				catch (Exception PLEASESTOP)
				{
					Console.WriteLine("UNHANDLED BUTTS: NOT A VALID IMAGE FILE: ", PLEASESTOP.Message);
				}
			}
		}
		


		private void MoveToCanvas_Click(object sender, RoutedEventArgs e)
		{
			if (null != PreviewImage.Source)
				CanvasManager.AddImage(PreviewImage.Source.ToString());
			else
				Console.WriteLine("ERROR: IMAGE SELECT IS NULL");
		}

		private void SaveFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = "C:\\Users\\tyler.miller\\Desktop" + "\\TESTFOLDER";
			sfd.Filter = "BITMAP IMAGE | *.bmp|PNG IMAGE | *.png";
			sfd.DefaultExt = "bmp";

			bool? userClickedSave = sfd.ShowDialog();

			if (userClickedSave == true)
			{
				CanvasManager.ExportCanvas(sfd.FileName);
			}

		}
	}
}
