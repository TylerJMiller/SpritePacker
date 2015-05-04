using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpritePacker
{
	class CanvasImage
	{
		public int mWidth;
		public int mHeight;
		public int mX;
		public int mY;

		public CanvasImage(double tWidth, double tHeight, int tX, int tY)
		{
			mWidth = (int)tWidth;
			mHeight = (int)tHeight;
			mX = tX;
			mY = tY;
		}
	}
}
