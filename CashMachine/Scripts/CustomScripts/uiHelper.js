
$(document).ready(function () {
    clearCreditNumber($("table").attr("id"));
    $("table :input").click(function () {
        var tableId = $("table").attr("id");
        var input, hiddenInput, inputMaxLength;
        if (tableId === "credit-card") {
            input = $("#masked-card-number");
            hiddenInput = $("#card-number");
            inputMaxLength = 19;
        } else if (tableId === "pin") {
            input = $("#masked-pin");
            hiddenInput = $("#pin");
            inputMaxLength = 4;
        } else {
            var input = $("#withdraw");
        }
        var currentValue = input.val();

        if (tableId === "t-withdraw") {
            input.val(currentValue + this.value);
            return;
        }

        if (this.value !== "*" && this.value !== "#" && currentValue.length !== inputMaxLength) {
            hiddenInput.val((hiddenInput.val() + this.value));
            if (tableId === "credit-card") {
                input.val((currentValue + this.value).maskCreditCard());
            } else {
                input.val(input.val() + "*");
            }
        }
    });
});

String.prototype.maskCreditCard = function () {
    return this.replace(/[^0-9]/g, "").substr(0, 16).split("").reduce(cardFormat, "");
    function cardFormat(str, l, i) {
        return str + ((!i || (i % 4)) ? "" : "-") + l;
    }
}

function clearCreditNumber(tableId) {
    if (tableId === "credit-card") {
        $("#card-number").val("");
        $("#masked-card-number").val("");
    } else if (tableId === "pin") {
        $("#pin").val("");
        $("#masked-pin").val("");
    } else {
        $("#withdraw").val("");
    }
}

