using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpritePacker
{
	class CanvasImage
	{
		public string mMemberName;
		public int mWidth;
		public int mHeight;
		public int mX;
		public int mY;

		public CanvasImage(string tMemberName, double tWidth, double tHeight, int tX, int tY)
		{
			mMemberName = tMemberName;
			mWidth = (int)tWidth;
			mHeight = (int)tHeight;
			mX = tX;
			mY = tY;
		}
	}
}