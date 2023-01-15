function GetSaleReport() {
    var url = '/Reports/GetSaleReport';
    $('table').html("");
    var rowNo = 0;
    var thead = '<div class="table-head"><div class="head-line"><div class="head-column">No:</div><div class="head-column">Məhsul Adı</div><div class="head-column">Cəm Miqdar</div><div class="head-column">Cəm Məbləğ</div></div></div><div class="table-body">';
    $('table').append(thead);
    $.getJSON(url, function (data) {
        for (var row in data) {
            rowNo += 1;
            var value = ' <div class="body-line"><div class="body-column">' + rowNo + '</div><div class="body-column">' + data[row].name + '</div><div class="body-column">' + data[row].sumAmount + '</div><div class="body-column">' + data[row].sumTotal +'$</div></div>';
            $('table').append(value);
        }
    });

    $('table').append('</div>');

}
