const fieldsId=["buyer","productId","shipmentId","quantity"]
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
    let modalBody = document.getElementById('productInfoBody');

    // Create an HTML string with the product information
    let productHTML = `
      <p><strong>Name:</strong> ${productInfo.name}</p>
      <p><strong>Category:</strong> ${productInfo.category}</p>
      <p><strong>Description:</strong> ${productInfo.description}</p>
      <p><strong>Price:</strong> ${productInfo.price}</p>
      <p><strong>Seller Name:</strong> ${productInfo.sellerName}</p>
    `;

    // Set the inner HTML of the modal body with the product information
    modalBody.innerHTML = productHTML;
}
function AddToCart(ProductId) {
    $("#productId").val(ProductId);
    $("#cart-form").modal("show");
}
function CloseForm() {
    $("#cart-form").modal("hide");
}
function ClosePopUpInfo() {
    $('#productInfoModal').modal('hide');
}
function SubmitCartForm() {
    if (validateForm()) {
        let url = `http://localhost:5089/api/shop/add`;
        let dto = CollectInputForms(fieldsId);

        let request = new XMLHttpRequest();
        request.open("POST", url);
        request.setRequestHeader("content-type", "application/json");
        request.send(JSON.stringify(dto));
        request.onload = function () {
            if (request.status == 201) {
                swal("Success!", "Succesfully added to Cart", "success");
                setTimeout(function () {
                    location.reload(); // Reload the page after a delay of 900ms
                }, 900);
            } else if (request.status === 200) {
                swal("Success!", "Cart updated", "success");
                setTimeout(function () {
                    location.reload(); // Reload the page after a delay of 900ms
                }, 900);
            } else if (request.status === 400) {
                let response = JSON.parse(request.responseText);
                writeValidationMessage(response.errors)
            }
            else {
                let response = JSON.parse(request.responseText);
                swal("Error!", `${response.errorMessage}`, "error");
            }
        }
    }
    
}
function validateForm() {
    // Get the input value using jQuery
    var userInput = $('#quantity').val();

    // Check if the input is a valid number
    if (isNaN(userInput)) {
        // Display an error message if it's not a number
        $('.form-dialog [data-for="Quantity"]').text('Please enter a valid number.');
        return false; // Prevent form submission
    }

    // Attempt to parse the input as an integer
    try {
        var parsedInput = parseInt(userInput, 10);

        // Check if the parsed value is too large (arbitrary limit set to 1000000)
        if (parsedInput > 1000000) {
            // Display an error message if the number is too large
            $('.form-dialog [data-for="Quantity"]').text('Number is too large. Please enter a smaller number.');
            return false; // Prevent form submission
        }
    } catch (error) {
        // Handle the parse error
        $('.form-dialog [data-for="Quantity"]').text('Quantity is too large');
        return false; // Prevent form submission
    }

    // Clear any previous error messages
    $('.form-dialog [data-for="Quantity"]').text('');

    // Allow form submission if everything is valid
    return true;
}
