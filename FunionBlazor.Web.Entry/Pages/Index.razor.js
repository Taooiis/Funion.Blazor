export function getHtml(elementRef) {
    return elementRef.outerHTML;
}
export function getElementById(ElementId) {
    return document.getElementById(ElementId).innerHTML;
}
//MASA Blazor MDataTable ID 属性有问题生成的dom元素没有 id
export function SetTableByfatherId(ElementId) {
    return document.getElementById(ElementId).getElementsByTagName('table')[0].id = "MDataTable";
}

export function printContent(ElementId) {
    var table = document.getElementById(ElementId);
    var newTable = document.createElement("table");
    var headElement = document.createElement("head");
    var titleElement = document.createElement("title");
    titleElement.innerHTML = "大唐富平热电有限公司";
    headElement.appendChild(titleElement);
    newTable.appendChild(headElement);
    for (var i = 0; i < table.rows.length; i++) {
        var newRow = document.createElement("tr");
        for (var j = 0; j < table.rows[i].cells.length; j++) {
            if ( j != table.rows[i].cells.length - 1) {
                var newCell = document.createElement("td");
                newCell.innerHTML = table.rows[i].cells[j].innerHTML;
                newRow.appendChild(newCell);
            }
        }
        newTable.appendChild(newRow);
    }
    var printWin = window.open('', '', 'left=0,top=0,width=800,height=600,toolbar=0,scrollbars=0,status=0');
    printWin.document.write(newTable.outerHTML);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();
}


