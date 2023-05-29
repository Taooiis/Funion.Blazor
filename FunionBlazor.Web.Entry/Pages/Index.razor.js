export function getHtml(elementRef) {
    return elementRef.outerHTML;
}
export function getElementById(ElementId) {
    return document.getElementById(ElementId).innerHTML;
}
//MASA Blazor MDataTable ID 属性有问题生成的dom元素没有 id
export function SetTableByfatherId(ElementId) {
    return document.getElementById(ElementId).getElementsByTagName('table')[0].id ="MDataTable"; 
}

