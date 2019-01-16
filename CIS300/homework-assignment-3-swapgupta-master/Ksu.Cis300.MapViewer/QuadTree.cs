using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.MapViewer
{
    class QuadTree
    {
        private QuadTree _southwestChild; //southwest tree
        private QuadTree _southeastChild; //southeast tree
        private QuadTree _northwestChild; //northwest tree
        private QuadTree _northeastChild; //northeast tree
        private RectangleF _bounds; //the bounds of the map
        private List<StreetSegment> _streets; //the current list of streets

        /// <summary>
        /// Used to draw the lines
        /// </summary>
        /// <param name="graphics">The actual graphic</param>
        /// <param name="scaleFactor">Used to scale up and down</param>
        /// <param name="maxZoom">The top level of zoom</param>
        public void Draw(Graphics graphics, int scaleFactor, int maxZoom)
        {
            RectangleF bounds = graphics.ClipBounds;
            bounds.X = bounds.X / scaleFactor;
            bounds.Y = bounds.Y / scaleFactor;
            bounds.Height = bounds.Height / scaleFactor;
            bounds.Width = bounds.Width / scaleFactor;

            if (bounds.IntersectsWith(_bounds))
            {
                foreach(StreetSegment s in _streets)
                {
                    s.Draw(graphics, scaleFactor);
                }

                if (maxZoom > 0)
                {
                    maxZoom -= 1;
                    _northeastChild.Draw(graphics, scaleFactor, maxZoom);
                    _northwestChild.Draw(graphics, scaleFactor, maxZoom);
                    _southeastChild.Draw(graphics, scaleFactor, maxZoom);
                    _southwestChild.Draw(graphics, scaleFactor, maxZoom);
                }
            }
        }

        /// <summary>
        /// The constructor for quadtree
        /// </summary>
        /// <param name="s">The list to make a map</param>
        /// <param name="area">the bounds of the map</param>
        /// <param name="height">The height of zoom</param>
        public QuadTree(List<StreetSegment> s, RectangleF area, int height)
        {
            _bounds = area;
            if (height == 0)
            {
                _streets = s;
            }
            else
            {
                _streets = new List<StreetSegment>();
                List<StreetSegment> invisible = new List<StreetSegment>();
                SplitHeights(s, height, _streets, invisible);
                List<StreetSegment> east = new List<StreetSegment>();
                List<StreetSegment> west = new List<StreetSegment>();


                SplitEastWest(invisible, (_bounds.Width / 2) + _bounds.Left, west, east);

                List<StreetSegment> northeast = new List<StreetSegment>();
                List<StreetSegment> northwest = new List<StreetSegment>();
                List<StreetSegment> southeast = new List<StreetSegment>();
                List<StreetSegment> southwest = new List<StreetSegment>();
                SplitNorthSouth(east, (_bounds.Height) / 2 + _bounds.Top, northeast, southeast);
                SplitNorthSouth(west, (_bounds.Height) / 2 + _bounds.Top, northwest, southwest);

                //_southwestChild = new QuadTree(southwest, new RectangleF((_bounds.Height) / 2 + _bounds.Top, (_bounds.Width / 2) + _bounds.Left, _bounds.Width / 2, _bounds.Height / 2), height - 1);
                //_southeastChild = new QuadTree(southeast, new RectangleF((_bounds.Height) / 2 + _bounds.Top, _bounds.Y, _bounds.Width / 2, _bounds.Height / 2), height - 1);
                //_northwestChild = new QuadTree(northwest, new RectangleF(_bounds.X, (_bounds.Width/2) + _bounds.Left, _bounds.Width / 2, _bounds.Height / 2), height - 1);
                //_northeastChild = new QuadTree(northeast, new RectangleF(_bounds.X, _bounds.Y, _bounds.Width/2, _bounds.Height/2), height - 1);

                _southwestChild = new QuadTree(southwest, _bounds, height - 1);
                _southeastChild = new QuadTree(southeast, _bounds, height - 1);
                _northwestChild = new QuadTree(northwest, _bounds, height - 1);
                _northeastChild = new QuadTree(northeast, _bounds, height - 1);
            }
        }

        /// <summary>
        /// Splits east and west lines
        /// </summary>
        /// <param name="splitStreet">the list to split</param>
        /// <param name="x">the point to seperate east and west</param>
        /// <param name="westStreets">a list of west streets</param>
        /// <param name="eastStreets">a list of east streets</param>
        private static void SplitEastWest(List<StreetSegment> splitStreet, float x, List<StreetSegment> westStreets, List<StreetSegment> eastStreets)
        {
            foreach (StreetSegment s in splitStreet)
            {
                if(s.Start.X <= x & s.End.X <= x)
                {
                    westStreets.Add(s);
                }
                else if(s.Start.X >= x & s.End.X >= x)
                {
                    eastStreets.Add(s);
                }
                else
                {
                    StreetSegment sCopy1 = s;
                    StreetSegment sCopy2 = s;
                    float x1 = s.Start.X;
                    float y1 = s.Start.Y;
                    float x2 = s.End.X;
                    float y2 = s.End.Y;
                    float y = (((y2 - y1) * (x - x1))/(x2 - x1)) + y1;
                    
                    sCopy1.End = new PointF(x, y);
                    sCopy2.Start = new PointF(x, y);
                    if(sCopy1.Start.X >= x)
                    {
                        eastStreets.Add(sCopy1);
                        westStreets.Add(sCopy2);
                    }
                    else
                    {
                        eastStreets.Add(sCopy2);
                        westStreets.Add(sCopy1);
                    }
                }
            }
        }
        /// <summary>
        /// Splits based on height
        /// </summary>
        /// <param name="splitStreet">streets to split</param>
        /// <param name="height">point to split</param>
        /// <param name="currentlyVisible">visible streets</param>
        /// <param name="currentlyInvisible">invisible streets</param>
        private static void SplitHeights(List<StreetSegment> splitStreet, int height, List<StreetSegment> currentlyVisible, List<StreetSegment> currentlyInvisible)
        {
            foreach(StreetSegment s in splitStreet)
            {
                if (s.VisibleLevels > height)
                {
                    currentlyVisible.Add(s);
                }
                else currentlyInvisible.Add(s);
            }
        }

        /// <summary>
        /// splits north and south
        /// </summary>
        /// <param name="splitStreet">streets to split</param>
        /// <param name="y">point to split at</param>
        /// <param name="northStreets">north streets</param>
        /// <param name="southStreets">south streets</param>
        private static void SplitNorthSouth(List<StreetSegment> splitStreet, float y, List<StreetSegment> northStreets, List<StreetSegment> southStreets)
        {
            foreach (StreetSegment s in splitStreet)
            {
                if (s.Start.Y <= y & s.End.Y <= y)
                {
                    northStreets.Add(s);
                }
                else if (s.Start.Y >= y & s.End.Y >= y)
                {
                    southStreets.Add(s);
                }
                else
                {
                    StreetSegment sCopy1 = s;
                    StreetSegment sCopy2 = s;
                    float x1 = s.Start.X;
                    float y1 = s.Start.Y;
                    float x2 = s.End.X;
                    float y2 = s.End.Y;
                    float x = (((x2 - x1) * (y - y1)) / (y2 - y1)) + x1;

                    sCopy1.End = new PointF(x, y);
                    sCopy2.Start = new PointF(x, y);
                    if (sCopy1.Start.X >= x)
                    {
                        southStreets.Add(sCopy1);
                        northStreets.Add(sCopy2);
                    }
                    else
                    {
                        southStreets.Add(sCopy2);
                        northStreets.Add(sCopy1);
                    }
                }
            }
        }
    }
}
