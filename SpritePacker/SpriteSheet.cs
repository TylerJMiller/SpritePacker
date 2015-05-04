using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpritePacker
{
	class SpriteSheet
	{
		public List<CanvasImage> mCanvasImages;
		public Canvas mCanvas;
		public SpriteSheet(Canvas tCanvas)
		{
			mCanvas = tCanvas;
			mCanvasImages = new List<CanvasImage>();
		}

		public void AddImage(string tImageSource, int tX, int tY)
		{
			Image tImage = new Image();
			tImage.Source = new BitmapImage(new Uri(tImageSource));
			Canvas.SetTop(tImage, tX);
			Canvas.SetLeft(tImage, tY);
			mCanvas.Children.Add(tImage);
			mCanvasImages.Add(new CanvasImage(tImage.Width, tImage.Height, tX, tY));
		}
	}
}
