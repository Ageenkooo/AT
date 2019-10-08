<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" %>
 
<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
  
   <div class="container" ng-controller="studentsCtrl" ng-app="app">
 
        <table class="table table-striped table-hover table-condensed">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Credit</th>
                    <th>Semester</th>
 
                </tr>
            </thead>
 
            <tbody><tr ng-repeat="student in studentInfo">
                <td>{{student.id}}
                </td>
                <td>{{student.name}}
                </td>
                <td>{{student.credit}}
                </td>
                <td>{{student.semester}}
                </td>
            </tr>
        </tbody></table>
    </div>
</asp:content>