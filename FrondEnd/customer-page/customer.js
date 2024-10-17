document.addEventListener('DOMContentLoaded', function () {
    const logoutBtn = document.getElementById('logoutBtn');
    const userInfo = document.getElementById('userInfo');
    const availableMotorbikeBody = document.getElementById('rent-container');
    const myRentalsTableBody = document.getElementById('myRentalsTableBody');
    const rentalsModal = document.getElementById('rentalsModal');
    const closeRentalsModal = document.getElementById('closeRentalsModal');
    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
    let motorbikes = [];
    let rentals = [];

    // Display the current user's name
    if (userInfo && currentUser) {
        userInfo.textContent = `${currentUser.firstName}`;
    }

    // Fetch all motorbikes from server
    async function fetchMotorbikes() {
        try {
            const response = await fetch('http://localhost:5000/api/Manager/AllBikes');
            motorbikes = await response.json();
            displayAvailableMotorbikes();
        } catch (error) {
            console.error('Error fetching motorbikes:', error);
        }
    }

    // Fetch the user's rentals
    async function fetchRentals() {
        try {
            const response = await fetch('http://localhost:5000/api/Customer/GetAllRentals');
            rentals = await response.json();
        } catch (error) {
            console.error('Error fetching rentals:', error);
        }
    }

    // Display available motorbikes
    function displayAvailableMotorbikes() {
        const searchBar = document.getElementById('searchBar');
        const searchQuery = searchBar ? searchBar.value.toLowerCase() : '';

        availableMotorbikeBody.innerHTML = ''; // Clear previous content

        motorbikes.forEach(motorbike => {
            if (!isMotorbikeRented(motorbike.id) && (
                motorbike.title.toLowerCase().includes(searchQuery) ||
                motorbike.description.toLowerCase().includes(searchQuery) ||
                motorbike.model.toLowerCase().includes(searchQuery) ||
                motorbike.brand.toLowerCase().includes(searchQuery) ||
                motorbike.category.toLowerCase().includes(searchQuery)
            )) {
                const bikeBox = document.createElement('div');
                bikeBox.classList.add('rent-box');
                const imageUrls = motorbike.imageUrl.split(',');
                let fullUrl = imageUrls.map(url => `http://localhost:5000${url}`).join('');

                bikeBox.innerHTML = `
                    <img src="${fullUrl}" alt="${motorbike.title}">
                    <div class="rent-layer">
                        <h4>${motorbike.title}</h4>
                        <p>${motorbike.description}</p>
                        <p>Model: ${motorbike.model}</p>
                        <p>Brand: ${motorbike.brand}</p>
                        <p>Category: ${motorbike.category}</p>
                        <a href="#" onclick="rentMotorbike('${motorbike.id}')"><i class='bx'>Rent</i></a>
                    </div>
                `;
                availableMotorbikeBody.appendChild(bikeBox);
            }
        });
    }

    // Check if a motorbike is currently rented
    function isMotorbikeRented(id) {
        return rentals.some(rental => rental.motorbikeID == id);
    }

    // Rent a motorbike
    window.rentMotorbike = async function (id) {
        const rental = {
            customerID: currentUser.id,
            motorbikeID: id,
            rentalDate: new Date().toISOString(),
            returndate: new Date().toISOString() // Adjust if needed
        };

        console.log('Rental Object:', rental);  // For debugging

        try {
            const response = await fetch('http://localhost:5000/api/Customer/rental', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(rental)
            });

            if (!response.ok) {
                throw new Error('Failed to rent motorbike');
            }

            console.log('Motorbike rented successfully:', id);

            // Fetch updated motorbikes and rentals after renting
            await fetchMotorbikes();
            await fetchRentals();
        } catch (error) {
            console.error('Error renting motorbike:', error);
        }
    };
    
    // Display user's rentals in a modal window
    async function displayMyRentals() {
        const profileModal = document.getElementById('profileModal');
        
        // Close profile modal if open
        if (profileModal) {
            profileModal.style.display = 'none';
        }

        // Ensure rentals table body exists and clear previous content
        if (!myRentalsTableBody) return;
        myRentalsTableBody.innerHTML = '';

        try {
            // Fetch the user's rentals from the server
            const response = await fetch('http://localhost:5000/api/Customer/GetAllRentals');
            if (!response.ok) {
                throw new Error('Failed to fetch rentals');
            }

            const rentals = await response.json();

            // Iterate through the user's rentals and populate the table
            rentals.forEach(rental => {
                if (rental.customerID === currentUser.id) {
                    const motorbike = motorbikes.find(mb => mb.id === rental.motorbikeID);
                    if (motorbike) {
                        const row = document.createElement('tr');
                        row.innerHTML = `
                            <td>${motorbike.regNumber}</td>
                            <td>${motorbike.brand}</td>
                            <td>${motorbike.model}</td>
                            <td>${motorbike.category}</td>
                            <td>${new Date(rental.rentalDate).toLocaleDateString()}</td>
                            <td>${rental.status}</td>
                        `;
                        myRentalsTableBody.appendChild(row); // Append row to the table
                    }
                }
            });

            // Show the modal containing the rentals
            if (rentalsModal) {
                rentalsModal.style.display = 'block';
            }
        } catch (error) {
            console.error('Error fetching rentals:', error);
        }
    }

    // Logout functionality
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function () {
            sessionStorage.removeItem('currentUser');
            window.location.href = '../bikerent-greetingpage.html';
        });
    }

    // Event listener to filter motorbikes by search input
    document.getElementById('searchBar').addEventListener('input', displayAvailableMotorbikes);

    // Event listener to trigger rentals display when "rentalhistory" button is clicked
    document.getElementById('rentalhistory').addEventListener('click', async function() {
        await fetchRentals(); // Fetch rentals before displaying them
        displayMyRentals();
    });

    // Close rentals modal
    if (closeRentalsModal) {
        closeRentalsModal.addEventListener('click', function () {
            if (rentalsModal) {
                rentalsModal.style.display = 'none'; // Hide the modal
            }
        });
    }

    // Optional: Close the modal if the user clicks outside the modal content
    window.addEventListener('click', function (event) {
        if (event.target === rentalsModal) {
            rentalsModal.style.display = 'none'; // Hide the modal
        }
    });

    // Initial fetch calls
    fetchMotorbikes();
    fetchRentals();
});



