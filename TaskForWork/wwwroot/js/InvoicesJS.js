function GetAllInvoices() {
    var url = '/Invoices/GetAllInvoices';
    $('table').html("");

    var thead = '<div class="table-head"><div class="head-line"><div class="head-column">#</div><div class="head-column">Tarix</div><div class="head-column">Məhsul Adı</div><div class="head-column">Miqdar</div><div class="head-column">Qiymət</div><div class="head-column">Məbləğ</div></div></div><div class="table-body">';
    $('table').append(thead);
    $.getJSON(url, function (data) {
        for (var invoice in data) {
            var checkbox = '<input type="checkbox" name="secilmis" value=' + data[invoice].invId +' />';
            var value = ' <div class="body-line"><div class="body-column">' + checkbox + '</div><div class="body-column">' + data[invoice].saleDate + '</div><div class="body-column">' + data[invoice].itemName + '</div><div class="body-column">' + data[invoice].amount + '</div><div class="body-column">' + data[invoice].price + '</div><div class="body-column">' + data[invoice].total + '</div></div>';
            $('table').append(value);
        }
    });

    $('table').append('</div>');

}



function DataChanged() {
    if (document.getElementById("amount") != null) {
        getamount = Number(document.getElementById("amount").value);
        document.getElementById("total").value = Number(document.getElementById("price").value) * getamount;
    }
    else {
        document.getElementById("total").value = 0;
    }
    
}

$(document).on("click", "#addInvoice", async function () {
    var dict = {};
    var dictPrice = {};
    var inputOptionsPromise = new Promise(function (resolve) {
        // get your data and pass it to resolve()
        setTimeout(function () {
            var url = '/Items/GetAllItemsForInvoice';
            
            $.getJSON(url, function (data) {
                
                for (var item in data) {
                    dict[data[item].itemId] = data[item].itemName;
                    dictPrice[data[item].itemId] = data[item].itemPrice;
                }
                resolve(dict);
            });

        }, 2000)
    });

    const { value: item } = await Swal.fire({
        title: 'Satış Fakturası',
        input: 'select',
        inputOptions: inputOptionsPromise,
        inputPlaceholder: 'Məhsul seçin',
        showCancelButton: true,
        cancelButtonColor: "#3C3C3C",
        cancelButtonText: "İmtina Et",
        confirmButtonColor: "#165ba8",
        confirmButtonText: "Təsdiq"
    })

    if (item) {
       
        const { value: formValues } = await Swal.fire({
            title: dict[item],
            showCancelButton: true,
            cancelButtonColor: "#3C3C3C",
            cancelButtonText: "İmtina Et",
            confirmButtonColor: "#165ba8",
            confirmButtonText: "Göndər",
            html:
                '<label class="swal2-label">Qiymət</label>' +
                '<input onchange="DataChanged()" id="price" type="number" placeholder="Qiyməti" value="' + dictPrice[item] + '" class="swal2-input">' +
                '<label class="swal2-label">Miqdar</label>' +
                '<input onchange="DataChanged()" id="amount" type="number" placeholder="Miqdar" class= "swal2-input" > ' +
                '<label class="swal2-label">Cəm</label>' +
                '<input id="total" type="number" placeholder="Toplam Məbləğ" class="swal2-input" disabled>'+
                '<label class="swal2-label">Tarix</label>' +
                '<input id="date" type="date" placeholder="Tarix" class="swal2-input">',
            focusConfirm: false,
            preConfirm: () => {
                var date = new Date();
                if (document.getElementById('date').value!='') {
                    date = document.getElementById('date').value;

                }
                return [
                    item,
                    document.getElementById('amount').value,
                    document.getElementById('price').value,
                    document.getElementById('total').value,
                    date
                    
                ]
            }
        })
        $.ajax(
            {
                type: "POST", //HTTP POST Method
                url: "Invoices/AddInvoice", // Controller/View
                dataType: 'json',
                data: { //Passing data
                    InvId: 0,
                    ItemId: formValues[0],
                    Amount: formValues[1], //Reading text box values using Jquery
                    Price: formValues[2],
                    Total: formValues[3],
                    SaleDate: formValues[4]
                },
                success: function (result) {
                    if (result == 1) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Uğurlu',
                            text: 'Faktura uğurla əlavə edildi!',
                            confirmButtonColor: '#165ba8',
                            confirmButtonText: 'Bağla'
                        });
                        GetAllInvoices();
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Uğursuz',
                            text: 'Faktura əlavə edərkən xəta baş verdi!',
                            confirmButtonColor: '#165ba8',
                            confirmButtonText: 'Bağla'
                        });
                    }
                }
            });
    }
    else {
        Swal.fire({
            icon: 'info',
            title: 'Diqqət',
            text: `Heç bir məhsul seçilməyib`,
            confirmButtonColor: '#165ba8',
            confirmButtonText: 'Bağla'
        });
    }


});



