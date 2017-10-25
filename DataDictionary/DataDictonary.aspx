<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataDictonary.aspx.cs" Inherits="DataDictionary.DataDictonary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      
        <div class="row">
            <div class="col-md-12">
                <div class="tabbable tabbable-custom tabbable-full-width">
                    <div class="tab-pane" id="Div6">
                        <div class="form-horizontal myform">
                            <div class="col-md-12">
                                <div class="widget">
                                    <div class="widget-header">
                                        <h4>Connection String</h4>
                                    </div>
                                    <div class="widget-content">
                                        <table class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th width="20%">Data Source:</th>
                                                    <th width="20%">Database:</th>
                                                    <th width="20%">Login:</th>
                                                    <th width="20%">Password:</th>
                                                    <th width="20%">&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtDataSource" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdDataSource" runat="server" Display="Dynamic" ErrorMessage="DataSource Required" ValidationGroup="connstr" ForeColor="Red" ControlToValidate="txtDataSource">
                                                        </asp:RequiredFieldValidator></td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtDatabase" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdInitalCat" runat="server" Display="Dynamic" ErrorMessage="DataSource Required" ValidationGroup="connstr" ForeColor="Red" ControlToValidate="txtDatabase">
                                                        </asp:RequiredFieldValidator></td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtUserId" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdLogin" runat="server" Display="Dynamic" ErrorMessage="DataSource Required" ValidationGroup="connstr" ForeColor="Red" ControlToValidate="txtUserId">
                                                        </asp:RequiredFieldValidator></td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdPassword" runat="server" Display="Dynamic" ErrorMessage="DataSource Required" ValidationGroup="connstr" ForeColor="Red" ControlToValidate="txtPassword">
                                                        </asp:RequiredFieldValidator></td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="connstr" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="widget box col-md-3">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Tables</h4>
                        <asp:Button ID="btnTblExport" runat="server" Text="Export Excel" OnClick="btnTblExport_Click" />
                    </div>
                    <div class="widget-content ">
                        <asp:GridView ID="gvTableData" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                            EmptyDataText="No records found!!" AllowPaging="true" DataKeyNames="TABLE_NAME" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvTableData_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Table Name">
                                    <ItemTemplate>
                                       <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text='<%# Eval("TABLE_NAME") %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("TABLE_NAME") %>' NavigateUrl='<%# Eval("TABLE_NAME", "~/TableInfo.aspx?tablename={0}") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
                <div class="widget box col-md-3">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Stored Procedure</h4>
                    </div>
                    <div class="widget-content ">
                        <asp:GridView ID="gvProcData" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                            EmptyDataText="No records found!!" AllowPaging="true" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvProcData_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Stored Procedure">
                                    <ItemTemplate>
                                       <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text='<%# Eval("TABLE_NAME") %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("ROUTINE_NAME") %>' NavigateUrl='<%# Eval("ROUTINE_NAME", "~/StoredProcInfo.aspx?procname={0}") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
                <div class="widget box col-md-2">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Function</h4>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="gvFuncData" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                            EmptyDataText="No records found!!" AllowPaging="true" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvFuncData_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Function">
                                    <ItemTemplate>
                                       <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text='<%# Eval("TABLE_NAME") %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("ROUTINE_NAME") %>' NavigateUrl='<%# Eval("ROUTINE_NAME", "~/FunctionInfo.aspx?funname={0}") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
                <div class="widget box col-md-2">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>View</h4>
                    </div>
                    <div class="widget-content no-padding">
                        <asp:GridView ID="gvViewData" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                            EmptyDataText="No records found!!" AllowPaging="true" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvViewData_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text='<%# Eval("TABLE_NAME") %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("TABLE_NAME") %>' NavigateUrl='<%# Eval("TABLE_NAME", "~/ViewInfo.aspx?viewname={0}") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
                <div class="widget box col-md-2">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Trigger</h4>
                    </div>
                    <div class="widget-content no-padding">
                        <asp:GridView ID="gvTrigData" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                            EmptyDataText="No records found!!" AllowPaging="true" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvTrigData_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Trigger">
                                    <ItemTemplate>
                                       <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text='<%# Eval("TABLE_NAME") %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("NAME") %>' NavigateUrl='<%# Eval("NAME", "~/TriggerInfo.aspx?triname={0}") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
       
    </form>
</body>
</html>
