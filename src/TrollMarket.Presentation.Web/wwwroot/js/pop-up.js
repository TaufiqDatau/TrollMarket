/*

All of the javascript below required this html Modal

<div id="myModal" class="modal">
    <div class="modal-content">
        <div class="modal-header">
            <span class="close" id="closeModalBtn">&times;</span>
            <h2></h2>
        </div>
        <div class="modal-body">
        </div>
        <div class="modal-footer">
            <!-- Add footer content here, such as buttons or additional information -->
        </div>
    </div>
</div>

*/


/**
 * Shows the popup modal.
 *
 * @returns {void}
 */
function ShowPopUp() {
    const PopUpModal = document.querySelector("#myModal");
    PopUpModal.style.display = "block";
}

/**
 * Closes the popup modal and clears its content.
 *
 * @returns {void}
 */
function ClosePopUp() {
    let modal = document.querySelector("#myModal");

    modal.style.display = null;
    ClearModalContent();
}

/**
 * Clears the content of the popup modal.
 *
 * @returns {void}
 */
function ClearModalContent() {
    let popUpHeader = document.querySelector(".modal-header h2");
    let popUpBody = document.querySelector(".modal-body");
    popUpHeader.innerHTML = "";
    popUpBody.innerHTML = "";
}

/**
 * Collects input values from specified input fields and returns a data object.
 *
 * @param {string[]} inputFieldIds - An array of input field IDs to collect values from.
 * @returns {Object} - An object containing input field values with field IDs as keys.
 * @example
 * const inputFields = ["id", "title", "name"];
 * const formData = collectInputForms(inputFields);
 */
function CollectInputForms(inputFieldIds) {
    let dto = {};

    inputFieldIds.forEach((fieldId) => {
        const inputElement = document.getElementById(fieldId);

        if (inputElement) {
            if (inputElement.type === 'checkbox') {
                // For checkboxes, store a boolean indicating whether it is checked
                dto[fieldId] = inputElement.checked;
            } else if (inputElement.type === 'radio') {
                // For radio buttons, store the value only if it is checked
                if (inputElement.checked) {
                    dto[fieldId] = inputElement.value;
                }
            } else if (inputElement.tagName === 'SELECT') {
                // For select elements, store the selected option value
                dto[fieldId] = inputElement.value;
            } else if (inputElement.tagName === 'OPTION') {
                // For option elements, store the value if it is selected
                dto[fieldId] = inputElement.selected ? inputElement.value : "";
            } else if (inputElement.type === "number") {
                dto[fieldId] = inputElement.value === "" ? "0" : inputElement.value;
            } else {
                // For other input types (text, textarea, etc.), store the value
                dto[fieldId] = inputElement.value;
            }
        } else {
            dto[fieldId] = ""; // Set default value for non-existent elements
        }
    });

    return dto;
}


/**
 * Creates input forms in a modal body.
 *
 * @param {string} method - The method to be used for API call (e.g., "POST", "PUT", "PATCH").
 * @param {string} formTemplate - The HTML template for the input forms.
 * @returns {void}
 */
function createInputForms(method, formTemplate) {
    const modalBody = document.querySelector(".modal-body");

    // Set the inner HTML of the modal body to the provided form template
    modalBody.innerHTML = formTemplate;

    // Add an event listener for the submit button
    AddSubmitButtonEventListener(method);
}




function writeValidationMessage(errorMessages) {
    for (let field in errorMessages) {
        let messages = errorMessages[field];
        if (Array.isArray(messages) && messages.length > 0) {
            let message = messages[0];
            $(`.form-dialog [data-for="${field}"]`).text(message);
        }

    }
}