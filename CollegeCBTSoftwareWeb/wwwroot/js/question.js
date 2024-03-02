var classbtn = ""; var classfa = "";
var ap = "";
$(document).ready(function () {
    loadtable();

    $('#Question_Question').val("");
    $('#Question_OptionOne').val("");
    $('#Question_OptionTwo').val("");
    $('#Question_OptionThree').val("");
    $('#Question_OptionFour').val("");
    $('#Question_OptionFive').val("");
    $('#Question_Answer').val("");
    //$('#Question_ClassId').val("");
    //$('#Question_SubjectId').val("");
});

function loadtable() {
    datatable = $("#questionTable").DataTable({
        "processing": true, "serverSide": true,
        "filter": true,
        responsive: true,
        destroy: true,
        "ajax": {
            "url": "/Home/QuestionTableLoader",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{ "visible": true, "searchable": true, }],
        "columns": [
            {
                "data": "id", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; }
            },
            { "data": "courseTable.name", "autoWidth": true },
            { "data": "level", "autoWidth": true },
            { "data": "semester", "autoWidth": true },
            {
                "data": "question",
                render: function (data, type, row) {
                    if (data.length > 10) {
                        return data = data.substring(0, 50);
                    } else {
                        return data;
                    }
                }
            },
            {
                "data": null, "render": function (data, type, full) {
                    return `
                        <div class="d-flex justify-content-center">
                         <a href="/Questions/Upsert?id=${data.id}" style="cursor:pointer" title="Edit Question" class="btn btn-primary btn-sm">
                                <i class="fa fa-edit"></i>
                            </a> &nbsp;&nbsp;
                            <a onclick=managequestion("/api/DeleteQuestion?id=${data.id}")  href="#" style="cursor:pointer" class="btn btn-danger btn-sm">
                                   <i class="fa fa-trash"></i>
                               </a>
                        </div>
                    `;
                }
            }
        ]
    });
}
function managequestion(url) {
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
                'Question NOT Deleted!',
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