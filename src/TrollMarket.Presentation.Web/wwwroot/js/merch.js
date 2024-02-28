(function () {
    AddDiscontinueButtonListener();

}())
function ClosePopUpInfo() {
    $('#productInfoModal').modal('hide');
}

function AddDiscontinueButtonListener() {
    // Get the button element
    var discontinueButtons = document.querySelectorAll('.discontinue-btn');

    // Add a click event listener
    discontinueButtons.forEach(btn => {
        btn.addEventListener('click', function () {
            // Retrieve the data-id attribute value
            var dataId = this.getAttribute('data-id');
            const url = `http://localhost:5089/api/product/${dataId}/discontinue`;
            let request = new XMLHttpRequest();
            request.open("PATCH", url);
            request.send();
            request.onload = function () {
                if (request.status === 200) {
                    swal({
                        title: "Success!",
                        text: "Product has been discontinued",
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
           
        });
    })
    
}

function fetchAndShowProductInfo(button) {
    // Retrieve the data-id attribute value from the button
    var dataId = button.getAttribute('data-id');

    // Construct the API URL
    const apiUrl = `http://localhost:5089/api/product/${dataId}/infodetail`;

    // Create a new XMLHttpRequest object
    var request = new XMLHttpRequest();

    // Set up the GET request
    request.open('GET', apiUrl);
    request.send();
    // Define the onload function to handle the response
    request.onload = function () {
        if (request.status >= 200 && request.status < 400) {
            // Parse the JSON response
            var productInfo = JSON.parse(request.responseText);

            // Update the modal content with the fetched data
            updateModalContent(productInfo);

            // Show the modal
            $('#productInfoModal').modal('show');
        } else {
            swal("Error", "Failed to fetch product information", "error");
        }
    };



    // Send the request

}

function updateModalContent(productInfo) {
    // Get the modal body element
    var modalBody = document.getElementById('productInfoBody');

    // Create an HTML string with the product information
    var productHTML = `
      <p><strong>Name:</strong> ${productInfo.name}</p>
      <p><strong>Category:</strong> ${productInfo.category}</p>
      <p><strong>Description:</strong> ${productInfo.description}</p>
      <p><strong>Price:</strong> ${productInfo.price}</p>
      <p><strong>Discontinue:</strong> ${productInfo.discontinueStatus}</p>
    `;

    // Set the inner HTML of the modal body with the product information
    modalBody.innerHTML = productHTML;
}
