using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class UserInterface : Form
    {
        int _initalScale = 10; //the inital scale
        Map uxMap; //the Map to be used

        /// <summary>
        /// Used to read a csv file
        /// </summary>
        /// <param name="fileName">the name of the file</param>
        /// <param name="bounds">output a rectangle from the first line of the file</param>
        /// <returns></returns>
        private static List<StreetSegment> ReadFile(string fileName, out RectangleF bounds)
        {
            List<StreetSegment> map = new List<StreetSegment>();
            using (StreamReader input = new StreamReader(fileName))
            {
                string[] rect = input.ReadLine().Split(',');
                bounds = new RectangleF(0, 0, Convert.ToSingle(rect[0]), Convert.ToSingle(rect[1]));
                while (!input.EndOfStream)
                {
                    string[] para = input.ReadLine().Split(',');
                    PointF start = new PointF(Convert.ToSingle(para[0]), Convert.ToSingle(para[1]));
                    PointF end = new PointF(Convert.ToSingle(para[2]), Convert.ToSingle(para[3]));
                    StreetSegment street = new StreetSegment(start, end, Color.FromArgb(Convert.ToInt32(para[4])), Convert.ToSingle(para[5]), Convert.ToInt32(para[6]));
                    map.Add(street);
                }
                return map;
            }
        }

        /// <summary>
        /// Used to Initialize the component
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The event handler for Open Map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpenMap_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK) //if OK on Open Dialog is clicked
            {
                try
                {
                    RectangleF bounds;
                    List<StreetSegment> streetList = ReadFile(uxOpenDialog.FileName, out bounds);
                    uxMap = new Map(streetList, bounds, _initalScale);
                    uxMapContainer.Controls.Clear();
                    uxMapContainer.Controls.Add(uxMap);
                    uxZoomIn.Enabled = true;
                    uxZoomOut.Enabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }


        /// <summary>
        /// The event handler for Zoom In
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            Point zPoint = new Point(-uxMapContainer.AutoScrollPosition.X, -uxMapContainer.AutoScrollPosition.Y);
            uxMap.ZoomIn();
            if (!uxMap.canZoomIn)
            {
                uxZoomIn.Enabled = false;
            }
            if (uxMap.canZoomOut)
            {
                uxZoomOut.Enabled = true;
            }
            uxMapContainer.AutoScrollPosition = new Point(-(2 * zPoint.X + (uxMapContainer.ClientSize.Width)/2), -(2 * zPoint.Y + (uxMapContainer.ClientSize.Height)/2));
        }

        /// <summary>
        /// The event handler for Zoom Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            Point zPoint = new Point(-uxMapContainer.AutoScrollPosition.X, -uxMapContainer.AutoScrollPosition.Y);
            uxMap.ZoomOut();
            if (!uxMap.canZoomOut)
            {
                uxZoomOut.Enabled = false;
            }
            if (uxMap.canZoomIn)
            {
                uxZoomIn.Enabled = true;
            }
            uxMapContainer.AutoScrollPosition = new Point(-(zPoint.X / 2 + (uxMapContainer.ClientSize.Width) / 4), -(zPoint.Y / 2 + (uxMapContainer.ClientSize.Height) / 4));
        }
    }
}
