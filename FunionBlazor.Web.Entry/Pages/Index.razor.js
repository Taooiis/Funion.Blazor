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
    // 获取要处理的表格元素
    var table = document.getElementById(ElementId);

    // 创建新的表格元素
    var newTable = document.createElement("table");

    // 设置新表格的样式
    newTable.style.borderCollapse = "collapse";  // 合并边框
    newTable.style.border = "1px solid #000";   // 实线边框

    // 创建表头并添加到新表格中
    var headElement = document.createElement("thead");
    var titleElement = document.createElement("title");
    titleElement.innerHTML = "大唐富平热电有限公司";
    headElement.appendChild(titleElement);
    newTable.appendChild(headElement);

    // 处理表格中的每一行数据
    for (var i = 0; i < table.rows.length; i++) {
        // 创建新的行元素
        var newRow = document.createElement("tr");

        // 处理当前行的每个单元格
        for (var j = 0; j < table.rows[i].cells.length; j++) {
            if (j != table.rows[i].cells.length - 1) {
                // 创建新的单元格元素
                var newCell = document.createElement("td");

                // 复制旧的单元格的内容
                newCell.innerHTML = table.rows[i].cells[j].innerHTML;

                // 设置单元格样式
                newCell.style.border = "1px solid #000";  // 实线单元格边框

                // 将新的单元格添加到新的行中
                newRow.appendChild(newCell);
            }
        }

        // 将新的行添加到新表格中
        newTable.appendChild(newRow);
    }
    var printWin = window.open('', '', 'left=0,top=0,width=1500,height=1200,toolbar=0,scrollbars=0,status=0');
    printWin.document.write(newTable.outerHTML);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();
}


