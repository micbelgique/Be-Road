function getPrivateIP(callBack) {
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


//Info
function displayMICDetails(micId, data) {
    var modal = $('#accessInfoModal');
    modal.modal();
    modal.find('.modal-title').text("Acces info for " + data.Value);
    modal.find('#id').attr("value", micId);
    modal.find('#dataId').attr("value", data.Id);
    getPrivateIP(function cb(ip) {
        modal.find('#ip').attr("value", ip);
    });
}

//Info
function displayAccessInfoPopup(dataName, data) {
    var modal = $('#accessInfoModal');
    modal.modal();
    modal.find('.modal-title').text("Acces info for " + data.Value);

    modal.find('form').submit(function (evt) {
        getPrivateIP(function cb(ip) {
            //int? dataId, string name, string reason, string ip
            const dataId = data.Id;
            const name = modal.find('#name').val();
            const reason = modal.find('#reason').val();
            console.log({ dataId: dataId, name: name, reason: reason, ip: ip });
            $.post('/MIC/AddAccessInfo', { dataId: dataId, name: name, reason: reason, ip: ip },
                function (returnedData) {
                    console.log(returnedData);
                    $('#trainee span.' + dataName).text(":" + data.Value);
                });
            modal.find('#reason').val("");
            modal.modal('toggle');
        });
        evt.preventDefault();
        modal.find('form').unbind('submit');
    });
}
