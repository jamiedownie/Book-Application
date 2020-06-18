<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BookApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Book Example</h1>        
    </div>

    <div class="row">
        <div>  
            <asp:GridView ID="BookGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CssClass="table table-striped"
                OnRowCancelingEdit="BookGridView_RowCancelingEdit" 
                OnRowDeleting="BookGridView_RowDeleting" 
                OnRowEditing="BookGridView_RowEditing" 
                OnRowUpdating="BookGridView_RowUpdating">  
                <Columns>  
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />  
                    <asp:BoundField DataField="Title" HeaderText="Title" />  
                    <asp:BoundField DataField="Author" HeaderText="Author" />  
                    <asp:BoundField DataField="Price" HeaderText="Price" />  
                    <asp:CommandField ShowEditButton="true" 
                        EditText="<i aria-hidden='true' class='glyphicon glyphicon-pencil'></i>" 
                        CancelText="<i aria-hidden='true' class='glyphicon glyphicon-remove'></i>" 
                        UpdateText="<i aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></i>"
                    />  
                    <asp:CommandField ShowDeleteButton="true" DeleteText="<i aria-hidden='true' class='glyphicon glyphicon-trash'></i>"/> 

                </Columns>  
            </asp:GridView>  
        </div>  
        <div class="panel panel-default">
            <div class="panel-heading">
                Add a New Book
            </div>
            <div class="panel-body">
                <div>
                    <asp:Label Text="Title" runat="server" AssociatedControlID="AddTitleTextBox" CssClass="control-label"/>
                    <asp:TextBox ID="AddTitleTextBox" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <asp:Label Text="Author" runat="server" AssociatedControlID="AddAuthorTextBox" CssClass="control-label"/>
                    <asp:TextBox ID="AddAuthorTextBox" runat="server" CssClass="form-control"/>
                </div>
                <div>
                    <asp:Label Text="Price" runat="server" AssociatedControlID="AddPriceTextBox" CssClass="control-label"/>
                    <asp:TextBox ID="AddPriceTextBox" runat="server" CssClass="form-control" />
                </div>            
                <div style="margin-top:10px">
                    <asp:Button ID="AddNewRowButton" Text="Add Book" runat="server" class="btn btn-primary padding-top" OnClick="AddNewRowButton_OnClick"/>
                </div>      
            </div>
        </div>            
        
    </div>

</asp:Content>
