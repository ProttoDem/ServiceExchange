// Check if the user is already logged in when the script loads
document.addEventListener('DOMContentLoaded', function () {
    const token = localStorage.getItem('jwtToken');
    if (token) {
        // Hide the login form and show the logged in message
        document.getElementById('loginFormContainer').style.display = 'none';
        document.getElementById('loggedInMessage').style.display = 'block';
    }
});

document.getElementById('loginForm').onsubmit = async function (event) {
    event.preventDefault();

    async function loginUser() {
        const url = 'https://localhost/login';
        const data = {};

        try {
            const response = await fetch(url, {
                method: 'POST', // or 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data), // body data type must match "Content-Type" header
            });
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const json = await response.json();
            console.log("Login successful:", json);
            // Handle success - for example, you might save the received token to localStorage and redirect the user
        } catch (error) {
            console.error("Error during login:", error);
            // Handle errors - for example, you might display an error message to the user
        }
    }
};

function logout() {
    localStorage.removeItem('jwtToken');
    // Update UI or redirect as needed
    document.getElementById('loginFormContainer').style.display = 'block';
    document.getElementById('loggedInMessage').style.display = 'none';
}
