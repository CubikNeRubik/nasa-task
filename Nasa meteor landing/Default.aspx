<%@ Page Title="Nasa meteor landings"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NasaMeteorLanding.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="alert" class="alert alert-danger" role="alert">
      The user should see a message saying - mass was not found, jumping to first-year where there is a mass that fits the criteria!
    </div>

    <div class="form-group">
        <label for="yearInput">Choose year</label>
        <select class="form-control" id="yearInput" runat="server"></select>
    </div>

    <div class="form-group">
        <label for="massInput">Show meteors with mass bigger then ...</label>
        <input type="text" class="form-control" id="massInput" placeholder="Type mass" />
    </div>

    <button id="submit" class="btn btn-primary"><i class="glyphicon glyphicon-search"></i> Search</button>

    <table class="table">
    <thead>
        <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            <th scope="col">Nametype</th>
            <th scope="col">Recclass</th>
            <th scope="col">Mass (g)</th>
            <th scope="col">Fall</th>
            <th scope="col">Year</th>
            <th scope="col">Latitude</th>
            <th scope="col">Longitude</th>
        </tr>
    </thead>
    <tbody>
    </tbody>
    </table>

        <script>
            $('#alert').hide();

            $("#submit").click(function (e) {

                e.preventDefault();

                $('#alert').hide();

                var year = $('#MainContent_yearInput').val() || null;
                var mass = $('#massInput').val() || null;

                $.ajax({
                    type: "POST",
                    url: "Default.aspx/Search",
                    data: '{"year": ' + year + ', "mass": ' + mass + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: "true",
                    cache: "false",
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    success: function (jqXHR) {
                        var data = jqXHR.d;

                        if (mass > 0 && data.length === 0) {
                            $.ajax({
                                type: "POST",
                                url: "Default.aspx/Search",
                                data: '{"year": null, "mass": ' + mass + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: "true",
                                cache: "false",
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                                },
                                success: function (jqXHR) {
                                    var data = jqXHR.d;

                                    if (data.length) {
                                        data = data.sort((a, b) => a.Year - b.Year);

                                        $('#alert').show();

                                        $('#MainContent_yearInput').val(data[0].Year);
                                        render([data[0]]);
                                    }
                                }
                            });
                        }
                        else {
                            render(data);
                        }
                    }
                });

            })

            function render(data) {
                $("table tbody").empty();
                for (let row of data) {

                    var markup = `<tr>
    <td>${row.Id}</td>
    <td>${row.Name}</td>
    <td>${row.Nametype}</td>
    <td>${row.Recclass}</td>
    <td>${row.Mass}</td>
    <td>${row.Fall}</td>
    <td>${row.Year}</td>
    <td>${row.Latitude}</td>
    <td>${row.Longitude}</td>
</tr>`;
                    $("table tbody").append(markup);
                }
            }

    </script>
</asp:Content>
