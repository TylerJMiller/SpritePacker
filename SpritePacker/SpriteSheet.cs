using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace SpritePacker
{
	class Employee
	{
		public int _id;
	}

	class SpriteSheet
	{
		List<CanvasImage> mCanvasImages;
		Canvas mCanvas;
		int mDrawX;
		int mDrawY;
		int mNextLineY;

		public SpriteSheet(Canvas tCanvas)
		{
			mCanvas = tCanvas;
			//mCanvas.Width = 500;
			//mCanvas.Height = 500;
			mCanvasImages = new List<CanvasImage>();
		}

		//checks if given image can fit on canvas
		//if not true then outputs error and returns
		//if true then adds image to canvas and adds new CanvasImage to mCanvasImages list
		public void AddImage(string tImageSource)	
		{

			Image tImage = new Image();
			tImage.Source = new BitmapImage(new Uri(tImageSource));

			if (mDrawY + (int)tImage.Source.Height > mCanvas.Height)
			{
				Console.WriteLine("Failed to add image: Canvas is full");
				return;
			}
			if ((int)tImage.Source.Height + mDrawY > mNextLineY)
			{
				mNextLineY = mDrawY + (int)tImage.Source.Height;
			}

			if (mDrawX + (int)tImage.Source.Width > mCanvas.Width)
			{
				mDrawX = 0;
				mDrawY = mNextLineY;
				mNextLineY = 0;
			}
			if (mDrawY + (int)tImage.Source.Height > mCanvas.Height)
			{
				Console.WriteLine("Failed to add image: Canvas is full");
				return;
			}
			Canvas.SetTop(tImage, mDrawY);
			Canvas.SetLeft(tImage, mDrawX);
			mCanvas.Children.Add(tImage);
			string tFileName = tImageSource.Substring(tImageSource.LastIndexOf("/") + 1, tImageSource.LastIndexOf(".") - tImageSource.LastIndexOf("/") - 1);
			mCanvasImages.Add(new CanvasImage(tImageSource.Substring(tImageSource.LastIndexOf("/") + 1, tImageSource.LastIndexOf(".") - tImageSource.LastIndexOf("/") - 1), tImage.Source.Width, tImage.Source.Height, mDrawX, mDrawY));
			Console.WriteLine("Image Added");
			mDrawX += (int)tImage.Source.Width;
		}

		//outputs canvas as png/bmp to given directory then outputs xml representation to same directory
		public void ExportCanvas(string tFileFull)
		{
			string tFileDir = tFileFull.Substring(0, tFileFull.LastIndexOf("\\") + 1);
			string tFileName = tFileFull.Substring(tFileFull.LastIndexOf("\\") + 1, tFileFull.LastIndexOf(".") - tFileFull.LastIndexOf("\\") - 1);
			string tFileExt = tFileFull.Substring(tFileFull.LastIndexOf("."));

			Transform tTransform = mCanvas.LayoutTransform;
			Size tSize = new Size(mCanvas.Width, mCanvas.Height);
			mCanvas.Measure(tSize);
			mCanvas.Arrange(new Rect(tSize));
			RenderTargetBitmap tRenderBitmap = new RenderTargetBitmap((int)tSize.Width, (int)tSize.Height, 96d, 96d, PixelFormats.Pbgra32);
			tRenderBitmap.Render(mCanvas);
			using (FileStream tOutStream = new FileStream(tFileFull, FileMode.Create))
			{
				PngBitmapEncoder tEncoder = new PngBitmapEncoder();
				tEncoder.Frames.Add(BitmapFrame.Create(tRenderBitmap));
				tEncoder.Save(tOutStream);
				Console.WriteLine("Saved " + tOutStream.Name.Substring(tOutStream.Name.LastIndexOf("\\") + 1) + " at " + tOutStream.Name.Substring(0, tOutStream.Name.LastIndexOf("\\") + 1));
			}
			mCanvas.LayoutTransform = tTransform;
			


			XmlWriterSettings tXMLSettings = new XmlWriterSettings();
			tXMLSettings.Indent = true;
			using (XmlWriter tXML = XmlWriter.Create(tFileFull.Substring(0, tFileFull.LastIndexOf(".")) + ".xml", tXMLSettings))
			{
				tXML.WriteStartDocument();
				tXML.WriteStartElement("SpriteSheet");
				tXML.WriteAttributeString("Filename", tFileName + tFileExt);
				foreach (CanvasImage tCanvasImage in mCanvasImages)
				{
					tXML.WriteStartElement("Sprite");
					tXML.WriteAttributeString("Name", tCanvasImage.mMemberName);
					tXML.WriteAttributeString("Height", tCanvasImage.mHeight.ToString());
					tXML.WriteAttributeString("Width", tCanvasImage.mWidth.ToString());
					tXML.WriteAttributeString("Y", tCanvasImage.mY.ToString());
					tXML.WriteAttributeString("X", tCanvasImage.mX.ToString());
					tXML.WriteEndElement();
				}
				tXML.WriteEndElement();
				tXML.WriteEndDocument();
			}
		}
	}
}
