<%@ Page Title="Ratings" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Ratings.aspx.cs" Inherits="HeathWayTech.Ratings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Ratings</h1>
        <p class="lead">These are the books with ratings greater than 3.</p>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
            <br />
            <p><asp:Button ID="RateButton" class="btn-primary btn-lg" runat="server" Text="Rate More Books" OnClick="RateButton_Click" /></p>
        </div>
    </div>
</asp:Content>

