const inputFields = ["shipmentId", "name", "price", "serviceStatus"]
function InsertForm() {
    $("#shipmentId").val(0);
    $("#shipment-form").modal("show");
}
function EditForm(id) {
    let request = new XMLHttpRequest();
    request.open("GET", `http://localhost:5089/api/shipment/${id}/GET`);
    request.send();
    request.onload = function () {
        let response = JSON.parse(request.responseText);
        if (request.status === 400) {
            swal("Error", `${response.errorMessage}`, "error");
        }else if (response.serviceStatus) {
            swal("Error", `You cannot edit this shipment since it already stopped`, "error");
        } else {
            populateForm(response);
            $("#shipment-form").modal("show");
        }
    }
}
function populateForm(data) {
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            var value = data[key];
            // Check if the element with the corresponding id exists
            if ($("#" + key).length > 0) {
                // For checkboxes, use .prop("checked")
                if ($("#" + key).is(":checkbox")) {
                    $("#" + key).prop("checked", value);
                } else {
                    $("#" + key).val(value);
                }
            }
        }
    }
}
function CloseForm() {
    $("#shipment-form").modal("hide");
}
function StopService(id) {
    let url = `http://localhost:5089/api/shipment/${id}/stopService/PATCH`;
    let request = new XMLHttpRequest();
    request.open("PATCH", url);
    request.send();
    request.onload = function () {
        if (request.status === 200) {
            swal({
                title: "Success!",
                text: "New shipment company has been added",
                icon: "success",
                buttons: {
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-success",
                        closeModal: false,
                    },
                },
                allowOutsideClick: false, // Prevent clicking outside the modal to close it
                showCancelButton: false, // Do not show the cancel button
            }).then(() => {
                setTimeout(function () {
                    window.location.reload();
                }, 500);
            });
        } else {
            let response = JSON.parse(request.responseText);
            swal("Error", `${response.errorMessage}`, "error");
        }
    }
}
function DeleteShipment(id) {
    let url = `http://localhost:5089/api/shipment/${id}/DELETE`;
    let request = new XMLHttpRequest();
    request.open("DELETE", url);
    request.send();
    request.onload = function () {
        if (request.status === 200) {
            swal({
                title: "Success!",
                text: "Shipment has been deleted",
                icon: "success",
                buttons: {
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-success",
                        closeModal: false,
                    },
                },
                allowOutsideClick: false, // Prevent clicking outside the modal to close it
                showCancelButton: false, // Do not show the cancel button
            }).then(() => {
                setTimeout(function () {
                    window.location.reload();
                }, 500);
            });
        } else {
            let response = JSON.parse(request.responseText);
            swal("Error", `${response.errorMessage}`, "error");
        }
    }
}
function SubmitForm() {
    if (validateForm()) {
        let dto = CollectInputForms(inputFields);
        dto.serviceStatus = $("#serviceStatus").prop("checked");
        let method = "POST";
        if (dto.shipmentId !== '0') {
            method = "PUT";
        }
        let request = new XMLHttpRequest();
        request.open(method, `http://localhost:5089/api/shipment`);
        request.setRequestHeader("content-type", "application/json");
        request.send(JSON.stringify(dto));
        request.onload = function () {
            if (request.status === 201) {
                swal({
                    title: "Success!",
                    text: "New shipment company has been added",
                    icon: "success",
                    buttons: {
                        confirm: {
                            text: "OK",
                            value: true,
                            visible: true,
                            className: "btn btn-success",
                            closeModal: false,
                        },
                    },
                    allowOutsideClick: false, // Prevent clicking outside the modal to close it
                    showCancelButton: false, // Do not show the cancel button
                }).then(() => {
                    setTimeout(function () {
                        window.location.reload();
                    }, 500);
                });
            } else if (request.status === 200) {
                swal({
                    title: "Success!",
                    text: "Product has been edited",
                    icon: "success",
                    buttons: {
                        confirm: {
                            text: "OK",
                            value: true,
                            visible: true,
                            className: "btn btn-success",
                            closeModal: false,
                        },
                    },
                    allowOutsideClick: false, // Prevent clicking outside the modal to close it
                    showCancelButton: false, // Do not show the cancel button
                }).then(() => {
                    setTimeout(function () {
                        window.location.reload();
                    }, 500);
                });
            } else if (request.status === 400) {
                let response = JSON.parse(request.responseText);
                writeValidationMessage(response.errors);
            }
            else {

                let response = JSON.parse(request.responseText);
                swal("Error", `${response.errorMessage}`, "error");
            }
        }
    }
    
}

function validateForm() {
    // Get the input value using jQuery
    var userInput = $('#price').val();

    // Check if the input is a valid number
    if (isNaN(userInput)) {
        // Display an error message if it's not a number
        $('.form-dialog [data-for="Price"]').text('Please enter a valid number.');
        return false; // Prevent form submission
    }

    // Attempt to parse the input as an integer
    try {
        var parsedInput = parseInt(userInput, 10);

        // Check if the parsed value is too large (arbitrary limit set to 1000000)
        if (parsedInput > 1000000) {
            // Display an error message if the number is too large
            $('.form-dialog [data-for="Price"]').text('Number is too large. Please enter a smaller number.');
            return false; // Prevent form submission
        }
    } catch (error) {
        // Handle the parse error
        $('.form-dialog [data-for="Price"]').text('Quantity is too large');
        return false; // Prevent form submission
    }

    // Clear any previous error messages
    $('.form-dialog [data-for="Price"]').text('');

    // Allow form submission if everything is valid
    return true;
}