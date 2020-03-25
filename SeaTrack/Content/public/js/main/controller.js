var _listDeviceStatus = [];
updateListDeviceStatus();
setInterval(function () { updateListDeviceStatus(); }, 10000);
function win_reload() {
    window.location.reload();
}
function ActionMenu(index) {
    switch (index) {
        case 1:
            location.href = "/Home/Logout";
            break;
        default:
            break;
    }
};
function toHaily(a) {
    return a * 0.53996;
}
function updateListDeviceStatus() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetListDeviceStatus',
        data: {},
        success: function (data, txtStatus, XMLHttpRequest) {
            _listDeviceStatus = data.Result;
        }
    }, "json");
}
function attachInforwindows(marker, string_) {
    var infowin = new google.maps.InfoWindow({
        content: 'Đang cập nhật dữ liệu!',
    });
    infowin.setContent(string_);
    marker.addListener('click', function () {
        infowin.open(map, marker);
    });

    _armarker.push(marker);
    _infowins.push(infowin);
}
function drawingLinePoint(listStop, id, a) {
    cleanMap(0);
    var flightPath = new google.maps.Polyline({
        path: listStop,
        geodesic: true,
        strokeColor: '#14a84e',
        strokeOpacity: 1.0,
        strokeWeight: 1
    });
    flightPath.setMap(map);

    _flightPath.push(flightPath);

    var i = listStop.length - 1;
    for (i; i > 0; i--) {
        var point = new google.maps.LatLng(listStop[i].lat, listStop[i].lng);
        var marker = new MarkerWithLabel({
            position: point,
            icon: "/Content/public/img/icon/marker_ef.png",
            //labelContent: i + " lat: " + listStop[i].lat,
        });
        marker.setMap(map);

        var content_ = '<div class="">Tọa độ: <hr>' + listStop[i].lat + ' - ' + listStop[i].lng + '</div>';
        attachInforwindows(marker, content_);

    }

    _device = checkDevice(id);
    var point = new google.maps.LatLng(listStop[0].lat, listStop[0].lng);
    var marker = new MarkerWithLabel({
        position: point,
        icon: "/Content/public/img/icon/marker_ex.png",
    });
    marker.setMap(map);
    var infowin = new google.maps.InfoWindow({
        content: 'Đang cập nhật dữ liệu!',
    });
    infowin.setContent('<div class="">' + getInfoWindow(_device) + '</div>');
    marker.addListener('click', function () {
        infowin.open(map, marker);
    });
    _armarker.push(marker);
    _infowins.push(infowin);

    if (a == 1) {
        map.panTo(point);
        map.setZoom(6);
    }

    console.log("Again");

}
function newPointToLine(point) {
    var path = flightPath.getPath();
    path.push(point);
    flightPath.setPath(path);

    var markerLB = new MarkerWithLabel({
        position: point,
        icon: "/Content/public/img/icon/marker_ex.png",
        map: map
    });
}
function getInfoWindow(_dv) {
    var strStatus = '';
    var dt = new Date(parseInt(_dv.TransmitTime.substr(6)));
    var dte = dt.getDate() + '/' + (dt.getMonth() + 1) + '/' + dt.getFullYear()
        + ' ' + dt.getHours() + ':' + dt.getMinutes();
    strStatus += 'Tên thiết bị: <b>' + _dv.DeviceName + '</b><br>'
        + 'Thời gian: <b>' + dte + '</b><br>';
    strStatus += 'Trạng thái: <b>' + status + '</b><br>';
    strStatus += 'Toạ độ: <b>' + _dv.Latitude
        + ' (' + _dv.DirectionSN + ')</b> - <b>'
        + _dv.Longitude + ' (' + _dv.DirectionEW + ')</b><br>';
    strStatus += 'Tốc độ: <b>' + toHaily(_dv.Speed) + '</b> Hải lý / giờ <br>';
    return strStatus;
}
function checkDevice(id) {
    i = 0;
    while (i < _listDeviceStatus.length) {
        if (_listDeviceStatus[i].DeviceID == id) return _listDeviceStatus[i];
        i++;
    }
    return 0;
}
function makePoint(id, icon = "") {
    if (icon == "") icon = "/Content/public/img/icon/marker_ex.png";
    cleanMap(1);
    var i = 0;
    var lasted;
    var _device;
    _device = checkDevice(id);
    console.log(_device);
    if (_device != 0) {
        point = new window.google.maps.LatLng(_device.Latitude, _device.Longitude);

        marker = new MarkerWithLabel({
            position: point,
            icon: "/Content/public/img/icon/marker_ex.png",
        });
        var infowin = new google.maps.InfoWindow({
            content: 'Đang cập nhật dữ liệu!',
        });

        infowin.setContent('<div class="">' + getInfoWindow(_device) + '</div>');
        google.maps.event.addListener(marker, 'click', function () {
            infowin.open(map, marker);
        });
        _infowins.push(infowin);
        map.panTo(point);
        map.setZoom(6);
        _armarker.push(marker);
        marker.setMap(map);
    }
    else alert("Chưa có dữ liệu, vui lòng thử lại sau");
}
function makeListStop(list) {
    var re = [];
    for (var i = 0; i < list.length; i++) {
        re[i] = { lat: list[i].Latitude, lng: list[i].Longitude };
    }
    return re;
}
function setdrawingLinePoint(a = 0) {
    var id = $("#list_xelotrinh").val();
    var from = $("#date_form_d").val() + " " + $("#date_form_h").val() ;
    var to = $("#date_t_d").val() + " " + $("#date_t_h").val();
    var list;
    $.ajax({
        type: 'GET',
        url: '/Home/GetRoadmapByDateTime',
        data: { deviceID:id,From:from,To:to  },
        success: function (data, txtStatus, XMLHttpRequest) {
            list = data.Result;
            if (list == null) {
                alert("Chưa có dữ liệu cho phạm vi thời gian đã chọn");
            } else {
                //console.log(list);
                var listStop = makeListStop(list);
                drawingLinePoint(listStop, id, a);
                if (list.length > 0) {
                    var _tbl = "";
                    for (var i = 0; i < list.length; i++) {
                        var dt = new Date(parseInt(list[i]["TransmitTime"].substr(6)));
                        var dte = dt.getDate() + '/' + (dt.getMonth() + 1) + '/' + dt.getFullYear()
                            + ' ' + dt.getHours() + ':' + dt.getMinutes();
                        _tbl += '<tr id="tr' + list[i]["DeviceID"] + i + '"><td>'
                            + list[i]["Latitude"] + '.' + list[i]["DirectionEW"] + '  '
                            + list[i]["Longitude"] + '.' + list[i]["DirectionSN"]
                            + '</td><td>' + toHaily(list[i]["Speed"]) + '</td><td>' + dte + '</td></tr>';
                    }
                    $("#tblbodydataline").append(_tbl);
                }
            }
        },
    }, "json");
}
function interval_draw() {
    //_interval = setInterval(function () { setdrawingLinePoint() }, 50000);
}
function setup_DataTable() {
    var dad = [];
    $.ajax({
        type: 'GET',
        url: '/Home/GetListDeviceStatus',
        data: {},
        success: function (data, txtStatus, XMLHttpRequest) {
            dad = data.Result;
            //console.log(data);
            if (dad.length > 0) {

                var _tb = "";
                for (var i = 0; i < dad.length; i++) {
                    var dt = new Date(parseInt(dad[i]["TransmitTime"].substr(6)));
                    var dte = dt.getDate() + '/' + (dt.getMonth() + 1) + '/' + dt.getFullYear()
                        + ' ' + dt.getHours() + ':' + dt.getMinutes();

                    _tb += '<tr id="tr' + dad[i]["DeviceID"]
                        + '" classname="groupXe" onclick="makePoint('+ dad[i]["DeviceID"]
                        + ');" data-toggle="" data-placement="right" data-html="true" class="tr_hover_select">'
                        + '<td class="alignCenter">'
                        + (i + 1) + '</td><td>'
                        + '<img src="/Content/public/img/Xe-khach/' + _stt_cmd[1]["img"] + '">  '
                        + dad[i]["DeviceName"]
                        + '</td><td>' + toHaily(dad[i]["Speed"])
                        + '</td><td>' + dte + '</td><td>'
                        + dad[i]["Latitude"] + "." + dad[i]["DirectionSN"] + " - "
                        + dad[i]["Longitude"] + "." + dad[i]["DirectionEW"] 
                        + '</td></tr>';
                }
                $("#tblbodydata").append(_tb);
            }
        }
    }, "json");
   
}
function setup_selectDataLine() {
    var dad = [];
    $.ajax({
        type: 'GET',
        url: '/Home/GetListDeviceStatus',
        data: {},
        success: function (data, txtStatus, XMLHttpRequest) {
            dad = data.Result;
            if (dad.length > 0) {
                var _tb = '<option value="0">Tất cả</option>';
                
                for (var i = 0; i < dad.length; i++) {
                    _tb += '<option value="' + dad[i]["DeviceID"] + '">' + dad[i]["DeviceName"] + '</option>';
                }
                $("#list_xelotrinh").append(_tb);
            }
        }
    });
}
function setupMap(lat, lng, mapZoom) {
    var mapLatlng = new google.maps.LatLng(lat, lng);
    var myOptions = {
        zoom: mapZoom,
        center: mapLatlng,
        mapTypeControl: false,
        streetViewControl: true,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        fullscreenControl: false,
        fullscreenControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.RIGHT_CENTER
        },
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"), myOptions);

    var styles = {
        default: null,
        night: [
            { elementType: 'geometry', stylers: [{ color: '#242f3e' }] },
            { elementType: 'labels.text.stroke', stylers: [{ color: '#242f3e' }] },
            { elementType: 'labels.text.fill', stylers: [{ color: '#746855' }] },
            {
                featureType: 'administrative.locality',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563' }]
            },
            {
                featureType: 'poi',
                elementType: 'labels.icon',
                stylers: [{ visibility: 'off' }]
            },
            {
                featureType: 'road',
                elementType: 'geometry',
                stylers: [{ color: '#38414e' }]
            },
            {
                featureType: 'road',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#212a37' }]
            },
            {
                featureType: 'road',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#9ca5b3' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'geometry',
                stylers: [{ color: '#746855' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#1f2835' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#f3d19c' }]
            },
            {
                featureType: 'transit',
                elementType: 'geometry',
                stylers: [{ color: '#2f3948' }]
            },
            {
                featureType: 'transit.station',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563' }]
            },
            {
                featureType: 'water',
                elementType: 'geometry',
                stylers: [{ color: '#17263c' }]
            },
            {
                featureType: 'water',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#515c6d' }]
            },
            {
                featureType: 'water',
                elementType: 'labels.text.stroke',
                stylers: [{ color: '#17263c' }]
            }
        ],
    };

    var styleSelector = document.getElementById('style-selector');
    map.setOptions({
        styles: styles[styleSelector.value]
    });

    styleSelector.addEventListener('change', function () {
        if (styleSelector.value === "satellite") { map.setMapTypeId('hybrid'); }
        else {
            map.setMapTypeId('roadmap');
            map.setOptions({ styles: styles[styleSelector.value] });
        }
    });
};
function cleanMap(a = 0) {
    if (a == 1) {
        clearInterval(_interval);
    }
    if (_armarker.length > 0) {
        for (i = 0; i < _armarker.length; i++) {
            _armarker[i].setMap(null);
        }
        if (a == 0) _armarker = [];
    }
    if (_flightPath.length > 0) {
        for (j = 0; j < _flightPath.length; j++) {
            _flightPath[j].setMap(null);
        }
        if (a == 0) _flightPath = [];
    }

}
function ListDeviceSearch(id_search, list_result) {
    var _id_search, _list_result, filter, tr, td;
    _id_search = document.getElementById(id_search);
    _list_result = document.getElementById(list_result);
    tr = _list_result.getElementsByTagName('tr');
    filter = _id_search.value.toUpperCase();

    for (var i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td')[1];
        if (td.innerText.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
        }
    }
};




function setDate() {
    if (positionView == 0) {
        positionView = 1;
        if (armarker.length > 0) {
            for (var b = 0; b < armarker.length; b++) {
                armarker[b][1].setMap(null);
            }
            if (listdata != null) {
                if (listdata.length > 0) {
                    if (line) line.setMap(null);
                    if (line1) line1.setMap(null);
                    SetupLine(listdata, _CategoryID);
                    drawPointStop(listData_PauseStop);
                }
            }
        }
    }
    var currentdate = new Date();
    var curentMonth = (currentdate.getMonth() + 1) < 10 ? "0" + (currentdate.getMonth() + 1) : (currentdate.getMonth() + 1);
    var curentdate = currentdate.getDate() < 10 ? "0" + currentdate.getDate() : currentdate.getDate();
    var curentHour = currentdate.getHours() < 10 ? "0" + currentdate.getHours() : currentdate.getHours();
    var curentMinute = currentdate.getMinutes() < 10 ? "0" + currentdate.getMinutes() : currentdate.getMinutes();
    var curentSecond = currentdate.getSeconds() < 10 ? "0" + currentdate.getSeconds() : currentdate.getSeconds();
    var datetimeF = curentdate + "-" + +curentMonth + "-" + currentdate.getFullYear();
    // var datetimeF = currentdate.getFullYear() + "-" + curentMonth + "-" + curentdate + "T00:" + "00:00";
    //var datetimeF = currentdate.getFullYear() + "-" + curentMonth + "-" + curentdate;
    // var datetimeE = currentdate.getFullYear() + "-" + curentMonth + "-" + curentdate + "T" + curentHour + ":" + curentMinute + ":" + curentSecond;

    document.getElementById("date_form_h").value = "00:00";
    document.getElementById("date_form_d").value = datetimeF;
    document.getElementById("date_t_h").value = "23:59";
    document.getElementById("date_t_d").value = datetimeF;
};

// USER
function GetInfo_User() {
    //var obj = document.querySelector('.preloader'), inner = document.querySelector('.preloader_inner'), page = document.querySelector('.main');
    //obj.classList.add('show');
    //page.classList.remove('show');

    $.ajax({
        type: 'GET',
        dataType: "json",
        url: '/Home/UserInfo',
        data: {},
        success: function (data, txtStatus, XMLHttpRequest) {
            console.log(data);
            //var obj = document.querySelector('.preloader'), inner = document.querySelector('.preloader_inner'), page = document.querySelector('.main');
            //obj.classList.add('show');
            //page.classList.remove('show');

            if (data != null) {

                if (data["RoleID"] == 3) {
                    $("#item-taikhoan-them").show()
                    $("#item-taikhoan-sua").show()
                    $("#item-taikhoan-xoa").show()
                    $("#item-taikhoan-status").hide()
                    //GetList_User_Refresh();
                    console.log(1);
                }
                else if (data["RoleID"] == 4) {
                    $("#item-taikhoan-them").hide()
                    $("#item-taikhoan-sua").hide()
                    $("#item-taikhoan-xoa").hide()
                    $("#item-taikhoan-status").show()
                    var _tbody_taikhoan = document.getElementById("tbody_taikhoan");
                    _tbody_taikhoan.innerHTML = "";
                    var _tr = document.createElement("tr");
                    var _td = document.createElement("td");
                    _td.setAttribute("colspan", "9");
                    _td.innerHTML = "Bạn không có quyền xem danh sách tài khoản";
                    _tr.appendChild(_td);
                    _tbody_taikhoan.appendChild(_tr);
                }

                $("#thongtin_nguoitao").val(data["CreateBy"]);
                console.log(2);
                $("#thongtin_taikhoan").val(data["Username"]);
                console.log(3);
                $("#thongtin_tenkhachhang").val(data["FullName"]);
                console.log(4);
                $("#thongtin_sodienthoai").val(data["Phone"]);
                console.log(5);
                $("#thongtin_email").val("");
                console.log(6);
                $("#thongtin_diachi").val(data["Address"]);
                console.log(7);
                

            } else { alert("Không có dữ liệu thông tin tài khoản"); }

            //obj.classList.remove('show');
            //page.classList.add('show');
        }
    });

};