

$.fn.searchC = function (params) {

    var urlSearch = "/controles/GetSearchExpositor";
    var urlBusca = "/controles/GetCtlResultExpo?nombre=";
    var idEvento = $(this).data("idevento");

    $ctlDialog = setDialog();
    configureDelete();
    $("#btnAddExpo").click(
        function () {
            $ctlDialog.load(urlSearch,
                function () {
                    $("#btnBuscarExpo").click(function () {
                        
                        $ctlDialog.find("div[class='result']").load(urlBusca + $ctlDialog.find("input[class='txtBusca']").val(),
                            function () {
                                $(this).find("input[class='select']").click(function () {
                                    
                                    var idExpo = $(this).data("id");
                                    $.ajax(
                                        {
                                            type: "POST",
                                            url: "/servicios/AddExpoEvent",
                                            cache: false,
                                            contentType: "application/json; charset=utf-8",
                                            data: "{idEvento: " + idEvento + ", idExpositor: " + idExpo + " }",
                                            success: function (data) {
                                                
                                                var _urlExpo = "/controles/GetCtlExpositoresEvent/?idEvent=" + idEvento;
                                                
                                                $("#_divExpositores").load(_urlExpo,
                                                    function () {
                                                        $ctlDialog.dialog("close");
                                                        configureDelete();
                                                    });
                                            }
                                        });

                                });
                            });
                    });
                    $ctlDialog.dialog("open");
                });            
        });


    function configureDelete() {
        $("#_divExpositores").find("input[class='delete']").click(
            function () {
                var idExpo = $(this).data("id");
                $.ajax({
                            type: "POST",
                            url: "/servicios/DeleteSpeakEvent",
                            cache: false,
                            contentType: "application/json; charset=utf-8",
                            data: "{idEvento: " + idEvento + ", idExpositor: " + idExpo + " }",
                            success: function (data) {
                                var _urlExpo = "/controles/GetCtlExpositoresEvent/?idEvent=" + idEvento;
                                $("#_divExpositores").load(_urlExpo,
                                    function () {
                                        configureDelete();
                                    });
                            }
                        });
                
            });
    }
    

    function setDialog() {
        return $("<div id='frmRelIte'></div>").dialog(
      {
          width: 500,
          autoOpen: false,
          title: "Buscar Expositor",
          modal: true,
          resizable: false,
          buttons:
              {
                  "Cancelar": function () {
                      $ctlDialog.dialog("close");
                  }
              },
          beforeClose: function (event, ui) {
          },
          close: function (event, ui) {

              $ctlDialog.dialog("destroy").remove();
              $ctlDialog = setDialog();
          }
      });
    }
}