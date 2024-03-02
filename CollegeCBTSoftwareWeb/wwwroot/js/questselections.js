// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".basic-subject").select2({
    placeholder: "Select Subject",
    theme: "classic",
    allowClear: true
});
$(".select-for-class").select2({
    placeholder: "Select Class",
    theme: "classic",
    allowClear: true
});
$(document).ready(function () {
    if ($("#Node_PassageQuestion").length > 0) {
        tinymce.init({
            selector: "textarea#Node_PassageQuestion",
            theme: "modern",
            height: 300,
            plugins: [
                "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
                "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                "save table contextmenu directionality emoticons template paste textcolor"
            ],
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons",
            style_formats: [
                { title: 'Bold text', inline: 'b' },
                { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                { title: 'Example 1', inline: 'span', classes: 'example1' },
                { title: 'Example 2', inline: 'span', classes: 'example2' },
                { title: 'Table styles' },
                { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
            ]
        });
    }
});
function addquest() {
    var question = $("#question").val();
    var optionone = $("#optionone").val();
    var optiontwo = $("#optiontwo").val();
    var optionthree = $("#optionthree").val();
    var optionfour = $("#optionfour").val();
    var answer = $("#answer").val();
    var btnz = '<button type="button" onclick="remove(this);" class="btn btn-danger btn-sm who"><i class="fa fa-trash"></i></button>';
    if (question == "" || optionone == "" || optiontwo == "" || answer == "") {
        return swal.fire(
            'Adding Question Aborted',
            'Provide all required fields before adding!',
            'error'
        );
    }
    $("#questionHolder > tbody").append("<tr><td>" + question + "</td><td>" + optionone + "</td><td>" + optiontwo + "</td><td>" + optionthree + "</td><td>" + optionfour + "</td><td>" + answer + "</td><td>" + btnz + "</td></tr>");
    $("#question").val('');
    $("#optionone").val('');
    $("#optiontwo").val('');
    $("#optionthree").val('');
    $("#optionfour").val('');
    $("#answer").val('').focus();
}
function remove(button) {
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    Swal.fire({
        title: 'Are you sure?',
        text: "Remove: " + name + "?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '$success',
        cancelButtonColor: '$danger',
        confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
        if (result.value) {
            var table = $("#questionHolder")[0];
            table.deleteRow(row[0].rowIndex);
            Swal.fire(
                'Deleted!',
                'Question Removed',
                'success'
            )
        }
    });
};
function postquestion() {
    var questions = new Array();
    var nodequestionvm = {
        questions: []
    }

    $("#questionHolder TBODY TR").each(function () {
        var row = $(this);
        var destiny = {};
        destiny.question = row.find("TD").eq(0).html();
        destiny.optionone = row.find("TD").eq(1).html();
        destiny.optiontwo = row.find("TD").eq(2).html();
        destiny.optionthree = row.find("TD").eq(3).html();
        destiny.optionfour = row.find("TD").eq(4).html();
        destiny.answer = row.find("TD").eq(5).html();
        nodequestionvm.questions.push(destiny);
    });
    nodequestionvm.Id = $("#Node_Id").val();
    nodequestionvm.ClassId = $("#Node_ClassId").val();
    nodequestionvm.SubjectId = $("#Node_SubjectId").val();
    nodequestionvm.Compulsory = $("#Node_Compulsory").val();
    nodequestionvm.Node_Subject = $("#Node_Node_Subject").val();
    nodequestionvm.Title = $("#Node_Title").val();
    nodequestionvm.PassageQuestion = (tinyMCE.get('Node_PassageQuestion').getContent());
    
    var questz = Object.keys(nodequestionvm.questions).length;
    if ((questz < 1 && nodequestionvm.Id !="")|| nodequestionvm.ClassId == "" || nodequestionvm.SubjectId == "" || nodequestionvm.Node_Subject == "" || nodequestionvm.Title == "" || nodequestionvm.PassageQuestion == "") {
        return swal.fire(
            'All Questions Submission Failed',
            'Provide all required fields before submitting!',
            'error'
        );
    } else {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success btn-sm',
                cancelButton: 'btn btn-danger btn-sm'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: "Submit This Report!",
            text: "Confirm every information before submitting this report!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, Submit!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    method: 'POST',
                    url: '/api/NodeQuestions',
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: nodequestionvm,
                    cache: false,
                    success: function (data) {
                        $.unblockUI();
                        if (data.success) {
                            swalWithBootstrapButtons.fire(
                                'Report Information!',
                                data.message,
                                'success'
                            );
                            window.location.replace("/Questions/NodeQuestion/Index");
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: data.message,
                                footer: 'Outcome Server Report'
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
                            footer: 'Check internet connectivity'
                        });
                    },
                    complete: function () {
                        $.unblockUI();
                    }
                });
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                swalWithBootstrapButtons.fire(
                    'Cancelled',
                    'Report Not Submitted!',
                    'Information'
                );
            }
        });
    }
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

function deletequestion(button) {
    const id = $(button).attr("data-id");
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Do you want to delete this question?',
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
                url: "/api/DeleteNote?id=" + id,
                success: function (data) {
                    $.unblockUI();
                    if (data.success) {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        var row = $(button).closest("TR");
                        var table = $("#questionHolder")[0];
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
                'Node Question NOT Deleted!',
                'error'
            );
        }
    });
}