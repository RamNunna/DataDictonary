<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoredProcInfo.aspx.cs" Inherits="DataDictionary.StoredProcInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                        <h4><i class="icon-reorder"></i>Proc Info</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvPrcoInfo" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPrcoInfo_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PARAMETER_NAME" HeaderText="PARAMETER NAME" />
                                    <asp:BoundField DataField="DATA_TYPE" HeaderText="DATA TYPE" />                                  
                                    <asp:BoundField DataField="CHARACTER_MAXIMUM_LENGTH" HeaderText="MAXIMUM LENGTH" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
              <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Tables</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDependentTables" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDependentTables_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="table_name" HeaderText="Table Name" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                  <div class="col-md-4">
                    <div class="widget-header">
                        <h4><i class="icon-reorder"></i>Dependent Procs</h4>
                        <div class="widget-content ">
                            <asp:GridView ID="gvDependentOthers" runat="Server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover table-checkable table-responsive datatable"
                                EmptyDataText="No records found!!" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDependentOthers_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="table_name" HeaderText="Proc Name" />
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
                                      <asp:BoundField DataField="table_name" HeaderText="View Name" />

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
                                    <asp:BoundField DataField="table_name" HeaderText="Trigger Name" />

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
