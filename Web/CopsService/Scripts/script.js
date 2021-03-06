﻿function getPrivateIP(callBack) {
    window.RTCPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection;   //compatibility for firefox and chrome
    var pc = new RTCPeerConnection({ iceServers: [] }), noop = function () { };
    pc.createDataChannel("");    //create a bogus data channel
    pc.createOffer(pc.setLocalDescription.bind(pc), noop);    // create offer and set local description
    pc.onicecandidate = function (ice) {  //listen for candidate events
        if (!ice || !ice.candidate || !ice.candidate.candidate) return;
        var myIP = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/.exec(ice.candidate.candidate)[1];
        pc.onicecandidate = noop;
        callBack(myIP);
    };
}

if (document.getElementById('home') != null) {
    document.getElementById('title').addEventListener('mouseout', function () {
        document.getElementById('identity').style.display = 'none';
    }, false);
    document.getElementById('title').addEventListener('mouseover', function () {
        getPrivateIP(function cb(ip) {
            document.getElementById('identity').style.display = 'block';
            document.getElementById("ip").innerHTML = ip;
            document.getElementById("date").innerHTML = new Date();
        });
    }, false);
}

function askUserDetails(userId, data) {
    var modal = $('#accessInfoModal');
    modal.modal();
    modal.find('.modal-title').text("Access data of " + data);
    modal.find('#id').attr("value", userId);
}

//Info
function displayAccessInfoPopup(dataName) {
    var modal = $('#accessInfoModal');
    modal.modal();

    modal.find('form').submit(function (evt) {
        getPrivateIP(function cb(ip) {
            //int? dataId, string name, string reason, string ip
            const reason = modal.find('#reason').val();
            $.post('/Service/AddAccessInfo', { reason: reason},
                function (json) {
                    var data = JSON.parse(json);
                    console.log(data);
                    $('#user span.' + dataName).text(":" + data);
                });
            modal.find('#reason').val("");
            modal.modal('toggle');
        });
        evt.preventDefault();
        modal.find('form').unbind('submit');
    });
}

// Age calculation
$(window).load(function () {
    refreshDate();
});

$(document).ready(function () {
    $('#BirthDateD').keyup(
        function (event) {
            refreshDate();
        }
    );
    $('#BirthDateM').keyup(
        function (event) {
            refreshDate();
        }
    );
    $('#BirthDateY').keyup(
        function (event) {
            refreshDate();
        }
    );
});

function refreshDate() {
    var dd = $('#BirthDateD').val();
    var mm = $('#BirthDateM').val();
    var yyyy = $('#BirthDateY').val();
    if (dd.length == 2 && mm.length == 2 && yyyy.length == 4) {
        var today = new Date();
        var age = today.getFullYear() - Number(yyyy);
        $('#Age').val(today.getMonth() > Number(mm) || (today.getMonth() == Number(mm) && today.getDay() >= Number(dd)) ? age : age - 1);
    }
}

// Account deleting (close)
$(document).ready(function () {
    $('#cancelBtn').click(
        function (event) {
            $("#deletingModal").modal("hide");
        }
    );
});