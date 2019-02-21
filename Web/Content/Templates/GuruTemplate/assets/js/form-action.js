function ViewListPager(requestLink) {
    $('.data-list-render').html('');
    $.ajax({
        type: "POST",
        url: requestLink,
        //data: "AlertType= " + alertType + "&FromDate=" + fromDate + "&ToDate=" + toDate + "&SearchText=" + searchText + '&baseUrl=' + baseUrl,
        success: function (response) {
            $('.data-list-render').html(response);
        }
    });
}

function CheckTypeFile(typefile, type) {
    if (type == "image") {
        switch (typefile) {
            case 'image/png':
            //case 'image/gif':
            case 'image/jpeg':
            case 'image/bmp':
                return true;
            default:
                return false;
        }
    }
    else {
        if (type == "all") {
            switch (typefile) {
                case 'image/png':
                case 'image/gif':
                case 'image/jpeg':
                case 'image/bmp':
                case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
                case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document':
                case 'application/ms-word':
                case 'application/vnd.ms-excel':
                case 'application/mspowerpoint':
                case 'application/pdf':
                    return true;
                default:
                    return false;
            }
        }
        else {
            if (type == "file") {
                switch (typefile) {
                    case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
                    case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document':
                    case 'application/ms-word':
                    case 'application/vnd.ms-excel':
                    case 'application/mspowerpoint':
                    case 'application/pdf':
                        return true;
                    default:
                        return false;
                }
            }
            else {
                if (type == "pdf") {
                    switch (typefile) {
                        case 'application/pdf':
                            return true;
                        default:
                            return false;
                    }
                }
                else {
                    if (type == "excel") {
                        switch (typefile) {
                            case 'application/vnd.ms-excel':
                                return true;
                            case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
                                return true;
                            default:
                                return false;
                        }
                    }
                }
            }
        }
    }
}

//string targetID = "", string tableName = "", string displayField = "", string valueField = "", string sortBy = "", string sortType = "", string condition = ""
function LoadDynamicSelectList(targetId, sourceId, requestUrlParam, seletedAttr) {
    var selected = $('#' + sourceId).attr(seletedAttr);
    $.ajax({
        type: "POST",
        url: requestUrlParam,
        success: function (response) {
            var html = '<option value="0"> - Chọn loại thông báo - </option>';
            if (typeof response != 'undefined') {
                $.map(response, function (item) {
                    //if (selected == item.Code) {
                    //    html += '<option value="' + item.Code + '" selected>' + item.Name + '</option>';
                    //}
                    //else {
                        html += '<option value="' + item.Code + '">' + item.Name + '</option>';
                    //}
                });
            }
            $('#' + targetId).html(html);
            $('#' + targetId).val(selected).trigger('change');
        }
    });
}

function Redirect(url) {
    window.location.href = url;
}