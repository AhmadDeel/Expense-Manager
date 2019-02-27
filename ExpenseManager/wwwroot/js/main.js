const categoryChange = (dropDownValue) => {
    const url = "/Expense/Category?cat=" + dropDownValue;
    $('body').load(url);
};

const AddEditExpenses = (itemId) => {
    const url = "/Expense/AddEditExpenses?itemId=" + itemId;
    if (itemId > 0) {
        $('#title').html("ویرایش");
    }
    $("#expenseFormModelDiv").load(url, () => {
        $("#expenseFormModel").modal("show");
    });
    $('#expenseFormModel').on('shown.bs.modal', () => {
        $('#calender-container .input-group.date').datepicker({
            todayBtn: true,
            calendarWeeks: true,
            todayHighlight: true,
            autoclose: true,
        }).datepicker('update', new Date());
    });
};

const ReportExpense = () => {
    const url = "/Expense/ExpenseSummary";
    $("#expenseReportModalDiv").load(url, () => {
        $("#expenseReportModal").modal("show");
    });
};

const DeleteExpense = (itemId) => {
    const ans = confirm("آیا مطمئن هستید: ");
    if (ans) {
        $.ajax({
            type: "POST",
            url: "/Expense/Delete?itemId=" + itemId,
            success: () => {
                window.location.href = "/Expense/Index";
            }
        });
    }
};

const addData = () => {
    const myformdata = $("#expenseForm").serialize();
    $.ajax({
        type: "POST",
        url: "/Expense/Create",
        data: myformdata,
        success: () => {
            $("#myModal").modal("hide");
            window.location.href = "/Expense/Index";
        },
        error: (errormessage) => {
            alert(errormessage.responseText);
        }
    });
};
