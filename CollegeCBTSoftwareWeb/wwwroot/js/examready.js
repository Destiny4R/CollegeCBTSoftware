var classbtn = ""; var classfa = "";
var ap = "";
$(document).ready(function () {
    loadtable();

});

function loadtable() {
    $("#publishtable").DataTable({
        "processing": true, "serverSide": true,
        "filter": true,
        responsive: true,
        destroy: true,
        "ajax": {
            "url": "/Home/ExampublishLoader",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{ "visible": true, "searchable": true, }],
        "columns": [
            {
                "data": "id", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; }
            },
            { "data": "semester", "autoWidth": true },
            { "data": "sessionTable.name", "autoWidth": true },
            { "data": "level", "autoWidth": true },
            { "data": "courseTable.name", "autoWidth": true },
            {
                "data": "startDate", "autoWidth": true,
                render: function (data, type, row) {
                    if (data.length > 10) {
                        return data = data.substring(0, 10);
                    } else {
                        return data;
                    }
                }
            },
            { "data": "noToAnswer", "autoWidth": true },
            { "data": "duration", "autoWidth": true },
            {
                "data": "publishExam",
                render: function (data, type, row) {
                    if (data == true) {
                        classfa = "fa-unlock";
                        classbtn = "btn-success";
                        return data;
                    } else {
                        classfa = "fa-lock";
                        classbtn = "btn-warning";
                        return data;
                    }
                }
            },
            {
                "data": null, "render": function (data, type, full) {
                    return `
                        <div class="d-flex justify-content-center">
                         <a class=" btn-secondary btn-sm" href="/PublishExams/Upsert?id=${data.id}" style="cursor:pointer" title="Edit Published Information">
                                <i class="fa fa-edit"></i>
                            </a> &nbsp;
                            <a onclick=puasefinish("/api/PauseOrFinish?id=${data.id}") class="btn ${classbtn} btn-sm" href="#" style="cursor:pointer" title="Pause/Start or Finish Exams">
                                <i class="fa ${classfa}"></i>
                            </a> &nbsp;
                            <a onclick=DeleteExamz("/api/DeleteExamsReady?id=${data.id}") class="btn btn-danger btn-sm" href="#" style="cursor:pointer">
                                   <i class="fa fa-trash"></i>
                               </a>
                        </div>
                    `;
                }
            }
        ]
    });
}
function DeleteExamz(url) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to recover this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    $.unblockUI();
                    if (data.success) {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        loadtable();
                    } else {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'error'
                        );
                    }
                },
                beforeSend: function () {
                    blockcallback();
                },
                error: function () {
                    $.unblockUI();
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swal.fire(
                'Cancelled',
                'Student NOT Deleted!',
                'error'
            );
        }
    });
}

function puasefinish(url) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "Pause/Finish Exam's",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Execute!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    $.unblockUI();
                    if (data.success) {
                        swal.fire(
                            'Action Message!',
                            data.message,
                            'success'
                        );
                        loadtable();
                    } else {
                        swal.fire(
                            'Action Message!',
                            data.message,
                            'error'
                        );
                    }
                },
                beforeSend: function () {
                    blockcallback();
                },
                error: function () {
                    $.unblockUI();
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swal.fire(
                'Cancelled',
                'Action aborted!',
                'error'
            );
        }
    });
}

function blockcallback() {
    $.blockUI({
        message: '<div class="bs-spinner mt-4 mt-lg-0"><div class= "spinner-border text-success mr-2 mt-2" style="width: 10rem; height: 10rem;" role="status"><span class="sr-only">Loading...</span></div> ',
        fadeIn: 800,
        //timeout: 2000, unblock after 2 seconds
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            zIndex: 1200,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            zIndex: 1201,
            padding: 0,
            backgroundColor: 'transparent'
        },
        onBlock: function () {

        }
    });
}