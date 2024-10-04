

function dashboardshow() {
    document.getElementById('dashboardcontainer').style.display = 'block';
    document.getElementById('customerdcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'none';
    document.getElementById('overduedcontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'none';
    document.getElementById('generatecontainer').style.display = 'none';
}

document.addEventListener('DOMContentLoaded', function () {
    const addBikeForm = document.getElementById('add-bike-form');
    const bikesTableBody = document.getElementById('bikes-table').querySelector('tbody');


    async function displayBikes() {
        try {
            const response = await fetch('http://localhost:5000/api/Manager/AllBikes', {
                method: 'GET',
            });


            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                const bikes = await response.json();
                bikesTableBody.innerHTML = '';
                // console.log(bikes);


                bikes.forEach((bike, index) => {
                    const row = document.createElement('tr');

                    const imageUrls = bike.imageUrl.split(',');
                    let imagesHtml = '';
                    imageUrls.forEach(url => {
                        const fullUrl = `http://localhost:5000${url}`.trim();
                        // console.log(fullUrl);
                        // Ensure proper URL with no spaces
                        imagesHtml += `<img src="${fullUrl}" alt="${bike.model}" style="max-width: 100px; margin-right: 10px;" />`;
                    });

                    row.innerHTML = `
                        <td>${imagesHtml}</td>
                        <td>${bike.title}</td>
                        <td>${bike.description}</td>
                        <td>${bike.regnumber}</td>
                        <td>${bike.brand}</td>
                        <td>${bike.model}</td>
                        <td>${bike.category}</td>
                        <td>${bike.isAvailable ? 'Available' : 'Not Available'}</td>
                        <td><button class="delete-button" data-id="${bike.id}">Delete</button></td>
                    `;
                    bikesTableBody.appendChild(row);
                });

                document.querySelectorAll('.delete-button').forEach(button => {
                    button.addEventListener('click', async function () {
                        const bikeId = this.getAttribute('data-id');
                        await deleteBike(bikeId);
                        displayBikes();
                    });
                });
            } else {
                const text = await response.text();
                console.error('Non-JSON response received:', text);
            }
        } catch (error) {
            console.error('Network error:', error);
        }
    }

    async function deleteBike(bikeId) {
        try {
            const response = await fetch(`http://localhost:5000/api/Manager/${bikeId}`, {
                method: 'DELETE',
            });

            if (response.ok) {
                console.log('Bike deleted successfully');
            } else {
                const contentType = response.headers.get('content-type');
                if (contentType && contentType.includes('application/json')) {
                    const errorData = await response.json();
                    console.error('Error deleting bike:', errorData);
                } else {
                    const text = await response.text();
                    console.error('Error deleting bike:', text);
                }
            }
        } catch (error) {
            console.error('Network error:', error);
        }
    }

    // Add bike submission
    addBikeForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const title = document.getElementById('add-bike-title').value.trim();
        const description = document.getElementById('add-bike-description').value.trim();
        const regNumber = document.getElementById('add-bike-reg-number').value.trim();
        const brand = document.getElementById('add-bike-brand').value.trim();
        const model = document.getElementById('add-bike-model').value.trim();
        const category = document.getElementById('add-bike-category').value.trim();
        const imageInput = document.getElementById('add-bike-image').files;

        if (!title || !description || !regNumber || !brand || !model || !category ) {
            alert('All fields are required.');
            return;
        }

        const formData = new FormData();
        formData.append("Title", title);
        formData.append("Regnumber", regNumber);
        formData.append("Brand", brand);
        formData.append("Category", category);
        formData.append("Description", description);
        formData.append("Model", model);
        for (let i = 0; i < imageInput.length; i++) {
            formData.append("ImageFile", imageInput[i]);
        }

        try {
            const response = await fetch('http://localhost:5000/api/Manager/AddBike', {
                method: 'POST',
                body: formData,
            });

            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                const data = await response.json();
                console.log('Bike added successfully:', data);
                displayBikes();
            } else {

                const text = await response.text();
                console.error('Non-JSON response received:', text);
            }
        } catch (error) {
            console.error('Network error:', error);
        }
    });

    displayBikes();
});