$(document).on("click", "#editInvoice", async function () {
    var data = [];
    var count = 0;
    $("input[name='secilmis']:checked").each(function () {
        data[count] = $(this).val();
        count++;
    });
    if (data.length == 1)
    {
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "Invoices/EditInvoice?id="+data[0], // Controller/View/id
                success: async function (data) {
                    const { value: formValues } = await Swal.fire({
                        title: data.itemName,
                        showCancelButton: true,
                        cancelButtonColor: "#3C3C3C",
                        cancelButtonText: "İmtina Et",
                        confirmButtonColor: "#165ba8",
                        confirmButtonText: "Göndər",
                        html:
                            '<label class="swal2-label">Qiymət</label>' +
                            '<input onchange="DataChanged()" id="price" type="number" placeholder="Qiyməti" value="' + data.price + '" class="swal2-input">' +
                            '<label class="swal2-label">Miqdar</label>' +
                            '<input onchange="DataChanged()" id="amount" type="number" placeholder="Miqdar" value="' + data.amount + '" class= "swal2-input" > ' +
                            '<label class="swal2-label">Cəm</label>' +
                            '<input id="total" type="number" placeholder="Toplam Məbləğ" value="' + data.total + '" class="swal2-input" disabled>' +
                            '<label  class="swal2-label">Tarix</label>' +
                            '<input id="date" type="date" placeholder="Tarix" value="' + data.saleDate + '" class="swal2-input">',
                        focusConfirm: false,
                        preConfirm: () => {
                            var date = new Date();
                            if (document.getElementById('date').value != '') {
                                date = document.getElementById('date').value;

                            }
                            return [
                                data.invId,
                                document.getElementById('amount').value,
                                document.getElementById('price').value,
                                document.getElementById('total').value,
                                date

                            ]
                        }
                    })
                    $.ajax(
                        {
                            type: "POST", //HTTP POST Method
                            url: "Invoices/EditInvoice", // Controller/View
                            dataType: 'json',
                            data: { //Passing data
                                InvId: formValues[0],
                                Amount: formValues[1], //Reading text box values using Jquery
                                Price: formValues[2],
                                Total: formValues[3],
                                SaleDate: formValues[4]
                            },
                            success: function (result) {
                                if (result == 1) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: 'Uğurlu',
                                        text: 'Faktura uğurla dəyişdirildi!',
                                        confirmButtonColor: '#165ba8',
                                        confirmButtonText: 'Bağla'
                                    });
                                    GetAllInvoices();
                                }
                                else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Uğursuz',
                                        text: 'Faktura dəyişdirilərkən xəta baş verdi!',
                                        confirmButtonColor: '#165ba8',
                                        confirmButtonText: 'Bağla'
                                    });
                                }
                            }
                        });

                }
            });
    }
    else
    {
        Swal.fire({
            icon: 'warning',
            title: 'Diqqət',
            text: 'Dəyişmək üçün yalnız 1 sətir seçilməlidir.',
            confirmButtonText: 'Bağla',
            confirmButtonColor: '#165ba8'
        })
    }
});




$(document).on("click", "#deleteInvoice", async function () {
    var data = [];
    var count = 0;
    $("input[name='secilmis']:checked").each(function () {
        data[count] = $(this).val();
        count++;
    });
    if (data.length == 0) {
        Swal.fire({
            icon: 'warning',
            title: 'Diqqət',
            text: 'Heç bir sətir seçilməyib',
            confirmButtonText: 'Bağla',
            confirmButtonColor: '#165ba8'
        })
    }
    else
    {

        Swal.fire({
            title: 'Silmək istədiyinizə əminsiniz?',
            text: "Seçilmiş fakturalar silinəcək!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#165ba8',
            cancelButtonColor: '#d33',
            cancelButtonText: 'İmtina Et',
            confirmButtonText: 'Bəli, Sil'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST", //HTTP POST Method
                    url: "Invoices/DeleteInvoices", // Controller/View
                    dataType: 'json',
                    data: { "data": data },
                    success: function (result) {
                        if (result == 1) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Uğurlu',
                                text: 'Fakturalar uğurla silindi!',
                                confirmButtonText: 'Bağla',
                                confirmButtonColor: '#165ba8'
                            });
                            GetAllInvoices();
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Uğursuz',
                                text: 'Faktura silinərkən xəta baş verdi!',
                                confirmButtonText: 'Bağla',
                                confirmButtonColor: '#165ba8'
                            });
                        }
                    }

                });
            }
        })

    }
});
