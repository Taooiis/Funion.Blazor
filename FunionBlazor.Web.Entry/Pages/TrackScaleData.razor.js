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

export function printContent(ElementId, Cstr) {
    // 获取要处理的表格元素
    var table = document.getElementById(ElementId);

    // 创建新的表格元素
    var newdiv = document.createElement("div");
    var newTable = document.createElement("table");

    // 设置新表格的样式
    newTable.style.borderCollapse = "collapse";  // 合并边框
    newTable.style.border = "1px solid #000";   // 实线边框
    newTable.style.margin = "auto";             // 居中对齐
    newTable.style.width = "100 %";             
    // 创建表头并添加到新表格中
    var headElement = document.createElement("thead");
    var theadElement = document.createElement("h2");
    theadElement.innerHTML = "大唐富平热电有限公司";
    theadElement.style.textAlign = "center";
    var element = document.createElement("h5");
    element.innerHTML = "过车时间:" + Cstr + " &nbsp;| &nbsp;重量单位: t";
    var titleElement = document.createElement("title");
    titleElement.innerHTML = "数据打印";
    headElement.appendChild(theadElement);
    headElement.appendChild(element);
    headElement.appendChild(titleElement);
    newTable.appendChild(headElement);

    // 处理表格中的每一行数据
    //存储第4列的合计值  (毛重)  存储第5列的合计值  (皮重) 存储第6列的合计值  (净重) 存储第7列的合计值  (标重) 存储第8列的合计值  (盈亏)
    var total4=0, total5=0, total6=0, total7=0, total8 = 0;  
     
    // 处理表格中的每一行数据
    for (var i = 0; i < table.rows.length; i++) {
        // 创建新的行元素
        var newRow = document.createElement("tr");
        // 处理当前行的每个单元格
        for (var j = 0; j < table.rows[i].cells.length; j++) {
            if (j != table.rows[i].cells.length ) {
                // 创建新的单元格元素
                var newCell = document.createElement("td");
                // 复制旧的单元格的内容
                newCell.innerHTML = table.rows[i].cells[j].innerHTML;
                // 设置单元格样式
                newCell.style.border = "1px solid #000";  // 实线单元格边框
                newCell.style.padding = "8px";           
                newCell.style.textAlign = "center";       // 居中对齐  
                // 将新的单元格添加到新的行中
                newRow.appendChild(newCell);
                if (i == 0) {
                    //列头
                } else if (j == 3) {  // 第4列  (毛重)
                    total4 += parseFloat(table.rows[i].cells[j].innerHTML);
                } else if (j == 4) {  // 第5列  (皮重)
                    total5 += parseFloat(table.rows[i].cells[j].innerHTML);
                } else if (j == 5) {  // 第6列  (净重)
                    total6 += parseFloat(table.rows[i].cells[j].innerHTML);
                } else if (j == 6) {  // 第7列  (标重)
                    total7 += parseFloat(table.rows[i].cells[j].innerHTML);
                } else if (j == 7) {  // 第7列  (盈亏)
                    total8 += parseFloat(table.rows[i].cells[j].innerHTML);
                }
            }
        }
        // 将新的行添加到新表格中
        newTable.appendChild(newRow);
    }
    var element = document.createElement("h5");
    element.innerHTML = "毛重合计：" + total4.toFixed(2) + "&nbsp;&nbsp;皮重合计：" + total5.toFixed(2) + "&nbsp;&nbsp;净重合计：" + total6.toFixed(2) + "&nbsp;&nbsp;标重合计：" + total7.toFixed(2) + "&nbsp;&nbsp;盈亏合计：" + total8.toFixed(2)
    newdiv.appendChild(newTable);
    newdiv.appendChild(element);
    var printWin = window.open('', '', 'left=0,top=0,width=1500,height=1200,toolbar=0,scrollbars=0,status=0');
    printWin.document.write(newdiv.outerHTML);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();
}


