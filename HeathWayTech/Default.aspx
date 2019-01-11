<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeathWayTech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ULTIMATE GUIDE TO BOOKS</h1>
        <p class="lead">See ratings of your favourites books and don't forget to leave a rating yourself</p>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Enter your name below to get started</h2>
            <p>
                <asp:Label ID="nameLabel" runat="server" Text="Name" Font-Bold="True" Font-Overline="False" Font-Size="Small" Height="30px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="nameTextBox" runat="server" AutoCompleteType="Enabled"></asp:TextBox>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter your name" ControlToValidate="nameTextBox" Display="Dynamic" Font-Bold="True" Font-Italic="True" Font-Names="Arial" ForeColor="Red"></asp:RequiredFieldValidator>
            </p>
            <p><asp:Button ID="GetStartedButton" class="btn-primary btn-lg" runat="server" Text="Get Started" OnClick="GetStartedButton_Click" /></p>
        </div>
    </div>
</asp:Content>
