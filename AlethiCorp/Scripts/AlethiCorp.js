/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery-ui.js" />
/// <reference path="jQuery.tmpl.js" />

$(document).ready(function () {

    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });

    $(":input[data-datepicker]").datepicker();

    $("#searchForm").submit(function () {
        $.getJSON($(this).attr("action"),  // the url to get JSON from
                  $(this).serialize(),     // make q=yellow, for example
                  function (data) {      // what to do with the response
                      var result = $("#searchTemplate").tmpl(data);
                      $("#searchResults").empty()
                                 .append(result);
                  }
        );
        return false;
    });
})