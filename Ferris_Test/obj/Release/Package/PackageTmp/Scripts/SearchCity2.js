var availableTutorials = "";
var url = "";
var cityholder = "";
$(document).ready(function () {

    $(function () {
        $("#dialog").dialog({
            autoOpen: false,
            resizable: true,
            width: "350",
            height: 300,
            modal: true,
            buttons: {
                "Close": function () {
                    var lines = $('textarea').val().split('\n');
                    $('#automplete-3').val(lines);
                    $('#hdn').val(lines);
                    cityholder = lines;
                    if ($(this).find('textarea').val().length) {
                        //$('#dialog').find('textarea').val('');
                        $(this).dialog("close");
                    }
                    else $(this).find('textarea').css({ borderColor: 'red' });
                }
            }
        });
    });

    $("#automplete-3").click(function () {
        $(this).select();
    });
    ///$("#myul li").click(function () {
    $(".fg1 li").click(function () {
        //alert(this.id); // id of clicked li by directly accessing DOMElement property
        //alert($(this).attr('id')); // jQuery's .attr() method, same but more verbose
        //alert($(this).html()); // gets innerHTML of clicked li
        //////alert($(this).text()); // gets text contents of clicked li
        //alert($(this).attr('href'));
        url = "http://localhost:64805/api/values/City?st1=" + $(this).text();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: url,
            //data: "{}",
            dataType: "json",
            success: function (msg) {
                //$('#RSSContent').html(msg);
                //alert(msg);
                availableTutorials = msg;
                $("#automplete-3").autocomplete({
                    source: availableTutorials,
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },

                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        cityholder += terms.join(",");
                        $('#hdn').val(cityholder);
                        this.value = "";
                        //alert(cityholder);
                        return false;
                    }
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });

    });


    $("#btncity").click(function () {

        $('#automplete-3').val($('#hdn').val());
        $('#dialog').find('textarea').val('');
        var str = $('#automplete-3').val();
        var arr = new Array();
        arr = str.split(",");
        $.each(arr, function (index, value) {
            if (value.length) {
                //$('#dialog').find('textarea').append(value);
                //$('#dialog').find('textarea').append("\n");
                var txt = value;
                var box = $("#txtareacity");
                box.val(box.val() + txt + "\n");
            }
        });
        $("#dialog").dialog("option", "title", "Loading....").dialog("open");
        $("span.ui-dialog-title").text('Cities');
        
        
    });
    ////SearchText();

    $("#automplete-3").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    })
    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }
});