var listItem = [
    {
        id: 472,
        name: 'iPhone Xs Max 64GB',
        price: 33990000,
        sku: 'SP000053',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/10/12/636749593270587915_iphoneXS-1o.png',
        link: '/iphone-xs-max-64gb',
        quantity: 3,
    },
    {
        id: 482,
        name: 'Samsung Galaxy S10+ (8 + 512GB)',
        price: 28990000,
        sku: 'SP000054',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2019/2/21/636863616938548520_ss-galaxy-s10-plus-daidien.png',
        link: '/samsung-galaxy-s10-plus-8-512gb',
        quantity: 4,
    },
    {
        id: 487,
        name: 'Xiaomi Redmi 6 3GB-32GB',
        price: 26990000,
        sku: 'SP000055',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/12/31/636818423658308841_xiaomi 6-3gb-64gb-xanh.png',
        link: '/dien-thoai/xiaomi-redmi-6-3gb-32gb',
        quantity: 2,
    },
    {
        id: 543,
        name: 'Samsung Galaxy M20',
        price: 4990000,
        sku: 'SP000032',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2019/2/13/636856463604969792_Galaxy-M20.png',
        link: '/dien-thoai/samsung-galaxy-m20',
        quantity: 2,
    },
    {
        id: 548,
        name: 'Samsung Galaxy S9 Plus Red 64GB',
        price: 19900000,
        sku: 'SP000037',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/12/18/636807396282758110_S9-RED.png',
        link: '/dien-thoai/samsung-galaxy-s9-plus-red-64gb',
        quantity: 43,
    },
    {
        id: 478,
        name: 'Samsung Galaxy S8 Plus',
        price: 17990000,
        sku: 'SP000039',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2017/9/7/636403755018028299_S8-S8-Plus.png',
        link: '/dien-thoai/samsung-galaxy-s8-plus',
        quantity: 3,
    },
    {
        id: 982,
        name: 'Samsung Galaxy A7 (2018)',
        price: 6990000,
        sku: 'SP000039',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/10/9/636746955613804701_A7-2018-.png',
        link: '/dien-thoai/samsung-galaxy-a7-2018',
        quantity: 3,
    },
    {
        id: 761,
        name: 'Samsung Galaxy A7 (2018)',
        price: 6990000,
        sku: 'SP000069',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/10/9/636746955613804701_A7-2018-.png',
        link: '/dien-thoai/samsung-galaxy-a7-2018',
        quantity: 23,
    },
    {
        id: 650,
        name: 'Samsung Galaxy A6+',
        price: 5990000,
        sku: 'SP000043',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/12/20/636809212091635486_ss-a6plus-daidien.png',
        link: '/dien-thoai/samsung-galaxy-a7-2018',
        quantity: 70,
    },
    {
        id: 439,
        name: 'iPhone 6s Plus 32GB',
        price: 10990000,
        sku: 'SP000010',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/1/27/636526515750427366_1o-6splus-32.png',
        link: '/dien-thoai/iphone-6s-plus-32gb',
        quantity: 70,
    },
];
var listProductChooseByTab = {
    'tab-1': [],
};
Number.prototype.formatNumber = function(n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};
var i = $(".list-size-order li").length + 1;
if (i > 1) {
    $(".btn-number-tab[data-type='minus'][data-field='quant[1]']").removeAttr('disabled')
}
function htmlBoxThanhToan(idTab) {
    var html = '<div class="row pos-order-info-container"><div class="col-md-12"><div class="pos-order-info-wrapper"><div class="pos-search-box"><button type="button" class="btn btn-link btn-number shadow-none btn-sm"><span class="fa fa-search m-0"></span></button><div id="divObjectInChargeFollow" class="flex-1"><select class="js-data-example-ajax-1 col-sm-12" id="ObjectInChargeFollow" placehoder></select></div><button type="button" class="btn btn-link btn-number shadow-none btn-sm"><span class="fa fa-user-plus m-0"></span></button></div></div><div class="d-flex f-direction-row"><button type="button" class="btn btn-primary btn-block rounded-0 p-4 btn_pos_create" onclick="thanhtoan()"><div class="row"><div class="col-6 bold text-left mt-1">Thanh toán</div><div class="col-6 text-right" style="flex-direction: column; align-items: flex-end;"><span class="total-price-tab large"></span></div></div></button><button type="button" class="btn btn-warning rounded-0" data-toggle="modal" data-target="#exampleModal-tab-' + idTab + '"><i class="fa fa-angle-right"></i></button></div></div></div>';
    return html;
}
function htmlBoxModalThanhToan(idTab) {
    idTab = 100;
    var html = '<div class="modal fade" id="exampleModal-tab-' + idTab + '" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel-tab-' + idTab + '" style="display: none;" aria-hidden="true"><div class="modal-dialog modal-md" role="document"><div class="modal-content"><div class="modal-header"><h5 class="modal-title" id="exampleModalLabel-tab-' + idTab + '">Chi tiết đơn hàng</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button></div><div class="modal-body pt-0 pb-0"><div class="row mt-2"><div class="col-md-12 mb-2">         </div></div></div><div class="modal-footer"><button onclick="ThanhToan(' + idTab + ')" type="button" class="btn btn-primary btn-sm">Lưu</button></div></div></div></div>';
    return html;
}
function renderAgainClass() {
    $(".js-data-example-ajax-1").select2({
        placeholder: "Nhập tên khách hàng, Số điện thoại",
        ajax: {
            url: "/TaskList/Home/LoadObjectInChargeType",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    ItemCode_KeySeach: params.term, // search term
                    itemCode: 'PERSON'
                };
            },
            processResults: function (data, params) {
                var formatData = $.map(data, function (obj) {
                    obj.id = obj.Code;
                    obj.text = obj.Name;
                    return obj;
                });
                return {
                    results: formatData,
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) {
            return markup;
        }, // let our custom formatter work
        minimumInputLength: 2,
    });
    $('.datepicker').datetimepicker({
        format: 'HH:mm DD/MM/YYYY',
        locale: 'vi',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
        }
    });

}
// chạy $(".js-data-example-ajax-1").select2()
renderAgainClass();
$('.btn-number-tab').on('click', function (e) {
    e.preventDefault();
    var fieldName = $(this).attr('data-field');
    var type = $(this).attr('data-type');
    var lengthTab = $(".list-size-order li").length;
    // tab-1
    var currentNameTab = $("#pills-tab li .active").attr('id');
    // 1
    var currentIdTab = currentNameTab ? currentNameTab.substr(4, currentNameTab.length) : 0;
    // tab cuối cùng vd tab-7 -> lastidtab 7
    var lastNameTab = $("#pills-tab li:last-child a").attr('id')
    var lastIdTab = lastNameTab ? lastNameTab.substr(4, currentNameTab.length) : 0;
    if (type == 'minus') {
        var minValue = 1;
        if (lengthTab > minValue) {
            // check div has bookmark
            if ($('#' + $(".list-size-order li .active").attr('id') + ' i').length == 0) {
                $("#tab-" + currentIdTab).parent().remove();
                $("#pills-tab-" + currentIdTab).remove();
                $(".list-size-order li:last-child a").addClass('active show');
                $("#pills-tabContent .tab-pane:last-child").addClass('active show');
            } else {
                notify('top', 'right', '', 'warning', 'animated fadeInLeft', 'animated fadeOutLeft', 'Thông báo', 'Vui lòng xóa hết danh sách hàng trước khi xóa đơn hàng');
            }
        }
        if ($(".list-size-order li").length == minValue) {
            $(this).attr('disabled', true);
        }

    } else if (type == 'plus') {
        var maxValue = 9999999999999;
        if (lengthTab < maxValue) {
            var add_size_order_tab = $('<li class="pos-order-nav-item"><a class="" id="tab-' + i + '" data-toggle="pill" href="#pills-tab-' + i + '" role="tab" aria-controls="pills-tab-' + i + '" aria-selected="false">#' + (lengthTab + 1) + '</a></li>');
            // add tab top
            $(add_size_order_tab).appendTo(".list-size-order").hide().fadeIn(300);

            var add_size_order_tab_content = $('<div class="tab-pane fade w-100" id="pills-tab-' + i + '" role="tabpanel" aria-labelledby="tab-' + i + '"><div class="accordion pos-order-list-wrapper flex-1" id="accordion-tab-' + i + '" role="tablist"></div>' + htmlBoxThanhToan(i) + htmlBoxModalThanhToan(i) + '</div>');
            // add tab content
            $(add_size_order_tab_content).appendTo(".tab-content");
            $(".list-size-order li a").removeClass('active show');
            $("#pills-tabContent .tab-pane").removeClass('active show');
            $(".list-size-order li:last-child a").addClass('active show');
            $("#pills-tabContent .tab-pane:last-child").addClass('active show');
            // ban đầu vào add tab mới thì vẫn disable khi có 2tab mới cho remove tab
            if (lengthTab > 0) {
                $(".btn-number-tab[data-type='minus'][data-field='" + fieldName + "']").removeAttr('disabled')
            }
            // chạy lại jquery $(".js-data-example-ajax-1").select2() cho box thanh toán mới
            renderAgainClass();
            i++;
        }
        if ($(".list-size-order li").length == maxValue) {
            $(this).attr('disabled', true);
        }
    }
    // change text div
    var j = 1;
    var add = '';
    $("#pills-tab li a").each(function () {
        if ($('#' + $(this).attr('id') + ' i')) {
            add = $('#' + $(this).attr('id') + ' i');
        }
        $(this).html('#' + j + ' ' + (add.prop('outerHTML') ? add.prop('outerHTML') : ''));
        j++;
        add = '';
    });
});
function renderAgainScript() {
    // cần phải off các sự kiện này k nó sẽ chạy 2 lần
    $(".btn-number").off("click");
    $(".input-number").off("change");
    $(".input-number").off("keydown");
    $(".input-discount-number").off("change");
    $(".input-discount-number").off("keydown");
    $(".display-input-quant").off("click");
    $(".mat-button-toggle-group input:radio").off("change");
    $(".remove-item").off("click");
    $('.btn-number').on('click',function(e){
        e.preventDefault();

        var fieldName = $(this).attr('data-field'); // tab-1-item-1
        var type      = $(this).attr('data-type');
        var input = $("input[name='"+fieldName+"']");
        var displayInput = $("#display-input-quant-"+fieldName);
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if(type == 'minus') {
                var minValue = parseInt(input.attr('min'));
                if(!minValue) minValue = 1;
                if(currentVal > minValue) {
                    input.val(currentVal - 1).change();
                    displayInput.text(currentVal - 1)
                }
                if(parseInt(input.val()) == minValue) {
                    $(this).attr('disabled', true);
                }

            } else if(type == 'plus') {
                var maxValue = parseInt(input.attr('max'));
                if(!maxValue) maxValue = 9999999999999;
                if(currentVal < maxValue) {
                    input.val(currentVal + 1).change();
                    displayInput.text(currentVal + 1)
                }
                if(parseInt(input.val()) == maxValue) {
                    $(this).attr('disabled', true);
                }

            }
        } else {
            input.val(0);
        }
    });
    $('.input-number').focusin(function(){
        $(this).data('oldValue', $(this).val());
    });
    $('.input-number').change(function() {
        var minValue =  parseInt($(this).attr('min'));
        var maxValue =  parseInt($(this).attr('max'));
        if(!minValue) minValue = 1;
        if(!maxValue) maxValue = 9999999999999;
        var valueCurrent = parseInt($(this).val());

        var name = $(this).attr('name');// tab-1-item-1
        if(valueCurrent >= minValue) {
            $(".btn-number[data-type='minus'][data-field='"+name+"']").removeAttr('disabled')
        } else {
            alert('Xin lỗi, cần phải mua số lượng ít nhất là 1');
            $(this).val($(this).data('oldValue'));
        }
        if(valueCurrent <= maxValue) {
            $(".btn-number[data-type='plus'][data-field='"+name+"']").removeAttr('disabled')
        } else {
            alert('Xin lỗi, quá số lượng bạn có thể mua');
            $(this).val($(this).data('oldValue'));
        }
        sumPriceItem(name);
    });
    $(".input-number").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $('.input-discount-number').focusin(function(){
        $(this).data('oldValue', $(this).val());
    });
    $('.input-discount-number').change(function() {
        var minValue =  parseInt($(this).attr('min'));
        var maxValue =  parseInt($(this).attr('max'));
        if(!minValue) minValue = 1;
        if(!maxValue) maxValue = 9999999999999;
        var valueCurrent = parseInt($(this).val());
        if(valueCurrent < minValue) {
            $(this).val(0);
        }
        if(valueCurrent > maxValue) {
            $(this).val($(this).data('oldValue'));
        }
        // lấy data-field vì name sẽ bị trùng với item size
        var nameTab = $(this).attr('data-field');// tab-1-item-1
        // gán giá trị để hiển thị
        var display = '';
        // nếu có giá trị khác 0
        if (parseInt($(this).val())) {
            // vnd || %
            var radioChoose = $("input:radio[name ='radio-" + nameTab + "']:checked").val();
            var valueDiscount = radioChoose == 'vnd' ? (parseInt($(this).val())) : ($("#price-" + nameTab).attr('data-field') * parseInt($(this).val())) / 100;
            display = ' Giảm (' + valueDiscount.formatNumber() + ')';
        }
        $('#display-input-discount-' + nameTab).text(display)
        $('#display-input-discount-' + nameTab).attr('data-discount', valueDiscount)
        sumPriceItem(nameTab);
    });
    $(".input-discount-number").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $('.display-input-quant').on('click',function(e){
        var fieldName = $(this).attr('data-field'); // tab-1-item-1
        var input = $("input[name='"+fieldName+"']");
        var currentVal = parseInt(input.val());
        // xóa disable dấu trừ
        var name = $(this).attr('name'); // tab-1-item-1
        $(".btn-number[data-type='minus'][data-field='"+name+"']").removeAttr('disabled')
        if (!isNaN(currentVal)) {
            var maxValue = parseInt(input.attr('max'));
            if(!maxValue) maxValue = 9999999999999;
            if(currentVal < maxValue) {
                input.val(currentVal + 1).change();
                $(this).text(currentVal + 1)
            }
            if(parseInt(input.val()) == maxValue) {
                $(this).attr('disabled', true);
            }
        } else {
            input.val(0);
        }
    });
    $('.mat-button-toggle-group input:radio').change(function(){ 
        var nameTab = $(this).attr('data-field'); // tab-1-item-1
        var radioChoose = $("input:radio[name ='radio-" + nameTab + "']:checked").val();
        if (radioChoose == 'vnd') {
            $("#input-discount-" + nameTab).attr('min', 0)
            $("#input-discount-" + nameTab).attr('max', $("#price-" + nameTab).attr('data-field')) // giá của sản phẩm
        } else {
            $("#input-discount-" + nameTab).attr('min', 0)
            $("#input-discount-" + nameTab).attr('max', 100)
        }
        $("#input-discount-" + nameTab).val('')
        $('#display-input-discount-' + nameTab).text('')
        sumPriceItem(nameTab);
    });
    $('.remove-item').on('click',function(e){
        $(this).parents(".box-pos-order-detail").remove();
        $(this).parents(".box-pos-order-detail").attr('id').split('-')[3];
        var idItemRemove = $(this).parents(".box-pos-order-detail").attr('id').split('-')[3];
        var currentNameTab = $("#pills-tab li .active").attr('id');
        var pos = listProductChooseByTab[currentNameTab].map(function (e) { return e.id; }).indexOf( parseInt(idItemRemove));
        if (pos >= 0) {
            listProductChooseByTab[currentNameTab].splice(pos, 1);
        }
        // đã xóa hết item trong tab xóa cờ
        if ($('.tab-content .tab-pane.active .box-pos-order-detail').length == 0) {
            $("#pills-tab li .active i").remove();
        }
        sumTotalPriceTabItem();
    });
    $('.input-note-item').change(function() {
        var nameTab = $(this).attr('data-field'); // tab-1-item-1
        sumPriceItem(nameTab);
    });
    sumTotalPriceTabItem();
}
function sumPriceItem(nameTab) { // tab-1-item-1
    var displayInput = $("#display-input-quant-"+nameTab);
    var price = $("#price-" + nameTab).attr('data-field');
    var size = parseInt($("input[name='"+nameTab+"']").val());
    var discount = $("#input-discount-" + nameTab).val() || 0;
    var radioChoose = $("input:radio[name ='radio-" + nameTab + "']:checked").val();
    var discountVND = (parseInt($("#display-input-discount-" + nameTab).attr('data-discount')) || 0) * size;
    var note = $("#input-note-" + nameTab).val();
    var totalPrice;
    if (radioChoose == 'vnd') {
        totalPrice = parseInt((price - discount) * size);
    } else if (radioChoose == '%') {
        totalPrice = parseInt(((price * (100 - discount)) / 100) * size);
    }
    $("#sum-price-" + nameTab).text(totalPrice.formatNumber());
    $("#sum-price-" + nameTab).attr('data-field', totalPrice);
    displayInput.text(size)
    // tinh tiền cho từng sản phẩm
    var pos = listProductChooseByTab['tab-' + nameTab.split('-')[1]].map(function (e) { return e.id; }).indexOf(parseInt(nameTab.split('-')[3]))
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].size = size;
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].discount = discount;
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].radioChoose = radioChoose;
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].totalPrice = totalPrice;
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].discountVND = discountVND;
    listProductChooseByTab['tab-' + nameTab.split('-')[1]][pos].note = note;
    sumTotalPriceTabItem();
}
// tính thanh toán
function sumTotalPriceTabItem() {
    // console.log('tinh tien')
    var totalPrice = 0;
    $('.tab-content .tab-pane.active .box-pos-order-detail').each(function () {
        totalPrice += parseInt($(this).find('.sum-price-tab').attr('data-field'));
    });
    $('.tab-content .tab-pane.active').find(".total-price-tab").text(totalPrice.formatNumber());
}

