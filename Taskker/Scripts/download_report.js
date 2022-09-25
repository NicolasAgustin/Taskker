function download() {
    $.ajax({
        type: 'GET',
        url: "/Report/DownloadReport",
        success: function (data) {
            alert('Reporte descargado')
        }
    })
}