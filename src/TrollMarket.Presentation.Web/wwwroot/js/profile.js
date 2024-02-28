const fieldId=["Amount","Username"]
function SubmitDeposit(method) {
    let dto = CollectInputForms(fieldId);

    let request = new XMLHttpRequest();
    request.open(method, "http://localhost:5089/api/transaction/topup");
    request.setRequestHeader('Content-type', 'application/json');
    request.send(JSON.stringify(dto));

    request.onload = function () {


        if (request.status === 200) {
            // Set a timeout to trigger the fade-out animation and remove the alert
            swal("Success!", "Deposit successful", "success");
            setTimeout(function () {
                location.reload(); // Reload the page after a delay of 900ms
            }, 900);
            
        } else {
            let response = JSON.parse(request.responseText);
            writeValidationMessage(response.errors);
        }
    };
}
function AddSubmitButtonEventListener(method) {
    const submitButton = document.querySelector("#submit-button");
    submitButton.addEventListener("click", function () {
        SubmitDeposit(method)
    });
}
(function () {
    AddSubmitButtonEventListener("PATCH");
}())

