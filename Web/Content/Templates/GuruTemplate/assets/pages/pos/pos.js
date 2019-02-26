  'use strict';
$(document).ready(function() {
    $( document ).ready(function() {
        var i = $(".list-size-order li").length + 1;
        if (i > 1) {
            $(".btn-number-tab[data-type='minus'][data-field='quant[1]']").removeAttr('disabled')
        }
        $('.btn-number-tab').on('click', function (e) {
            e.preventDefault();
            var fieldName = $(this).attr('data-field');
            var type = $(this).attr('data-type');
            var lengthTab = $(".list-size-order li").length;
            // tab-order-1
            var currentNameTab = $("#pills-tab li .active").attr('id');
            var currentIdTab = currentNameTab.substr(10, currentNameTab.length);
            var lastNameTab = $("#pills-tab li:last-child a").attr('id')
            var lastIdTab = lastNameTab.substr(10, currentNameTab.length);
            if (type == 'minus') {
                var minValue = 1;
                if (lengthTab > minValue) {
                    // check div has bookmark
                    if ($('#' + $(".list-size-order li .active").attr('id') + ' i').length == 0) {
                        $("#tab-order-" + currentIdTab).parent().remove();
                        $("#pills-order-" + currentIdTab).remove();
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
                    var add_size_order_tab = $('<li class="pos-order-nav-item"><a class="" id="tab-order-' + i + '" data-toggle="pill" href="#pills-order-' + i + '" role="tab" aria-controls="pills-order-' + i + '" aria-selected="false">#' + (lengthTab + 1) + '</a></li>');
                    $(add_size_order_tab).appendTo(".list-size-order").hide().fadeIn(300);

                    var add_size_order_tab_content = $('<div class="tab-pane fade" id="pills-order-' + i + '" role="tabpanel" aria-labelledby="tab-order-' + i + '"">' + (lengthTab + 1) + '</div>');
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
                display = 'Giảm (' + valueDiscount + ')';
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
            $("#sum-price-" + nameTab).text(totalPrice);
            $("#sum-price-" + nameTab).attr('data-field', totalPrice);
            sumTotalPriceTabItem();
        }
        // tính thanh toán
        function sumTotalPriceTabItem() {
            var totalPrice = 0;
            console.log(231, $('.tab-content .tab-pane.active'));
            $('.tab-content .tab-pane.active .box-pos-order-detail').each(function () {
                totalPrice += parseInt($(this).find('.sum-price-tab').attr('data-field'));
            });
            $('.tab-content .tab-pane.active').find(".total-price-tab").text(totalPrice);
        }
    });
});