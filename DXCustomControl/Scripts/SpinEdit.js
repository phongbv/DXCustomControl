function getNewValue(inputElement, addingValue, decimalPoint, thounsandPoint, maxTotalDigit) {
    var bindex = inputElement.selectionStart;
    var eindex = inputElement.selectionEnd;
    var oldValue = inputElement.value == null ? "" : inputElement.value.remove(bindex, eindex);
    // Viet them dau -
    if (addingValue === '-') {
        if (bindex != 0) {
            return null;
        } else {
            return (oldValue == null ? "" : addingValue + oldValue);
        }
    }
    else {
        var newValue = (oldValue == null ? "" : oldValue) + addingValue;
        return newValue;
    }
}

function isPassNumberLengthRange(value, decimalPoint, thounsandPoint, maxTotalDigit) {
    var currentTotalDigit = 0;
    if (value != null)
        currentTotalDigit = value.toString().replace(thounsandPoint, "").replace("-", "").replace(decimalPoint, "").length;
    return maxTotalDigit != undefined && maxTotalDigit != null && currentTotalDigit > maxTotalDigit ? false : true;
}

function KeyType(e) {
    // Allow: backspace, delete, tab, escape, enter , . and ','
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
        // Allow: Ctrl/cmd+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: Ctrl/cmd+C
        (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: Ctrl/cmd+X
        (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39) ||
        // Allow: co
        (e.keyCode == 86 && (e.ctrlKey === true || e.metaKey === true))) {
        // let it happen, don't do anything
        return 1;
    }
    // Ensure that it is a number and stop the keypress
    if (($.inArray(e.keyCode, [188, 189, 190]) == -1) && (e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        return -1;
    }
    return 0;
}
var tmp = null;
function isFloat(n, decimalPoint) {
    var numStr = '^[+-]?\\d+(\\' + decimalPoint + '\\d+)?$|^-$|^[+-]?\\d+\\' + decimalPoint + '$';
    var result = new RegExp(numStr).test(n.toString());
    return result;
}
function isInteger(n) {
    var numStr = /^\d+$|^-?\d+$|^-$/;
    return numStr.test(n.toString());
}
function OnSpinEditKeyDown(spinEdit, event) {
    var evtCheckResult = KeyType(event.htmlEvent);
    if (evtCheckResult == -1) {
        event.htmlEvent.returnValue = false;
    } else if (evtCheckResult == 1) {
        return;
    }
    var newValue = getNewValue(spinEdit.GetInputElement(), event.htmlEvent.key, spinEdit.DecimalPoint, spinEdit.ThousandPoint, spinEdit.MaxTotalDigit);
    if (!isPassNumberLengthRange(newValue, spinEdit.DecimalPoint, spinEdit.ThousandPoint, spinEdit.MaxTotalDigit)) {
        RaiseWarningMessage(spinEdit.GetInputElement(), 'Max total digits is ' + spinEdit.MaxTotalDigit);
        event.htmlEvent.returnValue = false;
    } else
        if (newValue == null || (!isFloat(newValue, spinEdit.DecimalPoint) && spinEdit.NumberType == 'f') || (!isInteger(newValue) && spinEdit.NumberType == 'i')) {
            event.htmlEvent.returnValue = false;
        }
}

function SpinEditFloat(sender, event) {
    var evtCheckResult = KeyType(event.htmlEvent);
    if (evtCheckResult == -1) {
        event.htmlEvent.returnValue = false;
    } else if (evtCheckResult == 1) {
        return;
    }
    var newValue = getNewValue(sender.GetInputElement(), event.htmlEvent, sender.MaxTotalDigit)
    if (newValue == null || !isFloat(newValue, sender.DecimalPoint)) {
        event.htmlEvent.returnValue = false;
    }
}


function SpinEditInteger(sender, event) {
    var evtCheckResult = KeyType(event);
    if (evtCheckResult == -1) {
        event.htmlEvent.returnValue = false;
    } else if (evtCheckResult == 1) {
        return;
    }
    var newValue = getNewValue(sender, event, sender.MaxTotalDigit)
    if (newValue == null || !isInteger(newValue)) {
        event.htmlEvent.returnValue = false;
    }
}

String.prototype.remove = function (bindex, eindex) {
    var result = this.split('');
    result.splice(bindex, eindex - bindex);
    var resultString = result.join('');
    return resultString;
}
String.prototype.formatNumber = function (decimalPoint, thousandPoint, prefix, subfix) {
    prefix = prefix == null || prefix == undefined ? '' : prefix;
    subfix = subfix == null || subfix == undefined ? '' : subfix;
    x = this.replace('.', decimalPoint).split(decimalPoint);
    x1 = x[0];
    x2 = x.length > 1 ? decimalPoint + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + thousandPoint + '$2');
    }
    return prefix + x1 + x2 + subfix;
}
function InitTextBox(spinEdit) {
    var input = spinEdit.GetInputElement();
    spinEdit.autoCompleteAttribute = { name: 'autocomplete', value: 'off' }
    $(input).bind("paste", function (e) {
        clipboardData = e.originalEvent.clipboardData || window.clipboardData;
        pastedData = clipboardData.getData('Text');
        var newValue = getNewValue(this, pastedData, spinEdit.DecimalPoint, spinEdit.ThousandPoint, spinEdit.MaxTotalDigit);
        if (newValue == null || (!isFloat(newValue, spinEdit.DecimalPoint) && spinEdit.NumberType == 'f') || (!isInteger(newValue) && spinEdit.NumberType == 'i')) {
            RaiseWarningMessage(input, 'The data which you have pasted is not invalid');
            e.preventDefault();
        } else {
            $(input).parents(".warning-control")[0].removeAttribute('data-warning')
        }

        // Do whatever with pasteddata
        // alert(pastedData);
    })
}