// customer show
function customershow() {
    document.getElementById('customerdcontainer').style.display = 'block';
    document.getElementById('dashboardcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'none';
    document.getElementById('overduedcontainer').style.display = 'none';
    document.getElementById('generatecontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'none';
}

async function displayCustomers() {
    try {
        // Fetch customers from the API
        const customerResponse = await fetch('http://localhost:5000/api/Customer/all');
        const customers = await customerResponse.json();

        const customerTable = document.getElementById('customer-body');
        customerTable.innerHTML = ''; // Clear existing rows

        // Iterate over customers and fetch their rentals
        customers.forEach(async (customer) => {
            const row = document.createElement('tr');
// console.log(customer);

            // Fetch rental data for the current customer using their ID
            const rentalResponse = await fetch(`http://localhost:5000/api/Customer/rentals/customer/${customer.id}`);
            const customerRentals = await rentalResponse.json();

            let rentalHistory = '<ul>';
            customerRentals.forEach(rental => {
                rentalHistory += `<li>Reg: ${rental.motorbikeID}, Date: ${rental.rentalDate}</li>`;
            });
            rentalHistory += '</ul>';
// console.log(customerRentals);

            row.innerHTML = `
                <td>${customer.firstName}</td>
                <td>${customer.nic}</td>
                <td>${customer.licence}</td>
                <td>${customer.mobilenumber}</td>
                <td>${rentalHistory}</td>
            `;
            customerTable.appendChild(row);
        });

        if (customers.length === 0) {
            const row = document.createElement('tr');
            row.innerHTML = '<td colspan="6">No customers found.</td>';
            customerTable.appendChild(row);
        }
    } catch (error) {
        console.error('Error fetching customer or rental data:', error);
        const customerTable = document.getElementById('customer-body');
        const row = document.createElement('tr');
        row.innerHTML = '<td colspan="6">Error fetching customer or rental data.</td>';
        customerTable.appendChild(row);
    }
}

displayCustomers();
// Load customers on page load
window.onload = displayCustomers;



// Function to show the rental section and hide others
function rentalshow() {
    document.getElementById('dashboardcontainer').style.display = 'none';
    document.getElementById('customerdcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'block';
    document.getElementById('overduedcontainer').style.display = 'none';
    document.getElementById('generatecontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'none';
}

// Function to display rentals
async function displayRentals() {
    try {
        // Fetch rentals from the API
        const rentalResponse = await fetch('http://localhost:5000/api/Customer/GetAllRentals');
        const rentals = await rentalResponse.json();
        //   console.log(rentals);

        // Fetch customers and motorbikes from their respective APIs
        const customerResponse = await fetch('http://localhost:5000/api/Customer/all');
        const customers = await customerResponse.json();
        // console.log(customers);

        const bikeResponse = await fetch('http://localhost:5000/api/Manager/AllBikes');
        const bikes = await bikeResponse.json();
        // console.log(bikes);

        const rentalTable = document.getElementById('rental-body');
        rentalTable.innerHTML = ''; // Clear existing rows

        rentals.forEach((rental) => {
            // Find the corresponding customer and motorbike
            const customer = customers.find(c => c.id === rental.customerID) || { firstName: 'Unknown', nic: 'Unknown', mobilenumber: 'Unknown' };
            const bike = bikes.find(b => b.id === rental.motorbikeID) || { regNumber: 'Unknown' };

            // Create a row for each rental
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${customer.nic}</td>
                <td>${customer.firstName}</td>
                <td>${customer.mobilenumber}</td>
                <td>${bike.regnumber}</td>
                <td>${rental.rentalDate}</td>
                <td>${rental.status}</td>
                <td>
                    <button class="btn btn-success btn-sm" onclick="acceptRental('${rental.id}')">Accept</button>
                    <button class="btn btn-danger btn-sm" onclick="rejectRental('${rental.id}')">Reject</button>
                </td>
            `;
            rentalTable.appendChild(row);
        });

        // If no rentals are found, show a message
        if (rentals.length === 0) {
            const row = document.createElement('tr');
            row.innerHTML = '<td colspan="7">No rentals found.</td>';
            rentalTable.appendChild(row);
        }
    } catch (error) {
        console.error('Error fetching rentals:', error);
        const rentalTable = document.getElementById('rental-body');
        const row = document.createElement('tr');
        row.innerHTML = '<td colspan="7">Error fetching rentals.</td>';
        rentalTable.appendChild(row);
    }
}

// Call displayRentals on page load
window.onload = displayRentals;


// Function to accept a rental request
async function acceptRental(rentalId) {
    try {
        const response = await fetch(`http://localhost:5000/api/Rentals/accept/${rentalId}`, {
            method: 'PATCH', // or PUT, depending on your API design
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ status: 'Accepted' }),
        });

        if (response.ok) {
            console.log('Rental accepted successfully');
            displayrentals(); // Refresh the rentals table
        } else {
            console.error('Error accepting rental:', await response.text());
        }
    } catch (error) {
        console.error('Network error:', error);
    }
}

// Function to reject a rental request
async function rejectRental(rentalId) {
    try {
        const response = await fetch(`http://localhost:5000/api/Rentals/reject/${rentalId}`, {
            method: 'DELETE', // Assuming DELETE is used for rejection
        });

        if (response.ok) {
            console.log('Rental rejected successfully');
            displayrentals(); // Refresh the rentals table
        } else {
            console.error('Error rejecting rental:', await response.text());
        }
    } catch (error) {
        console.error('Network error:', error);
    }
}
// Initialize the rental display on page load

displayrentals();




// overdue alert
function overdueshow() {
    document.getElementById('dashboardcontainer').style.display = 'none';
    document.getElementById('customerdcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'none';
    document.getElementById('overduedcontainer').style.display = 'block';
    document.getElementById('generatecontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'none';
    // console.log("tamil");
}

function checkOverdueRentals() {
    let customers = JSON.parse(localStorage.getItem('customers')) || [];
    console.log(customers)
    const now = new Date();
    const overdueList = document.getElementById('overdue-list');
    overdueList.innerHTML = '';

    customers.forEach(customer => {
        customer.rentalHistory.forEach(rental => {
            const returnDate = new Date(rental.returnDate);
            if (!rental.returnProcessed && returnDate < now) {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${customer.nic}</td>
                     <td>${customer.username}</td>
                    <td>${rental.regNumber}</td>
                    <td>${new Date(rental.rentalDate).toLocaleString()}</td>
                    <td>${returnDate.toLocaleString()}</p>
                    <td>${(now - returnDate) / (1000 * 60 * 60)}</td>
                `;
                overdueList.appendChild(row);
            }
        });
    });

    if (overduelist.innerHTML === '') {
        overduelist.innerHTML = 'No overdue rentals found';
    }
}
window.onload = checkOverdueRentals();


function returnshow() {
    document.getElementById('customerdcontainer').style.display = 'none';
    document.getElementById('dashboardcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'none';
    document.getElementById('overduedcontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'block';
    document.getElementById('generatecontainer').style.display = 'none';

}

// Function to return motorbike
async function returnMotorbike() {
    const nic = document.getElementById('return-nic').value;
    const registrationNumber = document.getElementById('return-registration').value;

    try {
        // Fetch all customers, motorbikes, and rentals
        let [customersResponse, motorbikesResponse, rentalsResponse] = await Promise.all([
            fetch('http://localhost:5000/api/Customer/all'),
            fetch('http://localhost:5000/api/Manager/AllBikes'),
            fetch('http://localhost:5000/api/Customer/GetAllRentals')
        ]);

        const customers = await customersResponse.json();
        const motorbikes = await motorbikesResponse.json();
        const rentals = await rentalsResponse.json();
console.log(motorbikes);
console.log(customers);

        // Find the customer by NIC
        const customer = customers.find(c => c.nic === nic);
        if (!customer) {
            alert('Customer not found');
            return;
        }

        // Find the motorbike by registration number
        const motorbike = motorbikes.find(b => b.regnumber === registrationNumber);
        if (!motorbike) {
            alert('Motorbike not found');
            return;
        }

        // Find the rental associated with the customer and motorbike
        const rental = rentals.find(r => r.nic === nic && r.regnumber === registrationNumber && !r.returnProcessed);
        if (!rental) {
            alert('Rental record not found or already processed');
            return;
        }

        // Send a PUT request to mark the bike as returned using bike-return endpoint
        const returnBikeResponse = await fetch(`http://localhost:5000/api/customer/bike-return/${rental.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                returnProcessed: true
            })
        });

        if (!returnBikeResponse.ok) {
            alert('Failed to process bike return');
            return;
        }

        // Update motorbike quantity (increment by 1)
        motorbike.quantity += 1;

        // Send a PUT request to update the motorbike quantity
        const updateBikeResponse = await fetch(`http://localhost:5000/api/bike/update/${motorbike.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                quantity: motorbike.quantity
            })
        });

        if (!updateBikeResponse.ok) {
            alert('Failed to update motorbike quantity');
            return;
        }

        alert('Motorbike returned successfully!');
        document.getElementById('return-motorbike-form').reset();

    } catch (error) {
        console.error('Error during motorbike return:', error);
        alert('An error occurred while processing the return.');
    }
}

// Attach form submission handler
window.onload = function () {
    const form = document.getElementById('return-motorbike-form');
    form.onsubmit = function (event) {
        event.preventDefault(); // Prevent form submission to server
        returnMotorbike();
    };
};


//report

function ReportMenu(reportType) {
    // Hide all sections
    document.getElementById('dashboardcontainer').style.display = 'none';
    document.getElementById('customerdcontainer').style.display = 'none';
    document.getElementById('rentaldcontainer').style.display = 'none';
    document.getElementById('overduedcontainer').style.display = 'none';
    document.getElementById('returncontainer').style.display = 'none';
    document.getElementById('generatecontainer').style.display = 'block';

    // Hide all report containers
    document.getElementById('rental-report-container').style.display = 'none';
    document.getElementById('customer-report-container').style.display = 'none';
    document.getElementById('bike-report-container').style.display = 'none';

    // Show the selected report container
    if (reportType === 'rental') {
        document.getElementById('rental-report-container').style.display = 'block';
    } else if (reportType === 'customer') {
        document.getElementById('customer-report-container').style.display = 'block';
    } else if (reportType === 'bike') {
        document.getElementById('bike-report-container').style.display = 'block';
    }
}


window.onload = function () {
    generateReports();
};

async function generateReports() {
    await generateRentalReport();
    await generateCustomerReport();
    await generateBikeReport();
}

async function generateRentalReport() {
    try {
        const rentalResponse = await fetch('http://localhost:5000/api/Customer/GetAllRentals');
        const rentals = await rentalResponse.json();
        const rentalTableBody = document.getElementById('rental-report-table').getElementsByTagName('tbody')[0];

        // Clear existing rows
        rentalTableBody.innerHTML = '';

        rentals.forEach(rental => {
            let row = rentalTableBody.insertRow();

            row.insertCell(0).textContent = rental.nic;
            row.insertCell(1).textContent = rental.regNumber;
            row.insertCell(2).textContent = rental.rentDate;
            row.insertCell(3).textContent = rental.returnProcessed ? 'Yes' : 'No';
        });
    } catch (error) {
        console.error('Error fetching rentals:', error);
    }
}

async function generateCustomerReport() {
    try {
        const customerResponse = await fetch('http://localhost:5000/api/Customer/all');
        const customers = await customerResponse.json();
        const customerTableBody = document.getElementById('customer-report-table').getElementsByTagName('tbody')[0];

        // Clear existing rows
        customerTableBody.innerHTML = '';
console.log(customers);

        customers.forEach(customer => {
            let row = customerTableBody.insertRow();

            row.insertCell(0).textContent = customer.nic;
            row.insertCell(1).textContent = customer.firstName;
            row.insertCell(2).textContent = customer.licence;
            row.insertCell(3).textContent = customer.mobilenumber;
            row.insertCell(4).textContent = customer.rentalHistory.length; // Assuming rentalHistory is part of the customer object
        });
    } catch (error) {
        console.error('Error fetching customers:', error);
    }
}

async function generateBikeReport() {
    try {
        const bikeResponse = await fetch('http://localhost:5000/api/Manager/AllBikes');
        const motorbikes = await bikeResponse.json();
        const bikeTableBody = document.getElementById('bike-report-table').getElementsByTagName('tbody')[0];

        // Clear existing rows
        bikeTableBody.innerHTML = '';

        motorbikes.forEach(bike => {
            let row = bikeTableBody.insertRow();

            row.insertCell(0).textContent = bike.regNumber;
            row.insertCell(1).textContent = bike.brand;
            row.insertCell(2).textContent = bike.model;
            row.insertCell(3).textContent = bike.category;
            row.insertCell(4).textContent = bike.quantity;
        });
    } catch (error) {
        console.error('Error fetching motorbikes:', error);
    }
}

// Call this function to generate the reports on page load or when required
window.onload = generateReports;
