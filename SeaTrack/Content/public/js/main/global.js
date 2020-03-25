$(document).ready(function () {

	setupMap(_def_Lat, _def_Lng, _def_zoom);
	setup_DataTable();
	setup_selectDataLine();
	

	$('[data-toggle="tooltip"]').tooltip();
	$('[data-toggle="popover"]').popover();

	$('#sidebarCollapse').on('click', function () {
		$('#s-sidebar').toggleClass('active');
		$('.s-sidebar').toggleClass('active');
		$('#menubar').toggleClass('active');
		$('#s-logo-menu').toggleClass('d-none');
		$("#sidebarCollapse").addClass("d-none");
		$("#giamsat_toggle").removeClass("d-none");
	});
	$('#giamsat_toggle').on('click', function () {
		//$( "#sidebarCollapse" ).trigger( "click" );
		$('#s-sidebar').toggleClass('active');
		$('.s-sidebar').toggleClass('active');
		$('#menubar').toggleClass('active');
		$('#s-logo-menu').toggleClass('d-none');
		$("#sidebarCollapse").removeClass("d-none");
		$("#giamsat_toggle").addClass("d-none");
	});



	$('#s-info-button').on('click', function () {
		$('#popUpBox_theMeaningOfIcon').toggleClass('d-none');
	});
	$("#closeMeaningOfIcon").on('click',function(){
		$('#s-info-button').trigger('click');
	})
	$('#sublefttab_1').mouseenter(function () {
		$('#sublefttab_1').addClass('s-over-width');
	});
	$('#sublefttab_1').mouseleave(function () {
		$('#sublefttab_1').removeClass('s-over-width');
	});



	$('input[name="datefilter"]').daterangepicker({
		autoUpdateInput: true,
		locale: {
			cancelLabel: 'Đóng',
			format: 'dd/mm/yyyy'
		}
	});
	$('input[name="datefilter"]').on('apply.daterangepicker', function (ev, picker) {
		$(this).val(picker.startDate.format('dd/MM/yyyy') + ' - ' + picker.endDate.format('dd/MM/yyyy'));
	});
	$('input[name="datefilter"]').on('cancel.daterangepicker', function (ev, picker) {
		$(this).val('');
	});
	$('.form_datetime').datetimepicker({
		language:  'vi',
		weekStart: 1,
		todayBtn: 1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		forceParse: 0,
		showMeridian: 1,
		format: 'dd-mm-yyyy'
	});
	$('.form_date').datetimepicker({
		language: 'vi',
		weekStart: 1,
		todayBtn: 1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		minView: 2,
		forceParse: 0,
		format: 'dd-mm-yyyy'
	});
	$('.form_time').datetimepicker({
		language: 'vi',
		weekStart: 1,
		todayBtn: 1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 1,
		minView: 0,
		maxView: 1,
		forceParse: 0,
		format: 'dd-mm-yyyy'
	});

	$("#player").disabled = true;
	$("#speed").disabled = true;

	$("#show_info_device_exp").click(function () {
		$("#btn_modal_exp").click();
	});

	$("#item-doithongtin-thietbi").click(function () {
		alert("Chức năng đang tạm khóa");
	});

	$("#feedback_send").click(function () {
		var cm = $.trim($("#feedback_comment").val());
		var _chude = $("#feedback_chude").val();
		insertDB_Provider(cm, _danhgia, _chude);
	});
	$("#show_admin_reload").click(function () {
		getinfoUser();
		$("#list-admin").show()
	})


	$("#show_bgt").click(function () {
		$("#list-bgt").show()
		$("#rounded").hide()
		$("#list_business").hide()
		$("#content_comment").hide()
		$("#list-admin").hide()
		$("#list-quantri").hide()
		$("#list-tienich").hide()
		$("#content-taikhoan").hide()
		$("#content-thietbi-taixe").hide()
		$("#menu-bgt-chitiet-vipham4h").click()
	});
	$("#show_business").click(function () {
		$("#list_business").show()
		$("#content_comment").hide()
		$("#list-bgt").hide()
		$("#rounded").hide()
		$("#list-quantri").hide()
		$("#list-admin").hide()
		$("#list-tienich").hide()
		$("#content-taikhoan").hide()
		$("#content-thietbi-taixe").hide()
		$("#menu-dn-tonghop").click()
	});
	$("#show_comment").click(function () {
		$("#content_comment").show()
		$("#list_business").hide()
		$("#list-bgt").hide()
		$("#rounded").hide()
		$("#list-quantri").hide()
		$("#list-admin").hide()
		$("#list-tienich").hide()
		$("#content-taikhoan").hide()
		$("#content-thietbi-taixe").hide()
	});
	$("#show_admin").click(function () {
		//getinfoUser();
		$("#list-admin").show()
		$("#list-bgt").hide()
		$("#list_business").hide()
		$("#rounded").hide()
		$("#content_comment").hide()
		$("#list-quantri").hide()
		$("#list-tienich").hide()
		$("#content-taikhoan").hide()
		$("#content-thietbi-taixe").hide()
	});
	$("#show_tienich").click(function () {
		$("#list-tienich").show()
		$("#list-quantri").hide()
		$("#list-admin").hide()
		$("#list-bgt").hide()
		$("#list_business").hide()
		$("#rounded").hide()
		$("#content_comment").hide()
		$("#content-taikhoan").hide()
		$("#content-thietbi-taixe").hide()
		$("#menu-tienich-thongtinthietbi").click()
	});
	$("#show_taikhoan").click(function () {
		$("#content-taikhoan").show();
		$("#content-thietbi-taixe").hide();
		$("#list-tienich").hide();
		$("#list-quantri").hide();
		$("#list-admin").hide();
		$("#list-bgt").hide();
		$("#list_business").hide();
		$("#rounded").hide();
		$("#content_comment").hide();
		$("#item-taikhoan-save").hide();
		$("#item-taikhoan-back").hide();
		//$("#sidebarCollapse").click();
		GetInfo_User();
	});
	$("#show_thietbi_taixe").click(function () {
		$("#content-thietbi-taixe").show()
		$("#content-taikhoan").hide()
		$("#list-tienich").hide()
		$("#list-quantri").hide()
		$("#list-admin").hide()
		$("#list-bgt").hide()
		$("#list_business").hide()
		$("#rounded").hide()
		$("#content_comment").hide()
		//GetList_Devices();
		//GetList_loaixe_forDDL();
		//GetList_tinh_forDDL();
		//GetList_loaivantai_forDDL();
		//GetList_nhomxe_forDDL();
	});

	$("#menu-tienich-thongtinthietbi").click(function () {
		$("#content-tienich-thongtinthietbi").show()
	});

	$("#menu-bgt-chitiet-vipham4h").click(function () {
		$("#content-bgt-chitiet-vipham4h").show()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_vipham4h(dataListStatus);
	});
	$("#menu-bgt-chitiet-vipham10h").click(function () {
		$("#content-bgt-chitiet-vipham10h").show()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_vipham10h(dataListStatus);
	});
	$("#menu-bgt-hanhtrinh").click(function () {
		$("#content-bgt-hanhtrinh").show()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_hanhtrinh(dataListStatus);
	});
	$("#menu-bgt-tocdo").click(function () {
		$("#content-bgt-tocdo").show()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_tocdo(dataListStatus);
	});
	$("#menu-bgt-quatocdogioihan").click(function () {
		$("#content-bgt-quatocdogioihan").show()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_quatocdogioihan(dataListStatus);
	});
	$("#menu-bgt-thoigianlaixelientuc").click(function () {
		$("#content-bgt-thoigianlaixelientuc").show()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_laixelientuc(dataListStatus);
	});
	$("#menu-bgt-thoigianlaixetrongngay").click(function () {
		$("#content-bgt-thoigianlaixetrongngay").show()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		//CreateSelect_bgt_laixetrongngay(dataListStatus);
	});
	$("#menu-bgt-dungdo").click(function () {
		$("#content-bgt-dungdo").show()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-tonghopxe").hide()
		//CreateSelect_bgt_dungdo(dataListStatus);
	});
	$("#menu-bgt-laixe").click(function () {
		$("#content-bgt-laixe").show()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		$("#content-bgt-tonghopxe").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		//CreateSelect_bgt_laixe(dataListStatus);
	});
	$("#menu-bgt-tonghopxe").click(function () {
		$("#content-bgt-tonghopxe").show()
		$("#content-bgt-laixe").hide()
		$("#content-bgt-dungdo").hide()
		$("#content-bgt-thoigianlaixelientuc").hide()
		$("#content-bgt-thoigianlaixetrongngay").hide()
		$("#content-bgt-chitiet-vipham4h").hide()
		$("#content-bgt-chitiet-vipham10h").hide()
		$("#content-bgt-hanhtrinh").hide()
		$("#content-bgt-tocdo").hide()
		$("#content-bgt-quatocdogioihan").hide()
		//CreateSelect_bgt_tonghopxe(dataListStatus);
	});

	$("#menu-dn-tonghop").click(function () {
		$("#content-dn-tonghop").show()
		$("#content-dn-dungdo").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_tonghop(dataListStatus);
	});
	$("#menu-dn-chitiet").click(function () {
		$("#content-dn-chitiet").show()
		$("#content-dn-tonghop").hide()
		$("#content-dn-dungdo").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_chitiet(dataListStatus);
	});
	$("#menu-dn-nhienlieu-tonghop").click(function () {
		$("#content-dn-nhienlieu-tonghop").show()
		$("#content-dn-dungdo").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_nhienlieu_tonghop(dataListStatus);
	});
	$("#menu-dn-khoadien").click(function () {
		$("#content-dn-khoadien").show()
		$("#content-dn-mototai").hide()
		$("#content-dn-dungdo").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_khoadien(dataListStatus);
	});
	$("#menu-dn-mototai").click(function () {
		$("#content-dn-mototai").show()
		$("#content-dn-khoadien").hide()
		$("#content-dn-dungdo").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_mototai(dataListStatus);
	});
	$("#menu-dn-dungdo").click(function () {
		$("#content-dn-dungdo").show()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		//CreateSelect_dn_dungdo(dataListStatus);
	});
	$("#menu-dn-tonghopkm").click(function () {
		$("#content-dn-tonghopkm").show()
		$("#content-dn-dungdo").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-maylanh").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()

		//CreateSelect_dn_tonghopkm(dataListStatus);
	});
	$("#menu-dn-maylanh").click(function () {
		$("#content-dn-maylanh").show()
		$("#content-dn-dungdo").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-ngugat").hide()
		$("#content-dn-tonghopkm").hide()

		//CreateSelect_dn_maylanh(dataListStatus);
	});
	$("#menu-dn-camerahanhtrinh").click(function () {
		window.open('PlayBackJourney?Cur=&im=&d=', "_blank");
	});
	$("#menu-dn-ngugat").click(function () {
		$("#content-dn-ngugat").show()
		var currentdate_dozy = new Date();
		var curentMonth_dozy = (currentdate_dozy.getMonth() + 1) < 10 ? "0" + (currentdate_dozy.getMonth() + 1) : (currentdate_dozy.getMonth() + 1);
		var curentdate_dozy = currentdate_dozy.getDate() < 10 ? "0" + currentdate_dozy.getDate() : currentdate_dozy.getDate();
		var datetimeDozy = curentdate_dozy + "-" + +curentMonth_dozy + "-" + currentdate_dozy.getFullYear();
		$("#date_form_dozy").value = datetimeDozy;
		$("#content-dn-camerahanhtrinh").hide()
		$("#content-dn-dungdo").hide()
		$("#content-dn-mototai").hide()
		$("#content-dn-khoadien").hide()
		$("#content-dn-tonghop").hide()
		$("#content-dn-chitiet").hide()
		$("#content-dn-nhienlieu-tonghop").hide()
		$("#content-dn-tonghopkm").hide()
		$("#content-dn-maylanh").hide()
		//CreateSelect_dn_ngugat(dataListStatus);
	});
});


var _infowindow = new google.maps.InfoWindow();
var service = new google.maps.DirectionsService();