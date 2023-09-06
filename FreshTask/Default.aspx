<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FreshTask.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="content-body">
        <div class="container-fluid">
            <div class="row page-titles">
                <div class="col p-md-0">
                    <h4>Create Invoice</h4>
                </div>
                <div class="col p-md-0">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Home</a>
                        </li>
                        <!-- <li class="breadcrumb-item"><a href="javascript:void(0)">Pages</a>
                        </li> -->
                        <li class="breadcrumb-item active">Create Invoice</li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">                                     
                                        <asp:Button ID="SaveBillButton" CssClass="btn btn-primary btn-sl-lg mr-3" runat="server" Text="Save bill in DB" OnClick="SaveBillButton_Click" />
                                     
                                        <button id="delete-button" class="btn btn-info  ">Delete selected rows</button>
                                       
                                </div>
                            </div>

                            <div class="row mt-5">
                                <div class="col-lg-12">
                                    <div class="create-invoice-table table-responsive">
                                        <table id="invoice-table" class="table invoice-details-table" style="min-width: 620px;">
                                            <thead>
                                                <tr>
                                                    <th>Manage</th>
                                                    <th>Items</th>
                                                   
                                                    <th>Quantity</th>
                                                    <th>Unit Price</th>
                                                    <th>Total</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td><input type="checkbox"  /></td>
                                                    <td class="muted-text"><asp:Label ID="Item1" runat="server" Text="Item 1"></asp:Label>
                                                    </td>
                                                   
                                                    <td class="muted-text">
                                                        <asp:TextBox runat="server" ID="Quantity1"  style="text-align:center;" value="0" />
                                                    </td>
                                                    <td class="muted-text"><asp:TextBox runat="server" ID="Price1" style="text-align:center;" value="0" /></td>
                                                    <td class="text-primary"><span>0.00</span></td>
                                                </tr>
                                                <tr>
                                                    <td><input type="checkbox" /></td>
                                                    <td class="muted-text"><asp:Label ID="Item2" runat="server" Text="Item 2"></asp:Label></td>
                                                   
                                                   
                                                    <td class="muted-text">
                                                        <asp:TextBox runat="server" ID="Quantity2"  style="text-align:center;" value="0" />
                                                    </td>
                                                    <td class="muted-text"><asp:TextBox runat="server" ID="Price2" style="text-align:center;" value="0" /></td>
                                                    <td class="text-primary"> <span>0.00</span></td>
                                                </tr>
                                                <tr>
                                                    <td><input type="checkbox" /></td>
                                                    <td class="muted-text"><asp:Label ID="Item3" runat="server" Text="Item 3"></asp:Label></td>
                                                   
                                                    
                                                    <td class="muted-text">
                                                        <asp:TextBox runat="server" ID="Quantity3" style="text-align:center;"  value="0" />
                                                    </td>
                                                    <td class="muted-text"><asp:TextBox runat="server" ID="Price3" style="text-align:center;" value="0" /></td>
                                                    <td class="text-primary"><span>0.00</span></td>
                                                </tr>
                                                 
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Net</td>
                                                    <td class="text-primary"><span id="net-cell">0.000</span></td>
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
        <!-- #/ container -->
    </div>
    <!--**********************************
        Content body end
    ***********************************-->

<script>

    const table = document.getElementById('invoice-table');
    const inputFields = table.querySelectorAll('input');

    inputFields.forEach(input => {
        input.addEventListener('input', calculateRowTotal);
    });

    function calculateRowTotal() {

        const row = this.parentNode.parentNode;
        const quantity = parseFloat(row.cells[2].getElementsByTagName('input')[0].value);
        const unitPrice = parseFloat(row.cells[3].getElementsByTagName('input')[0].value);
        const total = quantity * unitPrice;

        if (!isNaN(total))
            row.cells[4].textContent = total.toFixed(2);

        calculateNet();
    }

    function calculateNet() {
        const table = document.getElementById('invoice-table');
        const rows = table.querySelectorAll('tbody tr');
        const netCell = document.getElementById('net-cell');

        let netTotal = 0;

        for (let i = 0; i < rows.length - 1; i++) {
            const total = parseFloat(rows[i].cells[4].textContent);

            if (!isNaN(total))
                netTotal += total;
        }

        netCell.textContent = netTotal.toFixed(3);
    }

    const deleteButton = document.getElementById('delete-button');
    deleteButton.addEventListener('click', deleteSelectedRows);

    function deleteSelectedRows(event) {
        event.preventDefault();

        const table = document.getElementById('invoice-table');
        const rows = table.querySelectorAll('tbody tr');

        for (let i = rows.length - 2; i >= 0; i--) {
            const checkbox = rows[i].querySelector('input[type="checkbox"]');
            if (checkbox.checked) {
                table.deleteRow(i + 1);
            }
        }
        calculateNet();
    }

</script>
         
</asp:Content>
