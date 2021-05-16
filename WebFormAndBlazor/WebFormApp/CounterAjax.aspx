<%@ Page Title="Counter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CounterAjax.aspx.cs" Inherits="WebFormApp.CounterAjax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Counter</h2>
	<asp:UpdatePanel runat="server" ID="UpdPanel" UpdateMode="Conditional">
		<ContentTemplate>
<asp:HiddenField ID="HdnCounter" Value="0"  runat="server" />
	<asp:Label runat="server" id="CounterParagraph"></asp:Label>
	<asp:Button ID="BtnCounter" runat="server" OnClick="BtnCounter_Click" Text="Click me" />
			</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="BtnCounter" EventName="Click" />
		</Triggers>
		</asp:UpdatePanel>
</asp:Content>
