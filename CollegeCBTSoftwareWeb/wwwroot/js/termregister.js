var classbtn = ""; var classfa = "";
var ap = "";
$(document).ready(function () {
    loadtable();

});

function loadtable() {
    $("#studentExamTable").DataTable({
        "processing": true, "serverSide": true,
        "filter": true,
        responsive: true,
        destroy: true,
        "ajax": {
            "url": "/Home/StudentTermReg",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{ "visible": true, "searchable": true, }],
        "columns": [
            {
                "data": "id", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; }
            },
            { "data": "studentTable.fullName", "autoWidth": true },
            { "data": "studentTable.applicationUser.userName", "autoWidth": true },
            { "data": "semester", "autoWidth": true },
            { "data": "sessionTable.name", "autoWidth": true },
            { "data": "level", "autoWidth": true },
            {
                "data": null, "render": function (data, type, full) {
                    var output = Object.keys(data.studentCourseReg).length;
                    return output;
                }
                
            },
            {
                "data": null, "render": function (data, type, full) {
                    return `
                        <div class="d-flex justify-content-center">
                         <a href="/Student-Manager/StudentExamRegister/Edit?id=${data.id}" style="cursor:pointer" title="Edit Student Information" class="btn btn-sm btn-primary">
                                <i class="fa fa-edit"></i>
                            </a> &nbsp;&nbsp;&nbsp;
                            <a onclick=deletestudentReg("/api/DeleteStudentTermReg?id=${data.id}") class="btn btn-sm btn-danger"  href="#" style="cursor:pointer">
                                   <i class="fa fa-trash"></i>
                               </a>
                        </div>
                    `;
                }
            }
        ]
    });
}
function deletestudentReg(url) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "Every data associated to this student will be remove as well!",
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
                'Student Term Registeration NOT Deleted!',
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