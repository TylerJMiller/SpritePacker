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
			catch (Exception tFailure)
			{
				Console.WriteLine("MASSIVE FAILURE (this should not happen): " + tFailure.Message);
			}
		}

		//opens a file dialog and takes in one image file
		//checks if image file is valid
		//if not valid then outputs error and leaves image preview null
		//if valid then sets the chosen image as the preview image
		private void ImportFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opf = new OpenFileDialog();
			opf.InitialDirectory = "C:\\Users\\tyler.miller\\Desktop" + "\\TESTFOLDER";
			opf.Filter = "ALL FILES (*.*)|*.*|PNG IMAGES (*.png)|*.png|BITMAP IMAGES (*.bmp)|*.bmp|COMPUSERVE GIF (*.gif)|*.gif";
			opf.FilterIndex = 1;
			opf.RestoreDirectory = true;
			bool? userClickedOK = opf.ShowDialog();
			
			if (userClickedOK == true)
			{
				try
				{
					PreviewImage.Source = new BitmapImage(new Uri(opf.FileName));
					string whatever = PreviewImage.Source.ToString();
				}
				catch (Exception PLEASESTOP)
				{
					Console.WriteLine("UNHANDLED BUTTS: NOT A VALID IMAGE FILE: ", PLEASESTOP.Message);
				}
			}
		}
		

		//if null then outputs an error
		//if not then passes the preview image to the canvas
		private void MoveToCanvas_Click(object sender, RoutedEventArgs e)
		{
			if (null != PreviewImage.Source)
				CanvasManager.AddImage(PreviewImage.Source.ToString());
			else
				Console.WriteLine("ERROR: IMAGE SELECT IS NULL");
		}


		//opens save file dialog then passes the chosen directory to the canvas manager
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

		//opens a multi-select file dialog
		//checks if image file is valid
		//if valid then passes to canvas and moves to next image
		//if not then moves to next image
		private void ImportBatch_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opf = new OpenFileDialog();
			opf.InitialDirectory = "C:\\Users\\tyler.miller\\Desktop" + "\\TESTFOLDER";
			opf.Filter = "ALL FILES (*.*)|*.*|PNG IMAGES (*.png)|*.png|BITMAP IMAGES (*.bmp)|*.bmp|COMPUSERVE GIF (*.gif)|*.gif";
			opf.FilterIndex = 1;
			opf.Multiselect = true;
			opf.RestoreDirectory = true;
			bool? userClickedOK = opf.ShowDialog();

			if (userClickedOK == true)
			{
				foreach (string tFileName in opf.FileNames)
				{
					try
					{
						CanvasManager.AddImage(tFileName);
					}
					catch (Exception PLEASESTOP)
					{
						Console.WriteLine("UNHANDLED BUTTS: NOT A VALID IMAGE FILE: ", PLEASESTOP.Message);
					}
				}
			}

		}
	}
}
