<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventarioCiclicoVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.InventarioCiclicoVisor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >                       

        <div id="RestrictionZoneID" class="WindowContenedor">
             
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows >
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Agregar Inventario ciclico" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   

                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <asp:Label runat="server" ID="_lblCategoria" Text="Categoria Articulo"></asp:Label>
                                                        <asp:DropDownList runat="server" ID="_ddlCategoriaArticulo"></asp:DropDownList>
                                                     
                                                       <%-- <asp:CheckBox runat="server" ID="_cbxSemana"  Text="Dias Semana" OnCheckedChanged="_cbxSemana_CheckedChanged" AutoPostBack="True"/>
                                                        <asp:CheckBox runat="server" ID="_cbxDia"  Text="Dia Especifico" OnCheckedChanged="_cbxDia_CheckedChanged" AutoPostBack="True"/>--%>

                                                    <asp:RadioButtonList runat="server" ID="_rblPanel" OnSelectedIndexChanged="_rblPanel_SelectedIndexChanged" AutoPostBack="true">                                                        
                                                        <asp:ListItem Selected="True">Dias Semana</asp:ListItem>
                                                        <asp:ListItem>Dia Especifico</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                    <asp:Panel ID="PanelDiasSemana" runat="server" GroupingText="Por Dias de la Semana">
                                                        <asp:Label runat="server" ID="_lblFechaInicio" Text="FechaInicio"></asp:Label>
                                                        <telerik:RadDatePicker runat="server" ID="_rdpFechaInicio"></telerik:RadDatePicker>
                                                        <asp:Label runat="server" ID="_lblFechaFinal" Text="FechaFinal"></asp:Label>
                                                        <telerik:RadDatePicker runat="server" ID="_rdpFechaFinal"></telerik:RadDatePicker>                                                        
                                                        <asp:Panel runat="server" GroupingText="Dias de la Semana">                                                           
                                                            <asp:CheckBox runat="server" ID="_cbLunes" Text="Lunes" />
                                                            <asp:CheckBox runat="server" ID="_cbMartes" Text="Martes" />
                                                            <asp:CheckBox runat="server" ID="_cbMiercoles" Text="Miercoles" />
                                                            <asp:CheckBox runat="server" ID="_cbJueves" Text="Jueves" />
                                                            <asp:CheckBox runat="server" ID="_cbViernes" Text="Viernes" />
                                                            <asp:CheckBox runat="server" ID="_cbSabado" Text="Sabado" />
                                                            <asp:CheckBox runat="server" ID="_cbDomingo" Text="Domingo" />
                                                        </asp:Panel>
                                                        <asp:Button runat="server" ID="_btnDiasSemanaAgregar" Text="Agregar" OnClick="_btnDiasSemanaAgregar_Click"/>
                                                    </asp:Panel>

                                                    <asp:Panel ID="PanelDiaEspecifico" runat="server" GroupingText="Dia Especifico" Visible="false" >
                                                         <asp:Label runat="server" ID="_lblDiaEspecifico" Text="FechaInicio"></asp:Label>
                                                         <telerik:RadDatePicker runat="server" ID="_rdpDiaEspecifico"></telerik:RadDatePicker>
                                                        <asp:Button runat="server" ID="_btnDiaEspecificoAgregar" Text="Agregar" OnClick="_btnDiaEspecificoAgregar_Click"/>
                                                    </asp:Panel>

                                                     <td>
                                                        <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        <telerik:RadDatePicker ID="_rdpB_FechaInicio" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="_rdpB_FechaFinal" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>                                                            
                                                        </td> 
                                                    <telerik:RadButton runat ="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click"></telerik:RadButton>

                                                    <telerik:RadGrid ID="RGInventariosCiclicos" AllowPaging="True" Width="100%"  OnNeedDataSource ="RGInventariosCiclicos_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreCategoria"
                                                                    SortExpression="NombreCategoria" HeaderText="Nombre Categoria" DataField="NombreCategoria">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaPorAplicar"
                                                                    SortExpression="FechaPorAplicar" HeaderText="Fecha Por Aplicar" DataField="FechaPorAplicar">
                                                                </telerik:GridBoundColumn>
                                                               

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>                                          
                                            </asp:UpdatePanel>                                                                                          
                                            </asp:Panel>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </ContentTemplate>
                            <Shortcuts>
                                <telerik:WindowShortcut CommandName="Maximize" Shortcut="Ctrl+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Minimize" Shortcut="Ctrl+F7"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                            </Shortcuts>
                           
                        </telerik:RadWindow>
        
                    </Windows>
                </telerik:RadWindowManager> 

               </div>                      

   </asp:Panel>
</asp:Content>

