(function (global, undefined) {
    var diagram;
    var jsonTextBox;

    function diagram_load(sender, args) {
        diagram = sender.get_kendoWidget();

        enableToolboxDragging();
        setDropTargetOnDiagram(diagram.element);

        diagram.layout({
            type: "tree",
            subtype: "left",
            roots: [diagram.getShapeById("nadal_winner")],
            verticalSeparation: 5,
            horizontalSeparation: 10,      
        });
    }

    function diagram_change(args) {
        var element = args.added[0];
        //Change connection type to polyline
        if (element instanceof kendo.dataviz.diagram.Connection) {
            //element.type("rectangle");
            element.points([]);
            element.refresh();
        }
    }

    function layoutBtn_click() {


        diagram.layout({
            type: "tree",
            subtype: "left",
            roots: [diagram.getShapeById("nadal_winner")],
            verticalSeparation: 5,
            horizontalSeparation: 10,
            animate: true
        });
    }

    function setDropTargetOnDiagram(element) {
        $telerik.$(element).kendoDropTarget({
            drop: function (e) {
                var draggable = e.draggable,
                    element = e.dropTarget,
                    diagram = element.getKendoDiagram();

                if (draggable && draggable.hint) {
                    var item = draggable.hint.data("data"),
                        offset = draggable.hintOffset,
                        point = new kendo.dataviz.diagram.Point(offset.left, offset.top),
                        transformed = diagram.documentToModel(point);

                    item.x = transformed.x;
                    item.y = transformed.y;

        
                    diagram.addShape(item);
                }
            }
        });
    }

    function getPositionMethod(deg) {
        var rad = deg * (Math.PI / 180);
        return function (shape) {
            var bounds = shape.bounds(),
                r = bounds.width / 2,
                circlePos = new kendo.dataviz.diagram.Point(r * Math.cos(rad), r * Math.sin(rad));

            return bounds.center().plus(circlePos);
        };
    }

    function enableToolboxDragging() {
        $telerik.$("#toolbox").kendoDraggable({
            filter: "div.item",
            hint: function (draggable) {
                var hint = draggable.clone(true);

                return hint;
            }
        });
    }

    function OnChange(args) {
        var shape = args.added[0],
            dataItem = shape.dataItem;

        if (shape instanceof kendo.dataviz.diagram.Shape) {
            shape.redraw({
                fill: { color: dataItem.fill.color },
                stroke: {
                    width: dataItem.stroke.width,
                    color: dataItem.stroke.color
                },
                content: {
                    color: dataItem.content.color,
                    align: dataItem.content.align
                }
            });
        }
    }

    //begin serialized

    function jsonText_load(sender) {
        jsonTextBox = sender;

        serializeToJSON();
    }

    function downloadJSON() {

        var form = document.createElement("form"),
            input = document.createElement("input");

        serializeToJSON();
        input.type = "hidden";
        input.name = "json";
        input.value = document.getElementById("JsonText").value;
        $telerik.$("body").append(form);
        form.action = "Default.aspx";
        form.method = "POST";
        form.appendChild(input);
        form.submit();
        form.parentNode.removeChild(form);
    }


    function serializeToJSON() {
        var diagram1 = $find("brackets").get_kendoWidget();
        var json = diagram1.save();//the diagram shapes and connections are saved in the json variable
        var jsonStr = Sys.Serialization.JavaScriptSerializer.serialize(json);
        document.getElementById("JsonText").value = jsonStr;
    }

    function loadFromServer(value) {
        diagram.load(value);
        serializeToJSON();
    }
    function loadFromJSON(value) {
        var json = document.getElementById("JsonText").value;
        loadDiagram(json);
    }
    function loadDiagram(json) { //load the JSON in the diagram
        diagram.load(Sys.Serialization.JavaScriptSerializer.deserialize(json));
    }

    function fileUploadedHandler(upload, args) {
        getAjaxManager().ajaxRequest();
    }

    // end jason serialized

    global.OnChange = OnChange;
    global.getPositionMethod = getPositionMethod;
    global.layoutBtn_click = layoutBtn_click;
    global.diagram_load = diagram_load;
    global.diagram_change = diagram_change;

    global.jsonText_load = jsonText_load;
    global.downloadJSON = downloadJSON;
    global.serializeToJSON = serializeToJSON;
    global.loadFromJSON = loadFromJSON;
    global.loadFromServer = loadFromServer;
    global.fileUploadedHandler = fileUploadedHandler;
})(window);