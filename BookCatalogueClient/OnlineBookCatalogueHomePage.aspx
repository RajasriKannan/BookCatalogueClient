<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineBookCatalogueHomePage.aspx.cs" Inherits="BookCatalogueClient.OnlineBookCatalogueHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Book Catalogue</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="~\Stylesheets\Parent.css" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <style>
        .button {
            background-color: hsl(120, 100%, 10%);
            box-shadow: 0 10px 50px hsl(60, 100%, 50%);
            color: white;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="header">
            <h1>Online Book Catalogue</h1>
        </div>
        <div class="row">
            <div class="col-9 col-s-9">
                <table class="CategoryTable">
                    <tr>
                        <td>Search By:                
                        </td>
                        <td>
                            <asp:DropDownList CssClass="categoryDropdown w3-round-large" ID="ddlKSearchCatalogue" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnSearchCatalogue" runat="server" />
                            <asp:HiddenField ID="hdnSearchText" runat="server" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchValue" runat="server"></asp:TextBox></td>

                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Button CssClass="button w3-button w3-round-large w3-hover-black" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSelectCatalogue" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>

                <div>

                    <asp:GridView ID="gvOnlineCatalogue" CssClass="w3-border w3-hsl(120, 100%, 10%) w3-table-all" GridLines="None" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="BookName" HeaderText="Book Name" />
                            <asp:BoundField DataField="Author" HeaderText="Author" />
                            <asp:BoundField DataField="Price" HeaderText="Price" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" OnClick="btnEditBookForm_Click" role="button" runat="server" Text="Edit / Delete" CssClass="button w3-button w3-round-large w3-hover-black"></asp:LinkButton>
                                    <%--<asp:LinkButton ID="lnkDelete" OnClick="btnEditDeleteItem_Click" role="button"  runat="server" Text="Delete" CssClass="button w3-button w3-round-large w3-hover-red"></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="btnAddNewBook" OnClick="btnAddNewBook_Click" runat="server" Text="Add New Books" CssClass="w3-panel button w3-button w3-round-large w3-hover-black" />
                <%-- Add New Books --%>
                <div id="AddNewBookForm" runat="server">
                    <div class="row w3-container">
                        <table class="w3-panel w3-table w3-leftbar w3-border-black">
                            <tr>
                                <th colspan="2">
                                    <h4>Add New Book</h4>
                                </th>
                            </tr>
                            <tr>
                                <td>Book Name</td>
                                <td>
                                    <asp:TextBox ID="txtBookName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Author</td>
                                <td>
                                    <asp:TextBox ID="txtAuthorName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Price</td>
                                <td>
                                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnInsertNewBook" OnClick="btnInsertBook_Click" runat="server" Text="Add" CssClass="button w3-button w3-round-large w3-hover-black" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelNewBook" OnClick="btnCancelNewBook_Click" runat="server" Text="Cancel" CssClass="button w3-button w3-round-large w3-hover-red" /></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <%-- Edit Book --%>
                <div id="EditBookForm" runat="server">
                    <div class="row w3-container">
                        <table class="w3-panel w3-table w3-leftbar w3-border-black">
                            <tr>
                                <th colspan="2">
                                    <h4>Edit Book</h4>
                                </th>
                            </tr>
                            <tr>
                                <td>Book Name</td>
                                <td>
                                    <asp:HiddenField ID="hdnBookID" runat="server" />
                                    <asp:TextBox ID="txtEditName" runat="server"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>Author</td>
                                <td>
                                    <asp:TextBox ID="txtEditAuthor" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Price</td>
                                <td>
                                    <asp:TextBox ID="txtEditPrice" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" OnClick="btnEditBook_Click" runat="server" Text="Edit" CssClass="button w3-button w3-round-large w3-hover-black" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdateDelete" OnClick="btnDeleteBook_Click" runat="server" Text="Delete" CssClass="button w3-button w3-round-large w3-hover-black" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdateCancel" OnClick="btnCancelEdit_Click" runat="server" Text="Cancel" CssClass="button w3-button w3-round-large w3-hover-red" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
