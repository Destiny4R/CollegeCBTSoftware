$(document).ready(function () {
    
    $('#ParetTable').DataTable({
        order: [[1, 'desc']],
        filter: false,
        paging: false,
        info: false
    });

    $('#classTable').DataTable({
        responsive: true,
        order: [[1, 'desc']],
    });

    $('#sessionTable').DataTable({
        order: [[1, 'desc']],
    });

    $('#subClassTable').DataTable({
        order: [[1, 'desc']],
    });
    $('#subjectTable').DataTable({
        order: [[1, 'desc']],
    });
    $('#subjectResultTable').DataTable({
        order: [[1, 'asc']],
        responsive: true,
        paging: false,
        info: false
    });
});

function removeParentData(url) {
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
                    $('#staticBackdrop').modal('hide');
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Parent Information Successfully Deleted.',
                            'success'
                        );
                        location.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                beforeSend: function () {
                    $('#staticBackdrop').modal('show');
                },
                error: function () {
                    $('#staticBackdrop').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $('#staticBackdrop').modal('hide');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Parent Data NOT Deleted!',
                'error'
            );
        }
    });
}
//API CALL FOR CLASS
function removeClass(url) {
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
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Class Successfully Deleted.',
                            'success'
                        );
                        location.reload();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: data.message,
                            footer: 'Information executing outcome!'
                        });
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
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Class NOT Deleted!',
                'error'
            );
        }
    });
}

//API CALL FOR SESSION
function removeSession(url) {
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
                    $('#staticBackdrop').modal('hide');
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Session Successfully Deleted.',
                            'success'
                        );
                        location.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                beforeSend: function () {
                    $('#staticBackdrop').modal('show');
                },
                error: function () {
                    $('#staticBackdrop').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $('#staticBackdrop').modal('hide');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Session NOT Deleted!',
                'error'
            );
        }
    });
}

//API CALL FOR SUBCLASS
function removeSubClass(url) {
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
                    $('#staticBackdrop').modal('hide');
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Sub Class Successfully Deleted.',
                            'success'
                        );
                        location.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                beforeSend: function () {
                    $('#staticBackdrop').modal('show');
                },
                error: function () {
                    $('#staticBackdrop').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $('#staticBackdrop').modal('hide');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Sub-Class NOT Deleted!',
                'error'
            );
        }
    });
}

//API CALL FOR SUBJECT
function removeSubject(url) {
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
                    $('#staticBackdrop').modal('hide');
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Subject Successfully Deleted.',
                            'success'
                        );
                        location.reload();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: data.message,
                            footer: 'Data execution message!'
                        });
                        toastr.error(data.message);
                    }
                },
                beforeSend: function () {
                    $('#staticBackdrop').modal('show');
                },
                error: function () {
                    $('#staticBackdrop').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $('#staticBackdrop').modal('hide');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Subject NOT Deleted!',
                'error'
            );
        }
    });
}
//API CALL FOR RESULT SUBJECT ASSESSMENT
function removeResultSubject(url) {
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
                url: "/WebApi/DeleteResult/"+url,
                success: function (data) {
                    $('#staticBackdrop').modal('hide');
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        location.reload();
                    } else {
                        toastr.error(data.message);
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: data,message,
                            footer: 'Check internet connectivity!'
                        });
                    }
                },
                beforeSend: function () {
                    $('#staticBackdrop').modal('show');
                },
                error: function () {
                    $('#staticBackdrop').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: 'Check internet connectivity!'
                    });
                },
                complete: function () {
                    $('#staticBackdrop').modal('hide');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Subject Record NOT Deleted!',
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

function deletestudentsubject(button) {
    const id = $(button).attr("data-id");
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
                url: "/api/SubectAbort?id=" + id,
                success: function (data) {
                    $.unblockUI();
                    if (data.success) {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        var row = $(button).closest("TR");
                        var table = $("#coursetablle")[0];
                        table.deleteRow(row[0].rowIndex);

                    } else {
                        swal.fire(
                            'Command Execution Failed!',
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
                'Student Subject NOT Deleted!',
                'error'
            );
        }
    });
}

function resetExampaper(button) {
    const id = $(button).attr("data-id");
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You reset this course for the Studen to re-take?!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/api/ResetExams?id=" + id,
                success: function (data) {
                    $.unblockUI();
                    if (data.success) {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        location.reload();

                    } else {
                        swal.fire(
                            'Command Execution Failed!',
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
                        footer: 'Check network connectivity!'
                    });
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swal.fire(
                'Cancelled',
                'No changes made for Examination!',
                'error'
            );
        }
    });
}
