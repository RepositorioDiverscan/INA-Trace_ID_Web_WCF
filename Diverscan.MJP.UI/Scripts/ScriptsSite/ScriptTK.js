var searchUrlFormat = "http://www.google.com/search?q={0}";
var mapSearchUrlFormat = "http://maps.google.com/maps?q={0}, {1}";

function formatTitle(title) {
    switch (title) {
        case "Owner":
            return "color: #8b0000";
            break;
        case "Accounting Manager":
            return "color: #4b0082";
            break;
        case "Marketing Manager":
            return "color: #191970";
            break;
        case "Order Administrator":
            return "color: #20b2aa";
            break;
        case "Sales Manager":
            return "color: #228b22";
            break;
        default:
            return "color: #008080";
    }
}

function setDisplay(isSelected) {
    return isSelected ? "block" : "none";
}

var internalDeselect = false;

function selectionChanged(sender, args) {
    if (!internalDeselect) {
        sender.get_masterTableView().rebind();
    }
}

function command(sender, args) {
    if (args.get_commandName() == "Page") {
        internalDeselect = true;
        var masterTable = sender.get_masterTableView();
        masterTable.clearSelectedItems();
        masterTable.rebind();
        internalDeselect = false;
    }
}