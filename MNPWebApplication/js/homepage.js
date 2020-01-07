jQuery(document).ready(function () {
    jQuery('#datepickfrom, #datepickto').datepicker({ dateFormat: 'dd-mm-yy' });

    jQuery('.notibar:not(.index-noti) .close').click(function () {
        jQuery(this).parent().fadeOut(function () {
            jQuery(this).remove();
        });
    });

    jQuery('.index-noti .close').click(function () {
        jQuery('.index-noti').addClass('hide-noti');

    });


    jQuery('.index-noti h3').click(function () {
        jQuery('.index-noti').removeClass('hide-noti');

    });

    //function updateHeader() {
    //    //alert(true);
    //    jQuery.ajax({
    //        type: "GET",
    //        url: '/Home/GetLastUpdate',
    //        success: function (recData) {
    //            jQuery('.headerwidget h2').html(recData);
    //        },
    //        error: function () {
                
    //        }
    //    });

    //}

    //jQuery(function () {
    //    setInterval(updateHeader, 10800000);
    //});

    jQuery('.confirmbutton').click(function () {
        var data_id = jQuery(this).attr("data-id");
        jConfirm('Bạn có chắc chắn muốn xóa?', 'Confirmation Dialog', function (r) {
            if (r == true) {
                //alert(true);
                jQuery.ajax({
                            type: "GET",
                            url: '/MNC/Delete',
                            data: { id: data_id },
                            success: function (recData) {
                                //jQuery.getScript('~/js/plugins/jquery.dataTables.min.js');
                                //jQuery('.table-container').html(recData);
                                window.location.reload();
                            },
                            error: function () {
                                jAlert('Something wrong!!!', 'Alert Dialog');
                                return false;
                            }
                        });
            }
            else {
                //alert(false);
                return false;
            }
            //jAlert('Confirmed: ' + r, 'Confirmation Results');
        });
        
    });

    jQuery('.target-deletebutton').click(function () {
        var data_id = jQuery(this).attr("data-id");
        jConfirm('Bạn có chắc chắn muốn xóa?', 'Confirmation Dialog', function (r) {
            if (r == true) {
                //alert(true);
                jQuery.ajax({
                    type: "GET",
                    url: '/Target/Delete',
                    data: { id: data_id },
                    success: function (recData) {
                        //jQuery.getScript('~/js/plugins/jquery.dataTables.min.js');
                        //jQuery('.table-container').html(recData);
                        window.location.reload();
                    },
                    error: function () {
                        jAlert('Something wrong!!!', 'Alert Dialog');
                        return false;
                    }
                });
            }
            else {
                //alert(false);
                return false;
            }
            //jAlert('Confirmed: ' + r, 'Confirmation Results');
        });

    });
    //jQuery('.search-fieldset .btn_search').click(function (e) {
    //    e.preventDefault();
    //    e.stopPropagation();
    //    var msisdn = jQuery('#mobile_number').val();
    //    var fromdate = jQuery('#datepickfrom').val();
    //    var todate = jQuery('#datepickto').val();
    //    jQuery.ajax({
    //        type: "GET",
    //        url: '/DataSearch/GetList',
    //        data: { msisdn: msisdn, fromdate: fromdate,todate:todate },
    //        success: function (recData) {
    //            jQuery.getScript('~/js/plugins/jquery.dataTables.min.js');
    //            jQuery('.table-container').html(recData);
    //        },
    //        error: function () { alert('A error'); }
    //    });
    //});
});
