<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" ng-app="app">
         <div class="row">
             <div class="col-md-6 col-lg-6">
                <div class="card">
                    <div class="card-header">
                       Выбор автора
                    </div>
                    <div class="card-block">
                        <asp:DropDownList CssClass="form-control" ID="AuthorsNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AuthorChange"></asp:DropDownList>
                        <br />
                        <p class="card-text">Информация про автора:</p>
                        <p class="card-text"><asp:Label ID="AuhtorInfo" runat="server"></asp:Label></p>
                    </div>
                    <div class="card-footer">
                            Поставить новую цену для всех комиксов этого автора
                            <div class="row">
                                <div class="col-md-4 col-lg-4">
                                    <asp:TextBox ID="newPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 col-lg-4">
                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary col-md-6 col-lg-6" Text="Set" OnClick="increasePrice" />
                                </div>
                            </div>
                       </div>
                </div>
             </div>
            <div class="col-md-6 col-lg-6">
                 <div class="card">
                    <div class="card-header">
                       Комиксы автора
                    </div>
                    <div class="card-block">
                         <asp:DropDownList CssClass="form-control" ID ="ComicsNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComicsChange"></asp:DropDownList>
                        <br />
                        <p class="card-text">Информация про комикс:</p>
                        <p class="card-text"><asp:Label ID="ComicsInfo" runat="server"></asp:Label></p>
                        <p class="card-text"><asp:Label ID="ComicsPrice" runat="server"></asp:Label></p>
                        <p class="card-text">Все авторы комикса</p>
                        <p class="card-text"> <asp:Label ID="ComicsAuthors" runat="server"></asp:Label></p>
                    </div>
                    <div class="card-footer">
                        Удалить этот комикс
                        <asp:Button ID="Button1" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="deleteComics" />
                    </div>
                 </div>
             </div>
       </div>
    </div>
</asp:Content>