document.addEventListener('DOMContentLoaded', function () {
    const profileModal = document.getElementById('profileModal');
    const editProfileForm = document.getElementById('editProfileForm');
    const closeBtn = document.querySelector('.close');
    const userInfo = document.getElementById('userInfo');

    // Retrieve current user information from session storage
    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));

    // Function to open the modal and populate the form
    function openProfileModal() {
        document.getElementById('rentalsModal').style.display = 'none';

        if (currentUser) {
            document.getElementById('username').value = currentUser.firstName || '';
            document.getElementById('nic').value = currentUser.nic || '';
            document.getElementById('number').value = currentUser.mobilenumber || '';
            document.getElementById('password').value = currentUser.password || '';
        }
        profileModal.style.display = 'block'; // Show the modal
    }

    // Function to close the modal
    function closeProfileModal() {
        profileModal.style.display = 'none'; // Hide the modal
    }

    // Event listener for profile link to open the modal
    userInfo.addEventListener('click', openProfileModal);

    // Event listener for close button to close the modal
    closeBtn.addEventListener('click', closeProfileModal);

    // Event listener to close the modal if clicked outside of the content area
    window.addEventListener('click', function (event) {
        if (event.target === profileModal) {
            closeProfileModal();
        }
    });

    // Handle form submission for editing profile
    editProfileForm.addEventListener('submit', async function (event) {
        event.preventDefault(); // Prevent form submission

        // Get updated user information from form fields
        const updatedUser = {
            firstName: document.getElementById('username').value,
            nic: document.getElementById('nic').value,
            mobilenumber: document.getElementById('number').value,
            password: document.getElementById('password').value
        };

        // Update session storage with the new user information
        sessionStorage.setItem('currentUser', JSON.stringify(updatedUser));

        // Update user info on the page
        userInfo.textContent = updatedUser.firstName;

        try {
            // Send updated user information to the server
            const response = await fetch(`http://localhost:5000/api/Customer/${currentUser.id}`, { // Using customer ID in URL
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedUser)
            });

            if (!response.ok) {
                throw new Error('Failed to update profile');
            }

            alert('Profile updated successfully!');
        } catch (error) {
            console.error('Error updating profile:', error);
            alert('There was an error updating your profile. Please try again.');
        } finally {
            // Close the modal
            closeProfileModal();
        }
    });
});
