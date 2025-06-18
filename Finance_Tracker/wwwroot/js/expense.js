let expenses = window.expenses = [
    { ID: 1, Title: "Groceries", Amount: 1200, Category: 1, Date: new Date(), Notes: "Monthly essentials" },
    { ID: 2, Title: "Uber", Amount: 450, Category: 2, Date: new Date(), Notes: "Office commute" },
    { ID: 2, Title: "Rent", Amount: 5000, Category: 3, Date: new Date(), Notes: "House Rent" },
    { ID: 2, Title: "Household things", Amount: 1200, Category: 4, Date: new Date(), Notes: "Household things" },
    { ID: 2, Title: "Medicines", Amount: 600, Category: 5, Date: new Date(), Notes: "Parent's meds" }
];

const categoriesData = [
    { category_id: 1, category_name: 'Food' },
    { category_id: 2, category_name: 'Travel' },
    { category_id: 3, category_name: 'Bills' },
    { category_id: 4, category_name: 'Shopping' },
    { category_id: 5, category_name: 'Health' },
];

$(() => {
    // Populate category dropdown
    const $categorySelect = $('#expenseCategory');
    $categorySelect.append(`<option value="" disabled selected>Select a category</option>`);
    categoriesData.forEach(cat => {
        $categorySelect.append(`<option value="${cat.category_id}">${cat.category_name}</option>`);
    });

    // Initialize pie chart
    const pieChart = $('#pie').dxPieChart({
        size: { width: 500 },
        palette: 'bright',
        dataSource: expenses,
        series: [{
            argumentField: 'Title',
            valueField: 'Amount',
            label: {
                visible: true,
                connector: { visible: true, width: 1 }
            },
        }],
        title: 'Your Spendings',
        export: { enabled: true },
        onPointClick(e) {
            toggleVisibility(e.target);
        },
        onLegendClick(e) {
            const arg = e.target;
            toggleVisibility(pieChart.getAllSeries()[0].getPointsByArg(arg)[0]);
        },
    }).dxPieChart('instance');

    function toggleVisibility(item) {
        if (item.isVisible()) {
            item.hide();
        } else {
            item.show();
        }
    }

    // Initialize DataGrid
    const grid = $('#expenseGrid').dxDataGrid({
        dataSource: expenses,
        keyExpr: 'ID',
        showBorders: true,
        paging: { enabled: false },
        editing: {
            mode: 'row',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: false,
            useIcons: true
        },
        columns: [
            { dataField: 'Title', caption: 'Title' },
            { dataField: 'Amount', caption: 'Amount', dataType: 'number' },
            {
                dataField: 'Category',
                caption: 'Category',
                lookup: {
                    dataSource: categoriesData,
                    displayExpr: 'category_name',
                    valueExpr: 'category_id',
                },
            },
            { dataField: 'Date', dataType: 'date' },
            { dataField: 'Notes', caption: 'Notes' },
            {
                type: 'buttons',
                width: 100,
                buttons: ['edit', 'delete']
            }
        ],
        onRowUpdated(e) {
            const updatedIndex = expenses.findIndex(item => item.ID === e.key);
            if (updatedIndex !== -1) {
                expenses[updatedIndex] = { ...expenses[updatedIndex], ...e.data };
                refreshData();
            }
        },
        onRowRemoved(e) {
            expenses = expenses.filter(item => item.ID !== e.key);
            refreshData();
        },
        onCellPrepared(e) {
            if (e.rowType === 'header') {
                e.cellElement.css({ 'font-weight': 'bold', 'text-align': 'center' });
            }
            if (e.rowType === 'data') {
                e.cellElement.css({ 'text-align': 'center', 'vertical-align': 'middle' });
            }
        }
    }).dxDataGrid('instance');

    function refreshData() {
        grid.option('dataSource', [...expenses]);
        pieChart.option('dataSource', [...expenses]);
    }

    // Handle form submit
    $('#addExpenseForm').on('submit', function (e) {
        e.preventDefault();

        const newExpense = {
            ID: Date.now(),
            Title: $('#expenseTitle').val(),
            Amount: parseFloat($('#expenseAmount').val()),
            Category: parseInt($('#expenseCategory').val()),
            Date: $('#expenseDate').val(),
            Notes: $('#expenseNote').val()
        };

        expenses.push(newExpense);
        refreshData();

        bootstrap.Modal.getInstance(document.getElementById('addExpenseModal')).hide();
        this.reset();
    });
});
