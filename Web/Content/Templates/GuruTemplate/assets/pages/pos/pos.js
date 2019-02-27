var listItem = [
    {
        id: 472,
        name: 'iPhone Xs Max 64GB',
        price: 33990000,
        maxsize: 10,
        sku: 'SP000053',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/10/12/636749593270587915_iphoneXS-1o.png',
        link: '/iphone-xs-max-64gb',
    },
    {
        id: 482,
        name: 'Samsung Galaxy S10+ (8 + 512GB)',
        price: 28990000,
        maxsize: 15,
        sku: 'SP000054',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2019/2/21/636863616938548520_ss-galaxy-s10-plus-daidien.png',
        link: '/samsung-galaxy-s10-plus-8-512gb',
    },
    {
        id: 487,
        name: 'Xiaomi Redmi 6 3GB-32GB',
        price: 26990000,
        maxsize: 10,
        sku: 'SP000055',
        image: 'https://img.fpt.shop/ImageResize/192x192/resize/normal/max/https://fptshop.com.vn/Uploads/Originals/2018/12/31/636818423658308841_xiaomi 6-3gb-64gb-xanh.png',
        link: '/dien-thoai/xiaomi-redmi-6-3gb-32gb',
    },
];
var listProductChoose = {
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
$('.btn-number-tab').on('click', function (e) {
    e.preventDefault();
    var fieldName = $(this).attr('data-field');
    var type = $(this).attr('data-type');
    var lengthTab = $(".list-size-order li").length;
    // tab-1
    var currentNameTab = $("#pills-tab li .active").attr('id');
    // 1
    var currentIdTab = currentNameTab.substr(4, currentNameTab.length);
    // tab cuối cùng vd tab-7 -> lastidtab 7
    var lastNameTab = $("#pills-tab li:last-child a").attr('id')
    var lastIdTab = lastNameTab.substr(4, currentNameTab.length);
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
            $(add_size_order_tab).appendTo(".list-size-order").hide().fadeIn(300);

            var add_size_order_tab_content = $('<div class="tab-pane fade" id="pills-tab-' + i + '" role="tabpanel" aria-labelledby="tab-' + i + '"">' + (lengthTab + 1) + '</div>');
            $(add_size_order_tab_content).appendTo(".tab-content");
            $(".btn-number-tab[data-type='minus'][data-field='" + fieldName + "']").removeAttr('disabled')
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
            display = 'Giảm (' + valueDiscount.formatNumber() + ')';
        }
        $('#display-input-discount-' + nameTab).text(display)
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
        $("#input-discount-" + nameTab).val(0)
        $('#display-input-discount-' + nameTab).text('')
        sumPriceItem(nameTab);
    });
    $('.remove-item').on('click',function(e){
        $(this).parents(".box-pos-order-detail").remove();
        sumTotalPriceTabItem();
    });
    sumTotalPriceTabItem();
}
function sumPriceItem(nameTab) { // tab-1-item-1
    var price = $("#price-" + nameTab).attr('data-field');
    var size = $("input[name='"+nameTab+"']").val();
    var discount = $("#input-discount-" + nameTab).val();
    var radioChoose = $("input:radio[name ='radio-" + nameTab + "']:checked").val();
    var totalPrice;
    if (radioChoose == 'vnd') {
        totalPrice = parseInt((price - discount) * size);
    } else if (radioChoose == '%') {
        totalPrice = parseInt(((price * (100 - discount)) / 100) * size);
    }
    $("#sum-price-" + nameTab).text(totalPrice.formatNumber());
    $("#sum-price-" + nameTab).attr('data-field', totalPrice);
    sumTotalPriceTabItem();
}
// tính thanh toán
function sumTotalPriceTabItem() {
    console.log('tinh tien')
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
var currentIdItem = 4;
function addItemToTab(id) {
    // tab-1
    var currentNameTab = $("#pills-tab li .active").attr('id');
     // 1
    var currentIdTab = currentNameTab.substr(4, currentNameTab.length);
    pos = listItem.map(function (e) { return e.id; }).indexOf(id);
    var productChoose = {};
    if (pos >= 0) {
        productChoose = listItem[pos];
        var html = '';
        html += '<div class="box-pos-order-detail" id="tab-' + currentIdTab + '-item-' + currentIdItem +'"><div class="pos-order-list-item" role="tab" id="heading-tab-' + currentIdTab +'-item-' + currentIdItem +'"><div class="d-flex mb-0 w-100 align-items-center"><div class="quantity display-input-quant" id="display-input-quant-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'" name="display-input-quant-tab-' + currentIdTab + '-item-' + currentIdItem +'">1</div><a class="collapsed" data-toggle="collapse" href="#collapse-tab-' + currentIdTab + '-item-' + currentIdItem +'" aria-expanded="false" aria-controls="collapse-tab-' + currentIdTab + '-item-' + currentIdItem +'"><div class="productname"><span class="primary-text">' + productChoose.name + '</span><span id="price-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-field="' + productChoose.price + '" class="second-text">× ' + productChoose.price.formatNumber() +'<span id="display-input-discount-tab-' + currentIdTab + '-item-' + currentIdItem +'"></span></span></div><div class="subtotal"><span class="second-text">' + productChoose.sku + '</span><span id="sum-price-tab-' + currentIdTab + '-item-' + currentIdItem +'" class="sum-price-tab font-weight-bold" data-field="' + productChoose.price + '">' + productChoose.price.formatNumber() + '</span></div></a><div class=""><button type="button" class="btn btn-link p-3 remove-item"><span class="fa fa-times" style=""></span></button></div></div></div><div id="collapse-tab-' + currentIdTab + '-item-' + currentIdItem +'" class="collapse" role="tabpanel" aria-labelledby="heading-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-parent="#accordion"><div class="pos-order-item-container"><div class="pos-order-item-content"><h5 class="text-center text-uppercase">Số lượng</h5><div class="input-group mb-2"><span class="input-group-btn"><button type="button" class="btn btn-link btn-number shadow-none btn-lg" disabled="disabled" data-type="minus" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'"><span class="fa fa-minus m-0"></span></button></span><input type="text" name="tab-' + currentIdTab + '-item-' + currentIdItem +'" class="form-control form-control input-number text-center" value="1"><span class="input-group-btn"><button type="button" class="btn btn-link btn-number shadow-none btn-lg" data-type="plus" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'"><span class="fa fa-plus m-0"></span></button></span></div><div class="d-flex mb-2"><div class="d-flex align-items-center justify-content-start"><div class="font-weight-bold">Giảm giá</div></div><div class="col d-flex align-items-center justify-content-center"><div class="form-radio mat-button-toggle-group"><form class="d-flex"><div class="radio"><label class="pl-0 mb-0"><input type="radio" name="radio-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'" checked="checked" value="vnd"><div class="radio-switch-discount">đ</div></label></div><div class="radio"><label class="pl-0 mb-0"><input type="radio" name="radio-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'" value="%"><div class="radio-switch-discount">%</div></label></div></form></div></div><div class="d-flex align-items-center justify-content-end flex-column"><div class="f-s-11">Nhập giảm giá</div><input type="text" id="input-discount-tab-' + currentIdTab + '-item-' + currentIdItem +'" data-field="tab-' + currentIdTab + '-item-' + currentIdItem +'" class="form-control form-control-sm text-right width-10x input-discount-number" value="" min="0" max="1000000"></div></div><div class=""><input type="text" class="form-control form-control-md form-bg-inverse" placeholder="Ghi chú"></div></div></div></div></div>';
        $('.tab-content .tab-pane.active .pos-order-list-wrapper').append(html);
        currentIdItem++;
        renderAgainScript();
    }
    console.log(432, productChoose)
}