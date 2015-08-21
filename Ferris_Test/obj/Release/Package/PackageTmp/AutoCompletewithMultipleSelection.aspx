<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCompletewithMultipleSelection.aspx.cs" Inherits="AutoCompletewithMultipleSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AutoComplete Box with jQuery</title>
    <link href="css/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>  
	<script type="text/javascript">
	    $(document).ready(function() {
	        SearchText();
	    });
	    function SearchText() {

	       var     availableTutorials = [
	                "ActionScript",
	                "Boostrap",
	                "C",
	                "C++",
	                "Ecommerce",
	                "Jquery",
	                "Groovy",
	                "Java",
	                "JavaScript",
	                "Lua",
	                "Perl",
	                "Ruby",
	                "Scala",
	                "Swing",
	                "XHTML"
	            ];
            
	        }

	        $("#txtSearch").autocomplete({
	            
	            source : availableTutorials,
	            //source: function(request, response) {
	            //    $.ajax({
	            //        type: "POST",
	            //        contentType: "application/json; charset=utf-8",
	            //        url: "AutoCompletewithMultipleSelection.aspx/GetAutoCompleteData",
	            //        data: "{'username':'" + extractLast(request.term) + "'}",
	            //        dataType: "json",
	            //        success: function(data) {
	            //            response(data.d);
	            //        },
	            //        error: function(result) {
	            //            alert("Error");
	            //        }
	            //    });
	            //},
	            focus: function() {
	                // prevent value inserted on focus
	                return false;
	            },
	            select: function(event, ui) {
	                var terms = split(this.value);
	                // remove the current input
	                terms.pop();
	                // add the selected item
	                terms.push(ui.item.value);
	                // add placeholder to get the comma-and-space at the end
	                terms.push("");
	                this.value = terms.join(", ");
	                return false;
	            }
	        });
	        $("#txtSearch").bind("keydown", function(event) {
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
	    }
	</script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="demo">
<div class="ui-widget">
    <label for="tbAuto">Enter UserName: </label>
   <input type="text" id="txtSearch" />
   <%--<asp:TextBox ID="txtSearch" runat="server" Width="300px"></asp:TextBox>--%>
</div>
    </form>
</body>
</html>
