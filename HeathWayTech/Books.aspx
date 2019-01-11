<%@ Page Title="Books" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Books.aspx.cs" Inherits="HeathWayTech.Books" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>BOOKS</h1>
        <p class="lead">Rate books on a scale of 1 (low) to 5 (high)</p>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
            <br />
            <p><asp:Button ID="RateButton" class="btn-primary btn-lg" runat="server" Text="Rate" OnClick="RateButton_Click" /></p>
            <p><asp:Button ID="ViewButton" class="btn-primary btn-lg" runat="server" Text="View Ratings" OnClick="ViewButton_Click" /></p>
            <!--<p><a class="btn btn-default" href="Rate.aspx">Rate &raquo;</a></p>-->
        </div>
    </div>
</asp:Content>
