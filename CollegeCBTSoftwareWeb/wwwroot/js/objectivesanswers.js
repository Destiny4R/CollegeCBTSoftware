// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function stoptimer() {
    clearInterval(countDown);
}
function examinationSubmission() {
    var totalquestions = 0;
    var getquestionz = new Array();
    var collectData = {
        getquestionz:[]
    };
    totalquestions = $("#totalOfQuestion").val();
    for (var i = 0; i <= totalquestions; i++) {
        var idproperty = 'fieldName[' + i + ']';
        var questionz = document.getElementsByName(idproperty);
        for (var answerCheck of questionz) {
            var mycollectedData = {};
            if (answerCheck.checked) {
                mycollectedData.id = $(answerCheck).attr("data-id");
                mycollectedData.answer = answerCheck.value;
                collectData.getquestionz.push(mycollectedData);
            }

        }
    }
    collectData.RegNumber = $('#regnumber').val();
    var sub = document.getElementsByName("course");
    collectData.semester = $('#semester').val();
    var claz = document.getElementsByName("level");
    var ses = document.getElementsByName("session");

    collectData.RegNumber = $('#regnumber').val();
    collectData.CourseId = $(sub).attr("data-id");
    collectData.level = $(claz).attr("data-id");
    collectData.sessionid = $(ses).attr("data-id");

    

    //Send result for marking
    $.ajax({
        method: 'POST',
        url: '/api/ExaminationData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: collectData,
        cache: false,
        success: function (data) {
            $.unblockUI();
            if (data.success) {
                //clearInterval(countDown);
                $('#quesAnswered').val(data.total);
                $('#corrAnswered').val(data.correct);
                $('#wrongAnswered').val(data.wrong);
                $('#finishExamsModal2').modal('show');
                $('#finishExams').hide();
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
                title: "Ooooop!",
                text: "No connectivity to the server detected!",
                icon: "error",
                showCancelButton: !0,
                confirmButtonColor: "#3d8ef8"
                //cancelButtonColor: "#f46a6a"
            })
        },
        complete: function () {
            $.unblockUI();
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
var dura = 0;
dura = $('#duration').val();
let timeSecond = dura * 60;
const timeH = document.getElementById("countdown");
function therealTimer() {
    //$('#basicphone').hide();
    displayTime(timeSecond);

    const countDown = setInterval(() => {
        timeSecond--;
        displayTime(timeSecond);
        if (timeSecond == 0 || timeSecond < 1) {
            //$('#basicphone').show();
            timeH.innerHTML = '';
            //alert("Time Up My Brrother");

            clearInterval(countDown);
        }
    }, 1000);

    function displayTime(second) {
        const min = Math.floor(second / 60);
        const sec = Math.floor(second % 60);
        timeH.innerHTML = convertSecondsToHMS(timeSecond)

        //`${min < 10 ? "0" : ""}${min}:${sec < 10 ? "0" : ""}${sec} `;
    }
}

function convertSecondsToHMS(totalSeconds) {
    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    return `${hours}h ${minutes}m ${seconds}s`;
}

$(document).ready(function () {
    therealTimer();
});