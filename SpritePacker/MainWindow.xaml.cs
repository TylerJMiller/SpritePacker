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

namespace SpritePacker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		SpriteSheet CanvasManager;
		public MainWindow()
		{
			InitializeComponent();
			CanvasManager = new SpriteSheet(ViewCanvas);
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
				catch (System.NotSupportedException FUCKTHEUSER)
				{
					Console.WriteLine("UNHANDLED EXCEPTION: NOT A VALID IMAGE FILE: ", FUCKTHEUSER.Message);
				}
			}
		}
		

		private void MoveToCanvas_Click(object sender, RoutedEventArgs e)
		{
			CanvasManager.AddImage(PreviewImage.Source.ToString(), 0, 0);
		}
	}
}
