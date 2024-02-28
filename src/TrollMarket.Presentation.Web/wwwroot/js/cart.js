function RemoveFromCart(buyer, productId, shipmentId) {
    let dto = {};
    dto["buyer"] = buyer;
    dto["productId"] = productId;
    dto["shipmentId"] = shipmentId;
    dto["quantity"] = 1;

    let url = `http://localhost:5089/api/shop`;
    let request = new XMLHttpRequest();
    request.open("DELETE", url);
    request.setRequestHeader("content-type", "application/json");
    request.send(JSON.stringify(dto));
    request.onload = function () {
        if (request.status === 204) {
            // Show SweetAlert
            swal({
                title: 'Success!',
                text: 'Product has been removed from the cart.',
                icon: 'success',
                confirmButtonText: 'OK'
            });

            // Reload the page after 700ms
            setTimeout(function () {
                location.reload();
            }, 700);
        } else {
            let response = JSON.parse(request.responseText);
            console.log(response);
        }
    }
}

function PurchaseAll(buyer) {
    let url = `http://localhost:5089/api/transaction/${buyer}/purchase`;
    let request = new XMLHttpRequest();
    request.open("POST", url);
    request.send();
    request.onload = function () {
        if (request.status === 200) {
            // API call succeeded
            swal({
                title: "Success!",
                text: "Purchase completed successfully.",
                icon: "success",
            }).then(() => {
                // Reload the page after a short delay
                setTimeout(() => {
                    location.reload();
                }, 1000); // 1000 milliseconds (1 second) delay
            });
        } else {
            // API call failed
            let errorMessage = "An error occurred.";
            if (request.responseText) {

                const response = JSON.parse(request.responseText);
                errorMessage = response.errorMessage || errorMessage;

                swal({
                    title: "Error!",
                    text: errorMessage,
                    icon: "error",
                });
            }
        };
    }
}