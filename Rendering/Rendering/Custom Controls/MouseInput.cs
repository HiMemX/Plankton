using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Custom_Controls
{

    public class MouseInfo
    {
        int lastx = 0;
        int lasty = 0;

        public int x = 0;
        public int y = 0;

        public float speedx = 0;
        public float speedy = 0;

        public bool middleMouseDown = false;
        public bool leftMouseDown = false;
        public bool rightMouseDown = false;

        public System.Drawing.Point mouseDownPoint = new Point(0,0);
        public System.Drawing.Point currentPoint = new Point(0, 0);
        public bool isBoxSelecting = false;
        public Rectangle lastScreenRectangle = Rectangle.Empty;

        public float scrollSpeed = 0;

        public void SetPosition(int x, int y)
        {
            lastx = this.x;
            lasty = this.y;

            this.x = x;
            this.y = y;

            speedx = x - lastx;
            speedy = y - lasty;
        }
    }

}
