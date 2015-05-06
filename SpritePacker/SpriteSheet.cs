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
		Employee[] employees = new Employee[4];
		public List<CanvasImage> mCanvasImages;
		public Canvas mCanvas;
		public int mDrawX;
		public int mDrawY;
		public SpriteSheet(Canvas tCanvas)
		{
			mCanvas = tCanvas;
			//mCanvas.Width = 500;
			//mCanvas.Height = 500;
			mCanvasImages = new List<CanvasImage>();
		}

		public void AddImage(string tImageSource)
		{

			Image tImage = new Image();
			tImage.Source = new BitmapImage(new Uri(tImageSource));
			if (mDrawX + (int)tImage.Source.Width > mCanvas.Width)
			{
				mDrawX = 0;
				mDrawY += (int)tImage.Source.Height;
			}
			if (mDrawY + (int)tImage.Source.Height > mCanvas.Height)
			{
				Console.WriteLine("Failed to add image: Canvas is full");
				return;
			}
			Canvas.SetTop(tImage, mDrawY);
			Canvas.SetLeft(tImage, mDrawX);
			mCanvas.Children.Add(tImage);
			mCanvasImages.Add(new CanvasImage(tImage.Source.Width, tImage.Source.Height, mDrawX, mDrawY));
			Console.WriteLine("Image Added");
			mDrawX += (int)tImage.Source.Width;
			//mDrawY += (int)tImage.Source.Height;
		}

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

				/*int tCount = tOutStream.Name.Count();
				int tStartIndex = tOutStream.Name.LastIndexOf("\\") + 1;
				int tSubLength = tOutStream.Name.Count() - (tOutStream.Name.LastIndexOf("\\") + 1) - 4;
				string testa = tOutStream.Name.Substring(tStartIndex,tSubLength) +".xml";
				*/

				/*
				XmlWriterSettings tXMLSettings = new XmlWriterSettings();
				tXMLSettings.Indent = true;
				//tXMLSettings.OutputMethod 
				using (XmlWriter tXML = XmlWriter.Create(tOutStream.Name.Substring(tOutStream.Name.LastIndexOf("\\") + 1, tOutStream.Name.Count() - 3) + ".xml", tXMLSettings))
				{
					tXML.WriteStartDocument();
					tXML.WriteStartElement("SpriteSheet");
					foreach (CanvasImage tCanvasImage in mCanvasImages)
					{
						tXML.WriteStartElement("Sprite");
						tXML.WriteElementString("X", tCanvasImage.mX.ToString());
						tXML.WriteElementString("Y", tCanvasImage.mY.ToString());
						tXML.WriteElementString("Width", tCanvasImage.mWidth.ToString());
						tXML.WriteElementString("Height", tCanvasImage.mHeight.ToString());
						tXML.WriteEndElement();
					}
					tXML.WriteEndElement();
					tXML.WriteEndDocument();
				}
				*/
			}
			mCanvas.LayoutTransform = tTransform;
			


			XmlWriterSettings tXMLSettings = new XmlWriterSettings();
			tXMLSettings.Indent = true;
			//tXMLSettings.OutputMethod 
			//using (XmlWriter tXML = XmlWriter.Create(tFileName.Substring(tFileName.LastIndexOf("\\") + 1, tFileName.Count() - 3) + ".xml", tXMLSettings))
			using (XmlWriter tXML = XmlWriter.Create(tFileFull.Substring(0, tFileFull.LastIndexOf(".")) + ".xml", tXMLSettings))
			{
				//string testc = tFileFull.Substring(0, tFileFull.LastIndexOf(".")) + ".xml";

				tXML.WriteStartDocument();
				tXML.WriteStartElement("SpriteSheet");
				tXML.WriteAttributeString("Filename", tFileName + tFileExt);
				foreach (CanvasImage tCanvasImage in mCanvasImages)
				{
					tXML.WriteStartElement("Sprite");
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
