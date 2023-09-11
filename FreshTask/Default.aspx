<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FreshTask.Default" %>

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
                                    <div class="col-11">
                                        
                                            <button class="btn btn-primary btn-sl-lg mr-3">Save bill in DB</button>
                                         
                                            <button id="delete-button" class="btn btn-info  ">Delete selected rows</button>   
                                    </div>
                                    <span id="add-item" class="material-symbols-outlined col-1 align-self-center btn">
                                        add
                                        </span>
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
                                                    <!-- <tr>
                                                        <td><input type="checkbox"  /></td>
                                                        <td class="muted-text">item 1</td>
                                                       
                                                        <td class="muted-text">
                                                            <input  style="text-align:center;" value="0" type="text">
                                                        </td>
                                                        <td class="muted-text"><input  style="text-align:center;" value="0" type="text"></td>
                                                        <td class="text-primary"><span>0.00</span></td>
                                                    </tr>
                                                    <tr>
                                                        <td><input type="checkbox" /></td>
                                                        <td class="muted-text">item 2</td>
                                                       
                                                       
                                                        <td class="muted-text">
                                                            <input  style="text-align:center;" value="0" type="text">
                                                        </td>
                                                        <td class="muted-text"><input style="text-align:center;" value="0" type="text"></td>
                                                        <td class="text-primary"> <span>0.00</span></td>
                                                    </tr>
                                                    <tr>
                                                        <td><input type="checkbox" /></td>
                                                        <td class="muted-text">item 3</td>
                                                       
                                                        
                                                        <td class="muted-text">
                                                            <input  style="text-align:center;"  value="0" type="text"  >
                                                        </td>
                                                        <td class="muted-text"><input  style="text-align:center;" value="0" type="text"></td>
                                                        <td class="text-primary"><span>0.00</span></td>
                                                    </tr>
                                                      -->
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>Net</td>
                                                        <td class="text-primary">
                                                            <span id='net-cell'>0.000</span>
                                                             <input type="hidden" id="net-cell-value" name="net-value" value=""/>
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
            <!-- #/ container -->
        </div>
    <script>
        const table = document.getElementById('invoice-table');

        let x = 1;

        var addItemSpan = document.getElementById("add-item");

        addItemSpan.addEventListener("click", function () {

            var tableBody = document.querySelector("#invoice-table tbody");

            var newRow = document.createElement("tr");

            newRow.innerHTML = `
                <td><input type="checkbox"></td>
                <td class="muted-text">Item ${x}</td>
                <td class="muted-text">
                    <input name="Quantity${x}" style="text-align:center;" value="0" type="text">
                </td>
                <td class="muted-text">
                    <input name="UnitPrice${x}" style="text-align:center;" value="0" type="text">
                </td>
                <td class="text-primary"><span>0.00</span></td>
            `;
            x++
            tableBody.insertBefore(newRow, tableBody.lastElementChild);
        });

        table.addEventListener('input', function (event) {
            const target = event.target;
            if (target.tagName === 'INPUT') {
                calculateRowTotal(target);
            }
        });

        function calculateRowTotal(input) {
            const row = input.closest('tr');
            const quantity = parseFloat(row.cells[2].querySelector('input').value);
            const unitPrice = parseFloat(row.cells[3].querySelector('input').value);
            const total = quantity * unitPrice;

            if (!isNaN(total))
                row.cells[4].querySelector('span').textContent = total.toFixed(2);

            calculateNet();
        }

        function calculateNet() {
            const rows = table.querySelectorAll('tbody tr');
            const netCell = document.getElementById('net-cell');
            const netCellValue = document.getElementById('net-cell-value')

            let netTotal = 0;

            for (let i = 0; i < rows.length - 1; i++) {
                const total = parseFloat(rows[i].cells[4].querySelector('span').textContent);

                if (!isNaN(total))
                    netTotal += total;
            }

            netCell.textContent = netTotal.toFixed(3);
            netCellValue.value = netCell.textContent
        }

        const deleteButton = document.getElementById('delete-button');
        deleteButton.addEventListener('click', deleteSelectedRows);

        function deleteSelectedRows(event) {
            event.preventDefault();

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

