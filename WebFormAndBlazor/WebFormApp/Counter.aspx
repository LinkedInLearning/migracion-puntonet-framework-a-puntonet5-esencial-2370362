<%@ Page Title="Counter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Counter.aspx.cs" Inherits="WebFormApp.Counter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Counter</h2>
	<p aria-live="assertive">Current count: <%= CurrentCount %></p>
	<asp:Button ID="BtnCounter" runat="server" OnClick="BtnCounter_Click" Text="Click me" />
</asp:Content>
