using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class Map : UserControl
    {
        private const int _maxZoom = 6; //the constant level of max zoom
        private int _scale; //the scale factor
        private int _zoom = 0; //the current level of zoom
        private QuadTree _streets; //Contains the map data

        /// <summary>
        /// Can the program zoom in
        /// </summary>
        public bool canZoomIn
        {
            get
            {
                if (_zoom < _maxZoom)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Can the program zoom out
        /// </summary>
        public bool canZoomOut
        {
            get
            {
                if (_zoom > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Is the point in the bounds
        /// </summary>
        /// <param name="point">the point to be checked</param>
        /// <param name="bounds">the outer rectangle</param>
        /// <returns></returns>
        private static bool IsWithinBounds(PointF point, RectangleF bounds)
        {
            if (point.X > bounds.Right | point.X < bounds.Left)
            {
                return false;
            }
            else if (point.Y > bounds.Bottom | point.Y < bounds.Top)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// The construct of Map
        /// </summary>
        /// <param name="map">a list of street segments</param>
        /// <param name="bounds">The bounds of the map</param>
        /// <param name="scale">The current scale</param>
        public Map(List<StreetSegment> map, RectangleF bounds, int scale)
        {
            int count = 0;
            foreach (StreetSegment s in map)
            {
                if (IsWithinBounds(s.Start, bounds) && IsWithinBounds(s.End, bounds))
                {
                    
                }
                else
                {
                    throw new ArgumentException("Street" + count + "is not within the given bounds.");
                }
                count += 1;
            }
            _streets = new QuadTree(map, bounds, _maxZoom);
            _scale = scale;
            Size = new Size((int)bounds.Width * scale, (int)bounds.Height * scale);
            InitializeComponent();
        }

        /// <summary>
        /// Used to redraw the map
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF map = e.ClipRectangle;
            Graphics g = e.Graphics;
            g.Clip = new Region(map);
            _streets.Draw(g, _scale, _zoom);
        }

        /// <summary>
        /// Zooms in and updates scale and zoom
        /// </summary>
        public void ZoomIn()
        {
            if (canZoomIn)
            {
                _zoom += 1;
                _scale = _scale * 2;
                Size = new Size(Size.Width * 2, Size.Height * 2);
                Invalidate();
            }
        }

        /// <summary>
        /// Zooms out and updates scale and zoom
        /// </summary>
        public void ZoomOut()
        {
            _zoom -= 1;
            _scale = _scale / 2;
            Size = new Size(Size.Width / 2, Size.Height / 2);
            Invalidate();
        }
    }
}
