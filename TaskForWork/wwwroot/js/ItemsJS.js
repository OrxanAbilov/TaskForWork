function GetAllItems() {
    var url = '/Items/GetAllItems';
    $('table').html("");

    var thead = '<div class="table-head"><div class="head-line"><div class="head-column">#</div><div class="head-column">Ad</div><div class="head-column">Qiymət</div><div class="head-column">Miqdar</div></div></div><div class="table-body">';
    $('table').append(thead);
    var rowNo = 0;
    $.getJSON(url, function (data) {
        for (var item in data) {
            rowNo += 1;
            var checkbox = '<input type="checkbox" name="secilmis" value=' + data[item].itemId +' />';
            var value = ' <div class="body-line"><div class="body-column">' + checkbox + '</div><div class="body-column">' + data[item].itemName + '</div><div class="body-column">' + data[item].itemPrice + '</div><div class="body-column">' + data[item].itemStock + '</div></div>';
            $('table').append(value);
        }
    });

    $('table').append('</div>');

}



$(document).on("click", "#addItem", async function () {
    const { value: formValues } = await Swal.fire({
        title: 'Yeni Məhsul',
        showCancelButton: true,
        cancelButtonColor: "#3C3C3C",
        cancelButtonText: "İmtina Et",
        confirmButtonColor: "#165ba8",
        confirmButtonText: "Göndər",
        html:
            '<input id="itemName" type="text" placeholder="Məhsul Adı" class="swal2-input">'+
            '<input id="itemPrice" type="number" placeholder="Məhsul Qiyməti" class="swal2-input">'+
            '<input id="itemStock" type="number" placeholder="Məhsul Miqdarı" class="swal2-input">',
        focusConfirm: false,
        preConfirm: () => {
            return [
                document.getElementById('itemName').value,
                document.getElementById('itemPrice').value,
                document.getElementById('itemStock').value
            ]
        }
    })
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: "Items/AddItem", // Controller/View
            dataType: 'json',
            data: { //Passing data
                ItemId: 0,
                ItemName: formValues[0], //Reading text box values using Jquery
                ItemPrice: formValues[1],
                ItemStock: formValues[2]
            },
            success: function (result) {
                if (result == 1) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Uğurlu',
                        text: 'Məhsul uğurla əlavə edildi!',
                        confirmButtonColor: '#165ba8',
                        confirmButtonText: 'Bağla'
                    });
                    GetAllItems();
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Uğursuz',
                        text: 'Məhsul əlavə edərkən xəta baş verdi!',
                        confirmButtonColor: '#165ba8',
                        confirmButtonText: 'Bağla'
                    });
                }
            }
        });
    
});



$(document).on("click", "#editItem", async function () {
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
                url: "Items/EditItem?id="+data[0], // Controller/View/id
                success: async function (data) {
                    var name = data.itemName
                    
                    const { value: formValues } = await Swal.fire({
                        title: 'Məhsul Məlumatlarını Dəyiş',
                        showCancelButton: true,
                        cancelButtonColor: "#3C3C3C",
                        cancelButtonText: "İmtina Et",
                        confirmButtonColor: "#165ba8",
                        confirmButtonText: "Göndər",
                        html:
                            '<input id="itemId" value = "' + data.itemId + '" type="hidden" placeholder="Məhsul Adı" class="swal2-input">' +
                            '<input id="itemName" value = "' + name + '" type="text" placeholder="Məhsul Adı" class="swal2-input">' +
                            '<input id="itemPrice" value = "' + data.itemPrice + '" type="number" placeholder="Məhsul Qiyməti" class="swal2-input">' +
                            '<input id="itemStock" value = "' + data.itemStock + '" type="number" placeholder="Məhsul Miqdarı" class="swal2-input">',
                        focusConfirm: false,
                        preConfirm: () => {
                            return [
                                document.getElementById('itemId').value,
                                document.getElementById('itemName').value,
                                document.getElementById('itemPrice').value,
                                document.getElementById('itemStock').value
                            ]
                        }
                    })
                    $.ajax(
                        {
                            type: "POST", //HTTP POST Method
                            url: "Items/EditItem", // Controller/View
                            dataType: 'json',
                            data: { //Passing data
                                ItemId: formValues[0],
                                ItemName: formValues[1], //Reading text box values using Jquery
                                ItemPrice: formValues[2],
                                ItemStock: formValues[3]
                            },
                            success: function (result) {
                                if (result == 1) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: 'Uğurlu',
                                        text: 'Məhsul məlumatları uğurla dəyişdirildi!',
                                        confirmButtonColor: '#165ba8',
                                        confirmButtonText:'Bağla'
                                    });
                                    GetAllItems();
                                }
                                else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Uğursuz',
                                        text: 'Məhsul dəyişdirilərkən xəta baş verdi!',
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

$(document).on("click", "#deleteItem", async function () {
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
            text: "Seçilmiş məhsullar silinəcək!",
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
                    url: "Items/DeleteItems", // Controller/View
                    dataType: 'json',
                    data: { "data": data },
                    success: function (result) {
                        if (result == 1) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Uğurlu',
                                text: 'Məhsul uğurla silindi!',
                                confirmButtonText: 'Bağla',
                                confirmButtonColor: '#165ba8'
                            });
                            GetAllItems();
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Uğursuz',
                                text: 'Məhsul silinərkən xəta baş verdi!',
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
