<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


     <div class="container" ng-controller="studentsCtrl" ng-app="app">
         <div>
            <label for="authorSelect"> Автор </label><br>
            <select name="authorSelect" ng-model="data.author">
                <option value="option-1">Option 1</option>
            </select>
            <div>
                Информация про автора:

            </div>
             <div>
               Комиксы автора:

            </div>
         </div>
        <div>
            <label for="comicsSelect"> Комикс </label><br>
            <select name="comicsSelect" ng-model="data.comics">
                <option value="option-1">Option 1</option>
            </select>
            <div>
                Информация про комикс:

            </div>
             <div>
               Авторы комикса:

            </div>
         </div>
         <div>
             <label for="authorSelect"> Автор </label><br>
            <select name="authorSelect" ng-model="data.authorForChanging">
                <option value="option-1">Option 1</option>
            </select>
             <md-button class="md-primary">Увеличить цену его комиксов на 1$</md-button>
         </div>
    </div>
</asp:Content>