function LoadListItem(pageSize = 20, page) {
    $.ajax({
        type: "POST",
        url: '/TaskList/Home/LoadObjectInChargeType?itemCode=32143',
        success: function (response) {
            var html = '';
            // listItem = response;
            if (typeof listItem != 'undefined') {
                $.map(listItem, function (item) {
                    html += '<div class="col-lg-4 col-sm-6 px-1"><div class="product-item mb-2 pointer py-1" onclick=addItemToTab(' + item.id + ') title="' + item.name + '"><div class="product-item-img"><img src="' + item.image + '" alt="" class="img110"></div><div class="product-item-info"><div class="pos-product-item-text">' + item.name + '</div><span class="font-weight-bold text-right">' + item.price.formatNumber() + '</span></div></div></div>';
                });
            }
            $('#listProductItem').html(html);
        }
    });
}
renderAgainScript();
LoadListItem();
function addNewTab() {
    $(".btn-number-tab[data-type='plus']").click();
    $(".list-size-order li:last-child a").addClass('active show');
    $("#pills-tabContent .tab-pane:last-child").addClass('active show');
}
addNewTab();
function addItemToTab(id) {
    // tab-1
    var currentNameTab = $("#pills-tab li .active").attr('id');
    // 1
    var currentIdTab = currentNameTab.substr(4, currentNameTab.length);
    var lengthTab = $(".list-size-order li").length;
    pos = listItem.map(function (e) { return e.id; }).indexOf(id);
    var productChoose = {};
    if (pos >= 0) {
        productChoose = listItem[pos];
        // add vào tab
        if (typeof listProductChooseByTab[currentNameTab] == 'undefined') {
            listProductChooseByTab[currentNameTab] = [];
        }
        // check item in listProductChooseByTab
        var pos2 = listProductChooseByTab[currentNameTab].map(function (e) { return e.id; }).indexOf(productChoose.id);
        if (pos2 >= 0) {
            // nếu có item trong tab
            $('#display-input-quant-' + currentNameTab + '-item-' + productChoose.id).click();
        } else {
            // đặt cờ cho tab
            if ($('.tab-content .tab-pane.active .box-pos-order-detail').length == 0) {
                $("#pills-tab li a.active").html($("#pills-tab li a.active").text() + '<i class="fa fa-bookmark" data-toggle="tooltip" data-placement="bottom" title="" data-original-title="Đã có sản phẩm"></i>');
            }
            // push html vào tab
            listProductChooseByTab[currentNameTab].push(productChoose)
            var html = '';
            html += '<div class="box-pos-order-detail" id="tab-' + currentIdTab + '-item-' + productChoose.id +'"><div class="pos-order-list-item" role="tab" id="heading-tab-' + currentIdTab +'-item-' + productChoose.id +'"><div class="d-flex mb-0 w-100 align-items-center"><div class="quantity display-input-quant" id="display-input-quant-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'" name="display-input-quant-tab-' + currentIdTab + '-item-' + productChoose.id +'">1</div><a class="collapsed" data-toggle="collapse" href="#collapse-tab-' + currentIdTab + '-item-' + productChoose.id +'" aria-expanded="false" aria-controls="collapse-tab-' + currentIdTab + '-item-' + productChoose.id +'"><div class="productname"><span class="primary-text">' + productChoose.name + '</span><span id="price-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-field="' + productChoose.price + '" class="second-text">× ' + productChoose.price.formatNumber() +'<span id="display-input-discount-tab-' + currentIdTab + '-item-' + productChoose.id +'"></span></span></div><div class="subtotal"><span class="second-text">' + productChoose.sku + '</span><span id="sum-price-tab-' + currentIdTab + '-item-' + productChoose.id +'" class="sum-price-tab font-weight-bold" data-field="' + productChoose.price + '">' + productChoose.price.formatNumber() + '</span></div></a><div class=""><button type="button" class="btn btn-link p-3 remove-item"><span class="fa fa-times" style=""></span></button></div></div></div><div id="collapse-tab-' + currentIdTab + '-item-' + productChoose.id +'" class="collapse" role="tabpanel" aria-labelledby="heading-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-parent="#accordion-tab-' + currentIdTab +'"><div class="pos-order-item-container"><div class="pos-order-item-content"><h5 class="text-center text-uppercase">Số lượng</h5><div class="input-group mb-2"><span class="input-group-btn"><button type="button" class="btn btn-link btn-number shadow-none btn-lg" disabled="disabled" data-type="minus" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'"><span class="fa fa-minus m-0"></span></button></span><input type="text" name="tab-' + currentIdTab + '-item-' + productChoose.id +'" class="form-control form-control input-number text-center" value="1"><span class="input-group-btn"><button type="button" class="btn btn-link btn-number shadow-none btn-lg" data-type="plus" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'"><span class="fa fa-plus m-0"></span></button></span></div><div class="d-flex mb-2"><div class="d-flex align-items-center justify-content-start"><div class="font-weight-bold">Giảm giá</div></div><div class="col d-flex align-items-center justify-content-center"><div class="form-radio mat-button-toggle-group"><form class="d-flex"><div class="radio"><label class="pl-0 mb-0"><input type="radio" name="radio-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'" checked="checked" value="vnd"><div class="radio-switch-discount">đ</div></label></div><div class="radio"><label class="pl-0 mb-0"><input type="radio" name="radio-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'" value="%"><div class="radio-switch-discount">%</div></label></div></form></div></div><div class="d-flex align-items-center justify-content-end flex-column"><div class="f-s-11">Nhập giảm giá</div><input type="text" id="input-discount-tab-' + currentIdTab + '-item-' + productChoose.id +'" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'" class="form-control form-control-sm text-right width-10x input-discount-number" value="" min="0" max="' + productChoose.price +'"></div></div><div class=""><input type="text" id="input-note-tab-' + currentIdTab + '-item-' + productChoose.id + '" class="form-control form-control-md form-bg-inverse input-note-item" data-field="tab-' + currentIdTab + '-item-' + productChoose.id +'" placeholder="Ghi chú"></div></div></div></div></div>';
            $('.tab-content .tab-pane.active .pos-order-list-wrapper').append(html);
            sumPriceItem('tab-' + currentIdTab + '-item-' + productChoose.id)  // tab-1-item-1
        }
        renderAgainScript();
    }
}
function thanhtoan() {
    // tab-1
    var currentNameTab = $("#pills-tab li .active").attr('id');
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    var hhmm = today.toLocaleTimeString().substr(0, 5);
    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = hhmm + ' ' + dd + '/' + mm + '/' + yyyy;
    var obj = {};
    // thu ngân
    obj.cashierName = "Vũ Thanh Bình";
    obj.currentDate = today;
    // get data từ search ajax
    obj.customer = {
        address: null,
        code: "27050",
        contact: "",
        customerName: "Nguyễn  Hoàng Thái",
        email: "hoangthai1331995@gmail.com",
        phone: "0569123448",
        remainPoint: 0,
        taxCode: "",
    };
    // obj.debt = 0; // món nợ
    // obj.discount = listProductChooseByTab[currentNameTab].length && listProductChooseByTab[currentNameTab].reduce(function (accumulator, currentValue) {
    //     return accumulator.discountVND + currentValue.discountVND
    // });
    // discount này lấy theo value input của form thanh toán k liên quan đến discount từng món
    obj.discount = 0;
    obj.discountPencent = 0;
    // obj.earnedPoints = 0; // điểm tích lũy
    // obj.fee = 0;
    // obj.initialPaid = 33213213; // trả ban đầu
    obj.items = listProductChooseByTab[currentNameTab];
    obj.note = [{
        content: "ghi chú tổng",
    }];
    // obj.oldDebt = 0;
    // obj.paid = 33213213;
    // obj.residual = 0; // dư
    obj.saleOrderCode = "PX000016"; // mã hóa đơn
    obj.storeAddress = '325 Huỳnh Tấn Phát';
    obj.storeName = "FPT Shop";
    obj.storePhone = "090912345";
    obj.subtotal = listProductChooseByTab[currentNameTab].length && listProductChooseByTab[currentNameTab].reduce(function (accumulator, currentValue) {
        return accumulator.totalPrice + currentValue.totalPrice
    });
    obj.total = listProductChooseByTab[currentNameTab].length && listProductChooseByTab[currentNameTab].reduce(function (accumulator, currentValue) {
        return accumulator.totalPrice + currentValue.totalPrice
    });
    obj.totalBeforeVat = listProductChooseByTab[currentNameTab].length && listProductChooseByTab[currentNameTab].reduce(function (accumulator, currentValue) {
        return accumulator.totalPrice + currentValue.totalPrice
    });
    // obj.totalDebt = 0;
    obj.totalQuantity = listProductChooseByTab[currentNameTab].length && listProductChooseByTab[currentNameTab].reduce(function (accumulator, currentValue) {
        return accumulator.size + currentValue.size
    });
    obj.transactionDate = today;
    obj.vat = 0;
    console.log(543, obj)
}