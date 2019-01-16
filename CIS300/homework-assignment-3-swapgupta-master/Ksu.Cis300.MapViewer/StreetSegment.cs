using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.MapViewer
{
    public struct StreetSegment
    {
        private PointF _start; //one end of street
        private PointF _end; //the other end of the street
        private Pen _pen; //the line to be drawn
        private int _visibleLevels; //What levels the street is visible

        /// <summary>
        /// Used to get, set endpoint
        /// </summary>
        public PointF End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = End;
            }
        }

        /// <summary>
        /// Used to get, set startpoint
        /// </summary>
        public PointF Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = Start;
            }
        }

        /// <summary>
        /// Used to get visible levels
        /// </summary>
        public int VisibleLevels
        {
            get
            {
                return _visibleLevels;
            }
        }

        /// <summary>
        /// The construct for a street segment
        /// </summary>
        /// <param name="start">the start point</param>
        /// <param name="end">the end point</param>
        /// <param name="color">The color of the line</param>
        /// <param name="lineWidth">The width of the line</param>
        /// <param name="visibleLevels">Levels that the line is visible</param>
        public StreetSegment(PointF start, PointF end, Color color, float lineWidth, int visibleLevels)
        {
            _start = start;
            _end = end;
            _visibleLevels = visibleLevels;
            _pen = new Pen(color, lineWidth);
        }

        /// <summary>
        /// Used to draw the street segment
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="scaleFactor"></param>
        public void Draw(Graphics graphics, int scaleFactor)
        {
            graphics.DrawLine(_pen, _start.X * scaleFactor, _start.Y * scaleFactor, _end.X * scaleFactor, _end.Y * scaleFactor);
        }
    }
}
