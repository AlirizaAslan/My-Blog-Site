$(document).ready(function () {
    $('#articlesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut deðil",
            "sInfo": "_TOTAL_ kayýttan _START_ - _END_ arasýndaki kayýtlar gösteriliyor",
            "sInfoEmpty": "Kayýt yok",
            "sInfoFiltered": "(_MAX_ kayýt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayýt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "Ýþleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eþleþen kayýt bulunamadý",
            "oPaginate": {
                "sFirst": "Ýlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sýralamasýný aktifleþtir",
                "sSortDescending": ": azalan sütun sýralamasýný aktifleþtir"
            },
            "select": {
                "rows": {
                    "_": "%d kayýt seçildi",
                    "0": "",
                    "1": "1 kayýt seçildi"
                }
            }
        }
    });
});