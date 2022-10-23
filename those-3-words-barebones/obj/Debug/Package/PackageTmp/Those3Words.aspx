<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Those3Words.aspx.cs" Inherits="those_3_words.Those3Words" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hiddenScore" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenAddress" />
    <asp:HiddenField runat="server" ID="hiddenCoords" />
    <asp:HiddenField runat="server" ID="hiddenCountry" />
    <asp:HiddenField runat="server" ID="hiddenTry" Value="3"/>
    <asp:Panel runat="server" BorderWidth="2" BorderStyle="Ridge">
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Label runat="server" Font-Size="XX-Large" Font-Bold="true" ForeColor="Red">THOSE 3 WORDS</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server">
            <asp:Table runat="server" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server">First Word: </asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="FirstWord" placeholder="toddler"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator runat="server" ControlToValidate="FirstWord" ValidationExpression="^[a-zA-Z]*$"></asp:RegularExpressionValidator>--%>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server">Second Word: </asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server"  ID="SecondWord" placeholder="geologist"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator runat="server" ControlToValidate="SecondWord" ValidationExpression="^[a-zA-Z]*$"></asp:RegularExpressionValidator>--%>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server">Third Word: </asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server"  ID="ThirdWord" placeholder="animated"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator runat="server" ControlToValidate="FirstWord" ValidationExpression="^[a-zA-Z]*$"></asp:RegularExpressionValidator>--%>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                        <asp:Button runat="server" ID="BtnValidate" Text="Submit" OnClick="BtnValidate_click" 
                            OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="pnlStatus" Visible="false">
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                    <asp:Label runat="server" ID="lblStatus" ForeColor="Red">Status</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="autoSuggest1" Text="" OnCheckedChanged="autoSuggest1_Toggle" AutoPostBack="true" Visible="false"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="autoSuggest2" Text="" OnCheckedChanged="autoSuggest2_Toggle" AutoPostBack="true" Visible="false"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="autoSuggest3" Text="" OnCheckedChanged="autoSuggest3_Toggle" AutoPostBack="true" Visible="false"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnContinue" Text="Continue" OnClick="BtnContinue_click"
                        OnClientClick="this.disabled=true;" UseSubmitBehavior="false" Visible="false"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="pnlOnWater" Visible="false">
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                    <asp:Label runat="server" ID="lblType" ForeColor="Red">Is it on land or water?</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="land" Text="Land" OnCheckedChanged="land_Toggle" AutoPostBack="true" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="water" Text="Water" OnCheckedChanged="water_Toggle" AutoPostBack="true" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <asp:HiddenField runat="server" ID="hiddenType" />
                    <asp:Button runat="server" ID="BtnLoW" Text="Next" OnClick="BtnLoW_click"
                        OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="pnlGame" Visible="false">
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" >
                    <asp:Label runat="server" ID="lblLoW" ForeColor="Red">Given coordinates are on: </asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" >
                    <asp:Label runat="server" ID="lblContinent" ForeColor="Red">It lies in the continent: </asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="GuessMade"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell>
                    <asp:Button runat="server" ID="BtnGuess" Text="Submit" OnClick="BtnGuess_click"
                        OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" >
                    <asp:Label runat="server" ID="lblChance" ForeColor="Red">You have 3 chances left.</asp:Label>
                    <asp:Label runat="server" ID="lblHint" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server">
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                        <asp:Button runat="server" ID="BtnReset" Text="Reset" OnClick="BtnReset_click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    </form>
</body>
</html>
