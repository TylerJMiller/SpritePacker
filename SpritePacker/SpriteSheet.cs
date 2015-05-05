﻿using System;
using System.IO;
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
		public int mDrawX;
		public int mDrawY;
		public SpriteSheet(Canvas tCanvas)
		{
			mCanvas = tCanvas;
			mCanvasImages = new List<CanvasImage>();
		}

		public void AddImage(string tImageSource)
		{
			Image tImage = new Image();
			tImage.Source = new BitmapImage(new Uri(tImageSource));
			Canvas.SetTop(tImage, mDrawY);
			Canvas.SetLeft(tImage, mDrawX);
			mCanvas.Children.Add(tImage);
			mCanvasImages.Add(new CanvasImage(tImage.Width, tImage.Height, mDrawX, mDrawY));
			mDrawX += (int)tImage.Source.Width;
			//mDrawY += (int)tImage.Source.Height;
		}

		public void ExportCanvas(string tTargetDir)
		{
			Transform tTransform = mCanvas.LayoutTransform;
			Size tSize = new Size(mCanvas.Width, mCanvas.Height);
			mCanvas.Measure(tSize);
			mCanvas.Arrange(new Rect(tSize));
			RenderTargetBitmap tRenderBitmap = new RenderTargetBitmap((int)tSize.Width, (int)tSize.Height, 96d, 96d, PixelFormats.Pbgra32);
			tRenderBitmap.Render(mCanvas);
			using (FileStream tOutStream = new FileStream(tTargetDir, FileMode.Create))
			{
				PngBitmapEncoder tEncoder = new PngBitmapEncoder();
				tEncoder.Frames.Add(BitmapFrame.Create(tRenderBitmap));
				tEncoder.Save(tOutStream);
			}
			mCanvas.LayoutTransform = tTransform;
		}
	}
}
