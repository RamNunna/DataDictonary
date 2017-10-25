<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableInfo.aspx.cs" Inherits="DataDictionary.TableInfo" %>

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
          <asp:Button ID="btnBack" runat="server" Text="Home" PostBackUrl="~/DataDictonary.aspx"/>
      <div class="row" id="tblInfo" runat="server">
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Table Info</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvTavleInfo" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTavleInfo_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="COLUMN_NAME" HeaderText="COLUMN NAME" />
                                    <asp:BoundField DataField="DATA_TYPE" HeaderText="DATA TYPE" />
                                    <asp:BoundField DataField="IS_NULLABLE" HeaderText="IS NULLABLE" />
                                    <asp:BoundField DataField="CHARACTER_MAXIMUM_LENGTH" HeaderText="MAXIMUM LENGTH" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Table Referenceed Tables</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDependentTables" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDependentTables_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PK_Column" HeaderText="Column" />
                                    <asp:BoundField DataField="PK_Table" HeaderText="Table Name" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Tables On Table</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDependentOthers" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDependentOthers_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FK_Column" HeaderText="Column" />
                                    <asp:BoundField DataField="FK_Table" HeaderText="Table Name" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Proc's </h4>
                        <div class="widget-content ">
                            <asp:GridView ID="dependentProc" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="dependentProc_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Stored Proc" />

                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent View's </h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDependentViews" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDependentViews_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="View" />

                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Triggers's </h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDenTriggers" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDenTriggers_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="View" />

                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Indexers </h4>
                        <div class="widget-content ">
                            <asp:GridView ID="grdIndexes" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="dependentProc_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IndexName" HeaderText="IndexName" />
                                       <asp:BoundField DataField="IndexType" HeaderText="IndexType" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
