function updateListDeviceStatus() {
    $.ajax({
        type: 'GET',
        url: 'Admin/Device/GetListDevice',
        data: {},
        success: function (data, txtStatus, XMLHttpRequest) {
            _listDeviceStatus = data.Result;
        }
    }, "json");
}
function setup_DataTable() {
    var dad = [];
    $.ajax({
        type: 'GET',
        url: 'Admin/Device/GetListDeviceStatus',
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
                        + '" classname="groupXe" onclick="makePoint(' + dad[i]["DeviceID"]
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