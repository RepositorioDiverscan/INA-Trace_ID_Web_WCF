using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_Diagrama
    {
        public class Hover
        {
            public double opacity { get; set; }
        }

        public class Fill
        {
            public string color { get; set; }
        }

        public class Content
        {
            public string align { get; set; }
            public string color { get; set; }
            public string text { get; set; }
            public Fill fill { get; set; }
        }

        public class Snap
        {
            public int size { get; set; }
            public int angle { get; set; }
        }

        public class Drag
        {
            public Snap snap { get; set; }
        }

        public class Editable
        {
            public bool connect { get; set; }
            public List<object> tools { get; set; }
            public Drag drag { get; set; }
            public bool remove { get; set; }
        }

        public class Connector
        {
            public string name { get; set; }
        }

        public class Rotation
        {
            public int angle { get; set; }
        }

        public class Stroke
        {
            public int width { get; set; }
            public string color { get; set; }
        }

        public class Fill2
        {
            public string color { get; set; }
        }

        public class Fill3
        {
            public string color { get; set; }
        }

        public class Stroke2
        {
            public string color { get; set; }
        }

        public class Fill4
        {
            public string color { get; set; }
        }

        public class Stroke3
        {
            public string color { get; set; }
        }

        public class Hover2
        {
            public Fill4 fill { get; set; }
            public Stroke3 stroke { get; set; }
        }

        public class ConnectorDefaults
        {
            public Fill3 fill { get; set; }
            public Stroke2 stroke { get; set; }
            public Hover2 hover { get; set; }
        }

        public class Shape
        {
            public string id { get; set; }
            public Hover hover { get; set; }
            public string cursor { get; set; }
            public Content content { get; set; }
            public bool selectable { get; set; }
            public bool serializable { get; set; }
            public bool enable { get; set; }
            public string type { get; set; }
            public string path { get; set; }
            public bool autoSize { get; set; }
            public object visual { get; set; }
            public double x { get; set; }
            public double y { get; set; }
            public int minWidth { get; set; }
            public int minHeight { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public Editable editable { get; set; }
            public List<Connector> connectors { get; set; }
            public Rotation rotation { get; set; }
            public Stroke stroke { get; set; }
            public Fill2 fill { get; set; }
            public ConnectorDefaults connectorDefaults { get; set; }
            public bool undoable { get; set; }
        }

        public class Stroke4
        {
        }

        public class Hover3
        {
            public Stroke4 stroke { get; set; }
        }

        public class Content2
        {
            public string align { get; set; }
            public string color { get; set; }
        }

        public class Stroke5
        {
            public int width { get; set; }
            public string color { get; set; }
        }

        public class Fill5
        {
            public string color { get; set; }
        }

        public class Stroke6
        {
            public string color { get; set; }
        }

        public class Handles
        {
            public int width { get; set; }
            public int height { get; set; }
            public Fill5 fill { get; set; }
            public Stroke6 stroke { get; set; }
        }

        public class Selection
        {
            public Handles handles { get; set; }
        }

        public class Snap2
        {
            public int size { get; set; }
            public int angle { get; set; }
        }

        public class Drag2
        {
            public Snap2 snap { get; set; }
        }

        public class Editable2
        {
            public List<object> tools { get; set; }
            public Drag2 drag { get; set; }
            public bool remove { get; set; }
        }

        public class DataItem
        {
        }

        public class To
        {
            public string shapeId { get; set; }
            public string connector { get; set; }
        }

        public class From
        {
            public string shapeId { get; set; }
            public string connector { get; set; }
        }

        public class Connection
        {
            public string id { get; set; }
            public Hover3 hover { get; set; }
            public string cursor { get; set; }
            public Content2 content { get; set; }
            public bool selectable { get; set; }
            public bool serializable { get; set; }
            public bool enable { get; set; }
            public string startCap { get; set; }
            public string endCap { get; set; }
            public List<object> points { get; set; }
            public string fromConnector { get; set; }
            public string toConenctor { get; set; }
            public Stroke5 stroke { get; set; }
            public Selection selection { get; set; }
            public Editable2 editable { get; set; }
            public string type { get; set; }
            public DataItem dataItem { get; set; }
            public To to { get; set; }
            public double toX { get; set; }
            public double toY { get; set; }
            public From from { get; set; }
        }

        public class ShapesAndConnectios
        {
            public List<Shape> shapes { get; set; }
            public List<Connection> connections { get; set; }
        }
    }
}