function RaiseWarningMessage(input, message) {
    $(input).parents(".warning-control")[0].setAttribute('data-warning', message)
    setTimeout(function () {
        $(input).parents(".warning-control")[0].removeAttribute('data-warning')
    }, 2500);
}
function OnLosFocusSpin(s, e) {
    if (s.GetValue() == null) {
        return;
    }
    //if (s.cpIsFormat) {
    //    s.cpIsFormat = false;
    //    return;
    //}
    var lstNumber = s.GetValue().split(s.DecimalPoint);
    if (lstNumber.length == 1) {
        return;
    }
    var stringVal = "0." + lstNumber[1];
    var value = Math.round10(parseFloat(stringVal), -10);
    if (value == 0 || value == 1) {
        s.SetValue(lstNumber[0]);
    } else {
        s.SetValue(lstNumber[0] + s.DecimalPoint + value.toString().split('.')[1]);
    }
}

function ReFormatValue(s) {
    if (s.GetValue() != null)
        setTimeout(function () {
            s.SetText(s.GetValue().toString().formatNumber(s.DecimalPoint, s.ThousandPoint))
        }, 0)
}

ASPxClientTextEdit.prototype.GetDisplayFormatText = function (value) {
    if (this.IsCustomControl) {
        return value.toString().formatNumber(this.DecimalPoint, this.ThousandPoint, this.Prefix, this.Subfix);
    } else {
        return ASPx.Formatter.Format(this.displayFormat, value);
    }
}
ASPxClientTextEdit.prototype.UpdateFormat = function (thousandPoint, decimalPoint, prefix, subfix) {
    this.ThousandPoint = thousandPoint;
    this.DecimalPoint = decimalPoint;
    this.Prefix = prefix;
    this.Subfix = subfix;
    ReFormatValue(this)
}
ASPxClientTextEdit.prototype.GetValue = function () {
    var value = null;
    if (this.maskInfo != null)
        value = this.maskInfo.GetValue();
    else if (this.HasTextDecorators()) {
        var currentValue = this.GetRawValue();
        if (currentValue != null)
            value = currentValue.toString().replace(this.DecimalPoint, '.').remove(this.ThousandPoint);
    }
    else {
        var input = this.GetInputElement();
        value = input ? input.value : null;
    }
    return (value == "" && this.convertEmptyStringToNull) ? null : value;
}
//ASPxClientTextEdit.prototype.GetRawValue = function(value){
//    return ASPx.IsExists(this.stateObject) ? this.stateObject.rawValue.toString().replace(this.DecimalPoint, '.').remove(this.ThousandPoint) : null;
//}

function InitSpinEdit(s, e, MaxTotalDigit, MaxDigitAfterDecimalPoint, ThousandPoint, DecimalPoint, Prefix, Subfix, NumberType) {
    var rawValue = s.GetRawValue();
    s.MaxTotalDigit = MaxTotalDigit;
    s.MaxDigitAfterDecimalPoint = MaxDigitAfterDecimalPoint;
    s.ThousandPoint = ThousandPoint;
    s.DecimalPoint = DecimalPoint;
    s.Prefix = Prefix;
    s.Subfix = Subfix;
    s.IsCustomControl = true;
    s.NumberType = NumberType;
    InitTextBox(s);
    s.SetRawValue(rawValue);
    s.SetValue(rawValue);


}